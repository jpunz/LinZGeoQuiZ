using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LinzGeoQuiz
{
	public partial class Settings : ContentPage
	{
		public Settings()
		{
			InitializeComponent();
		}

		void Login_Clicked(object sender, System.EventArgs e)
		{
			Application.Current.MainPage = new Login();
		}
	}
}
