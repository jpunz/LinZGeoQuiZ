﻿using System;
using System.Diagnostics;

using Xamarin.Auth;
using Xamarin.Forms;

namespace LinzGeoQuiz
{
	class FacebookAuth
	{
		private static bool isAuthenticated = false;

		public static void authenticateFacebook(INavigation navigation)
		{
			var auth = new OAuth2Authenticator(
				clientId: "1104105879722551",
				scope: "email,publish_actions",
				authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
				redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));

			var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
			presenter.Completed += (sending, eventArgs) =>
			{
				if (eventArgs.IsAuthenticated)
				{
					Application.Current.Properties["Account"] = eventArgs.Account;

					// TODO remove if finished
					Debug.WriteLine(eventArgs.Account.Properties["access_token"]);

					isAuthenticated = true;
				}
				else
				{
					// Login canceled
					Debug.WriteLine("Authentication failed.");
					isAuthenticated = false;
				}
			};
			presenter.Login(auth);
		}

		public static void logout()
		{
			FacebookAPI.deleteAccessToken();
		}

		public static bool isFBAuthenticated()
		{
			return isAuthenticated;
		}
	}
}