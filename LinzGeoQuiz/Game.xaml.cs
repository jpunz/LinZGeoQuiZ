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

		public Game()
		{
			InitializeComponent();
			geoObjects = new Firebase().getStreets();

			setNewStreet();
		}

		private void setNewStreet()
		{
			LblGeoObjectName.Text = System.Linq.Enumerable.ElementAt(geoObjects, new Random().Next(geoObjects.Count - 1)).name;
			map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(48.286998, 14.294665), Distance.FromKilometers(5)));
		}


		void Cancel_Handle_Clicked(object sender, System.EventArgs e)
		{
		}

		void Done_Handle_Clicked(object sender, System.EventArgs e)
		{
			setNewStreet();
		}
	}
}
