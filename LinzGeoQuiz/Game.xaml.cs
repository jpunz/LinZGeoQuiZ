using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Logic.Database;
using Logic.Model;

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
		}

		void Handle_Clicked(object sender, System.EventArgs e)
		{
			setNewStreet();
		}
	}
}
