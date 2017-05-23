using Xamarin.Forms;

namespace LinzGeoQuiz
{
	public partial class App : Application
	{
		public static double sumDistance;
		public static int sumQuestions, sumGames;

		public App()
		{
			InitializeComponent();

			MainPage = new MainTabbedPage();

			App.sumDistance = Properties.ContainsKey("sumDistance") ? (double)Properties["sumDistance"] : 0;
			App.sumQuestions = Properties.ContainsKey("sumQuestions") ? (int)Properties["sumQuestions"] : 0;
			App.sumGames = Properties.ContainsKey("sumGames") ? (int)Properties["sumGames"] : 0;
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
			Properties["sumDistance"] = sumDistance;
			Properties["sumQuestions"] = sumQuestions;
			Properties["sumGames"] = sumGames;
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
