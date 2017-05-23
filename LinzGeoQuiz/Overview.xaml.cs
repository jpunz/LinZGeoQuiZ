using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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
					LblMail.Text = o.GetValue("email").ToString();
					LblUser.Text = o.GetValue("name").ToString();
				}
			}
		}
	}
}
