using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Logic.Database;
using Logic.Model;
using Xamarin.Forms.Maps;

namespace LinzGeoQuiz
{
	public partial class Game : ContentPage
	{
		private ICollection<GeoObject> geoObjects;
		private Geocoder geoCoder;

		public Game()
		{
			InitializeComponent();
			geoObjects = new Firebase().getStreets();
			geoCoder = new Geocoder();

			setNewStreet();
		}

		private void setNewStreet()
		{
			LblGeoObjectName.Text = System.Linq.Enumerable.ElementAt(geoObjects, new Random().Next(geoObjects.Count - 1)).name;
			map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(48.286998, 14.294665), Distance.FromKilometers(5)));
			LblGeoObjectName.TextColor = Color.Black;
			BtnDone.Source = "Done.png";
		}

		void Handle_Tap(object sender, LinzGeoQuiz.CustomElement.TapEventArgs e)
		{
			if (LblGeoObjectName.TextColor.Equals(Color.Black))
			{
				PlacePin(e.Position.Longitude, e.Position.Latitude);
			}
		}

		void Cancel_Handle_Clicked(object sender, System.EventArgs e)
		{
			Navigation.PopModalAsync();
		}

		async void Done_Handle_Clicked(object sender, System.EventArgs e)
		{
			if (map.Pins.Count == 1 && LblGeoObjectName.TextColor.Equals(Color.Black))
			{
				BtnDone.Source = "Next.png";

				var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(map.Pins[0].Position);

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

				Pin rightPin = new Pin();
				rightPin.Label = "Correct location of ";
				rightPin.Address = LblGeoObjectName.Text;

				foreach (var position in possiblePositions)
				{
					rightPin.Position = new Position(position.Latitude, position.Longitude);
					map.MoveToRegion(MapSpan.FromCenterAndRadius(rightPin.Position, Distance.FromKilometers(5)));
					break;
				}

				map.Pins.Add(rightPin);

				LblGeoObjectName.Text = string.Format("Distance to location: {0:0.00}km", distance(rightPin.Position.Latitude, rightPin.Position.Longitude, map.Pins[0].Position.Latitude, map.Pins[0].Position.Longitude));
				LblGeoObjectName.TextColor = Color.Red;
			}
			else if(!LblGeoObjectName.TextColor.Equals(Color.Black))
			{
				map.Pins.Clear();

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

		private double distance(double lat1, double lon1, double lat2, double lon2)
		{
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
