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
		private ICollection<GeoObject> geoObjects;
		private Geocoder geoCoder;
		private TKCustomMap map;

		public Game()
		{
			InitializeComponent();

			map = new TKCustomMap(MapSpan.FromCenterAndRadius(new Position(48.286998, 14.294665), Distance.FromKilometers(5)));
			map.MapType = MapType.Satellite;
			map.BindingContext = new MapViewModel();
			map.SetBinding(TKCustomMap.CustomPinsProperty, "Pins");
			map.SetBinding(TKCustomMap.MapClickedCommandProperty, "MapClickedCommand");

			MainGrid.Children.Insert(0, map);

			geoObjects = new Firebase().getStreets();

			geoCoder = new Geocoder();

            setNewStreet();
        }

		private void setNewStreet()
		{
			LblGeoObjectName.Text = System.Linq.Enumerable.ElementAt(geoObjects, new Random().Next(geoObjects.Count - 1)).name;
			LblGeoObjectName.TextColor = Color.Black;
			BtnDone.Source = "Done.png";
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

					LblGeoObjectName.Text = string.Format("Distance to location: {0:0.00}km", distance(position, ((MapViewModel)map.BindingContext).Pins[0].Position));
					LblGeoObjectName.TextColor = Color.Red;

					break;
				}
			}
			else if(!LblGeoObjectName.TextColor.Equals(Color.Black))
			{
				((MapViewModel)map.BindingContext).clearPins();

                map.MoveToMapRegion(MapSpan.FromCenterAndRadius(new Position(48.286998, 14.294665), Distance.FromKilometers(5)), true);
                setNewStreet();
			}
		}

		public void PlacePin(double longitude, double latitude)
		{
			map.Pins.Clear();

			Pin userPin = new Pin();
			userPin.Position = new Position(latitude, longitude);
			userPin.Label = "Your guess";

			map.Pins.Add(userPin);
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
	}
}
