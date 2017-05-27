using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using TK.CustomMap.iOSUnified;
using UIKit;
using Xamarin;
using Xamarin.Forms;

namespace LinzGeoQuiz.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			//global::Xamarin.Forms.Forms.Init();
			//Xamarin.FormsMaps.Init();

			Forms.Init();
		    FormsMaps.Init();
		    TKCustomMapRenderer.InitMapRenderer();
		    NativePlacesApi.Init();

			Xamarin.Auth.Presenters.OAuthLoginPresenter.PlatformLogin = (authenticator) =>
			{
				var oAuthLogin = new OAuthLoginPresenter();
				oAuthLogin.Login(authenticator);
			};

			LoadApplication(App.getInstance());
		    return base.FinishedLaunching(app, options);

			//LoadApplication(new App());

			//return base.FinishedLaunching(app, options);
		}
	}
}
