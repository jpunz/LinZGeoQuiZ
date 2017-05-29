using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Logic.Database;
using Logic.Model;

using Xamarin.Forms;

namespace LinzGeoQuiz
{
	public partial class Overview : ContentPage
	{
		public Overview()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (App.sumQuestions > 0)
			{
				LblAvgDeviation.Text = string.Format("Avg. Deviation: {0:0.00}km", (App.sumDistance / App.sumQuestions));
				LblQuestionCount.Text = string.Format("Questions: {0:0}", App.sumQuestions);
				LblGameCount.Text = string.Format("Games: {0:0}", App.sumGames);
			}

			if (FacebookAuth.isFBAuthenticated())
			{
				var jsonResponse = "";
				Task getUserInfo = new Task(() =>
				{
					jsonResponse = FacebookAPI.getUserInfo().Result;

				});
				getUserInfo.Start();
				getUserInfo.Wait();

				if (jsonResponse.Length > 0)
				{
					JObject o = JObject.Parse(jsonResponse);
					LblUser.Text = o.GetValue("name").ToString();
				}
			}
			else
			{
				LblUser.Text = "Guest";
			}

			var highscoreEntries = new Firebase().getHighscoreEntries();

			List<String> items = new List<string>();
			foreach (KeyValuePair<String, HighscoreEntry> pair in highscoreEntries)
			{
				double avgDeviation = pair.Value.sumDistance / pair.Value.sumQuestions;
				items.Add("#" + (items.Count + 1) + " " + pair.Value.name + ": " + string.Format("{0:0.00}km", avgDeviation));
			}

			listViewHighscore.ItemsSource = items.ToArray();

            if(FacebookAuth.isFBAuthenticated())
            {
                BtnLogin.Text = "Logout";
                BtnLogin.Clicked += Logout_Clicked;
                BtnLogin.Clicked -= Login_Clicked;
            }
		}

        void Login_Clicked(object sender, System.EventArgs e)
        {
            FacebookAuth.authenticateFacebook(Navigation);
        }

        void Logout_Clicked(object sender, System.EventArgs e)
        {
            FacebookAuth.logout();

            BtnLogin.Text = "Login with Facebook";
            BtnLogin.Clicked -= Logout_Clicked;
            BtnLogin.Clicked += Login_Clicked;
        }
    }
}
