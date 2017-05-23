using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LinzGeoQuiz
{
	public partial class GameResult : ContentPage
	{
		private double avgDev = 0;

		public GameResult(double avgDeviation)
		{
			InitializeComponent();
			avgDev = avgDeviation;
			LblAvgDeviation.Text = string.Format("Avg. Deviation: {0:0.00}km", avgDeviation);
		}

		async void Close_Handle_Clicked(object sender, System.EventArgs e)
		{
			int numModals = Application.Current.MainPage.Navigation.ModalStack.Count;
			for (int currModal = 0; currModal < numModals; currModal++)
			{
				await Application.Current.MainPage.Navigation.PopModalAsync();
			}
		}

		void Share_Handle_Clicked(object sender, System.EventArgs e)
		{
			if (FacebookAuth.isFBAuthenticated())
			{
				FacebookAPI.createPost(string.Format("I just played a few rounds Linz Geo Quiz and got an average deviation of {0:0.00}km!!!111 Come and check it out too :-)", avgDev));
			}
		}
	}
}
