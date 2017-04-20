using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LinzGeoQuiz
{
	public partial class ForgotPW : ContentPage
	{
		public ForgotPW()
		{
			InitializeComponent();
		}

		void Send_Clicked(object sender, System.EventArgs e)
		{
			Application.Current.MainPage = new Login();
		}

		void Cancel_Clicked(object sender, System.EventArgs e)
		{
			Application.Current.MainPage = new Login();
		}
	}
}
