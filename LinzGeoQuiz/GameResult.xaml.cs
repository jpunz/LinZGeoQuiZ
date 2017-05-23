using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LinzGeoQuiz
{
	public partial class GameResult : ContentPage
	{
		public GameResult(double avgDeviation)
		{
			InitializeComponent();

			LblAvgDeviation.Text = string.Format("Avg. Deviation: {0:0.00}km", avgDeviation);
		}

		async void Close_Handle_Clicked(object sender, System.EventArgs e)
		{
			int numModals = Application.Current.MainPage.Navigation.ModalStack.Count;
			for (int currModal = 0; currModal<numModals; currModal++)
			{
			    await Application.Current.MainPage.Navigation.PopModalAsync();
			}
		}
	}
}
