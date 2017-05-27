using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Logic.Database;
using Logic.Model;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;
using LinzGeoQuiz.ViewModel;
using TK.CustomMap;

namespace LinzGeoQuiz
{
	public partial class Game : ContentPage
	{
		private List<KeyValuePair<String, GeoObject>> geoObjects;
		private Geocoder geoCoder;
		private TKCustomMap map;
		private int numberOfQuestions;
		private String category;
		private bool isTimeGame;
		private int curQuestionNr;
		private double sumDistance = 0.0;
        private KeyValuePair<string, GeoObject> randomObject;
		private bool pauseTimer = false;

		public Game(int numberOfQuestions, String category, bool isTimeGame)
		{
			InitializeComponent();

			this.numberOfQuestions = numberOfQuestions;
			this.category = category;
			this.isTimeGame = isTimeGame;
			curQuestionNr = 0;

			map = new TKCustomMap(MapSpan.FromCenterAndRadius(new Position(48.286998, 14.294665), Distance.FromKilometers(5)));
			map.MapType = MapType.Satellite;
			map.BindingContext = new MapViewModel();
			map.SetBinding(TKCustomMap.CustomPinsProperty, "Pins");
			map.SetBinding(TKCustomMap.MapClickedCommandProperty, "MapClickedCommand");

			MainGrid.Children.Insert(0, map);

			contentViewRemainingTime.IsVisible = isTimeGame;
        }

		protected override void OnAppearing() // TODO don't block UI-thread
		{
            geoObjects = new Firebase().getGeoObjects(category);
			geoCoder = new Geocoder();

			setNewStreet();

			if (isTimeGame)
			{
				Device.StartTimer(TimeSpan.FromSeconds(1), () =>
				{
					if (!pauseTimer)
					{
						int remainingTime = int.Parse(LblRemainingTime.Text);
						if (remainingTime > 0)
						{
							LblRemainingTime.Text = "" + (remainingTime - 1);
						}
						else
						{
							BtnDone.Source = "Next.png";

							var currentDistance = 10;
							sumDistance += currentDistance;
							LblGeoObjectName.Text = string.Format("Distance to location: {0:0.00}km", currentDistance);
							LblGeoObjectName.TextColor = Color.Red;
						}
					}

					return true;
				});
			}

			contentViewLoading.IsVisible = false;
		}

		private void setNewStreet()
		{
            randomObject = System.Linq.Enumerable.ElementAt(geoObjects, new Random().Next(geoObjects.Count - 1));
            LblGeoObjectName.Text = randomObject.Value.name;
			LblGeoObjectName.TextColor = Color.Black;
			BtnDone.Source = "Done.png";
			curQuestionNr++;

			LblRemainingTime.Text = "30";
			pauseTimer = false;
		}

		async void Cancel_Handle_Clicked(object sender, System.EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		async void Done_Handle_Clicked(object sender, System.EventArgs e)
		{
			if (((MapViewModel)map.BindingContext).Pins.Count == 1 && LblGeoObjectName.TextColor.Equals(Color.Black))
			{
				BtnDone.Source = "Next.png";
				pauseTimer = true;

                if (randomObject.Key.Equals("streets"))
                {
                    var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(((MapViewModel)map.BindingContext).Pins[0].Position);

                    foreach (var address in possibleAddresses)
                    {
                        if (address.Contains(LblGeoObjectName.Text))
                        {
                            // Correct
                            LblGeoObjectName.Text = "Correct!";
                            LblGeoObjectName.TextColor = Color.Green;

                            return;
                        }
                    }

                    // If we land here, the chosen answer wasn't right
                    var possiblePositions = await geoCoder.GetPositionsForAddressAsync(LblGeoObjectName.Text + ", Linz");

                    foreach (var position in possiblePositions)
                    {
                        ((MapViewModel)map.BindingContext).addSolutionPin(position, LblGeoObjectName.Text);
                        map.MoveToMapRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(5)), true);

						var currentDistance = distance(position, ((MapViewModel)map.BindingContext).Pins[0].Position);
						sumDistance += currentDistance;
						LblGeoObjectName.Text = string.Format("Distance to location: {0:0.00}km", currentDistance);
                        LblGeoObjectName.TextColor = Color.Red;

                        break;
                    }
                }
                else // Other categories
                {
                    Position correctPosition = new Position(randomObject.Value.latitude, randomObject.Value.longitude);

                    ((MapViewModel)map.BindingContext).addSolutionPin(correctPosition, LblGeoObjectName.Text);
                    map.MoveToMapRegion(MapSpan.FromCenterAndRadius(new Position(randomObject.Value.latitude, randomObject.Value.longitude), Distance.FromKilometers(5)), true);

                    var dist = distance(correctPosition, ((MapViewModel)map.BindingContext).Pins[0].Position);
					sumDistance += dist;

                    if(dist < 0.01)
                    {
                        // Correct
                        LblGeoObjectName.Text = "Correct!";
                        LblGeoObjectName.TextColor = Color.Green;
                    }
                    else
                    {
                        LblGeoObjectName.Text = string.Format("Distance to location: {0:0.00}km", dist);
                        LblGeoObjectName.TextColor = Color.Red;
                    }
                }
			}
			else if (!LblGeoObjectName.TextColor.Equals(Color.Black))
			{
				// check if we reached question limit
				if (curQuestionNr < numberOfQuestions)
				{
					((MapViewModel)map.BindingContext).clearPins();

					map.MoveToMapRegion(MapSpan.FromCenterAndRadius(new Position(48.286998, 14.294665), Distance.FromKilometers(5)), true);
					setNewStreet();
				}
				else
				{
					await saveStatistics();

					await Navigation.PushModalAsync(new GameResult(sumDistance / numberOfQuestions));
				}
			}
		}

		private double distance(Position position1, Position position2)
		{
			double lat1 = position1.Latitude;
			double lon1 = position1.Longitude;
			double lat2 = position2.Latitude;
			double lon2 = position2.Longitude;

			double theta = lon1 - lon2;
			double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
			dist = Math.Acos(dist);
			dist = rad2deg(dist);
			dist = dist * 60 * 1.1515;
			dist = dist * 1.609344;

			return dist;
		}

		private double deg2rad(double deg)
		{
			return (deg * Math.PI / 180.0);
		}

		private double rad2deg(double rad)
		{
			return (rad / Math.PI * 180.0);
		}

		private Task saveStatistics()
		{
			App.sumDistance += sumDistance;
			App.sumQuestions += numberOfQuestions;
			App.sumGames++;

			App.getInstance().updateScore();
			return App.getInstance().SavePropertiesAsync();
		}
	}
}
