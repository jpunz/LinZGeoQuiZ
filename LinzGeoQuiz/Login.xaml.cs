using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Auth;

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
			//Application.Current.MainPage = new MainTabbedPage();
            authenticateFacebook(Navigation);
		}

		public static void authenticateFacebook(INavigation navigation)
		{
			var auth = new OAuth2Authenticator(
				clientId: "1104105879722551",
				scope: "",
				authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
				redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));

			var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
			presenter.Completed += (sending, eventArgs) =>
			{
				if (eventArgs.IsAuthenticated)
				{
					//Application.Current.Properties["Account"] = eventArgs.Account;
					System.Diagnostics.Debug.WriteLine("Success");
                    System.Diagnostics.Debug.WriteLine(eventArgs.Account.ToString());
				}
				else
				{
					// Login canceled
					System.Diagnostics.Debug.WriteLine("Cancel");
			   }
			};
			presenter.Login(auth);
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
