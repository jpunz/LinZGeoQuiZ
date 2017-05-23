using System;
using System.Collections.Generic;

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
		}
	}
}
