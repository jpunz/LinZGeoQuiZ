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
			FacebookAPI.createPost("Xamarin is retarded.");


			var jsonResponse = "";
			Task getUserInfo = new Task(() =>
			{
				jsonResponse = FacebookAPI.getUserInfo().Result;

			});
			getUserInfo.Start();
			getUserInfo.Wait();

			Debug.WriteLine(jsonResponse);
		}
	}
}
