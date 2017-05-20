using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LinzGeoQuiz
{
	public partial class GameSettings : ContentPage
	{
		public GameSettings()
		{
			InitializeComponent();
		}

		void Handle_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
		{
			LblNrQuestions.Text = ((Stepper)sender).Value.ToString();
		}

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			await Navigation.PushModalAsync(new Game(Int32.Parse(LblNrQuestions.Text)));
		}
	}
}
