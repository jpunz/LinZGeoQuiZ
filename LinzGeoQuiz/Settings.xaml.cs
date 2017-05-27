using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Auth;

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
			FacebookAuth.authenticateFacebook(Navigation);
		}

		void Logout_Clicked(object sender, System.EventArgs e)
		{
			FacebookAuth.logout();
		}
	}
}
