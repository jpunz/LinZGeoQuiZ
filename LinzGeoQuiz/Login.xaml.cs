using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LinzGeoQuiz
{
	public partial class Login : ContentPage
	{
		public Login()
		{
			InitializeComponent();
		}

		void Login_Clicked(object sender, System.EventArgs e)
		{
			Application.Current.MainPage = new MainTabbedPage();
		}

		void Register_Clicked(object sender, System.EventArgs e)
		{
			Application.Current.MainPage = new Register();
		}

		void ForgotPW_Clicked(object sender, System.EventArgs e)
		{
			Application.Current.MainPage = new ForgotPW();
		}

		void Cancel_Clicked(object sender, System.EventArgs e)
		{
			Application.Current.MainPage = new MainTabbedPage();
		}
	}
}
