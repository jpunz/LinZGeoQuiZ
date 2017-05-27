using Xamarin.Forms;

namespace LinzGeoQuiz
{
	public partial class App : Application
	{
		private static App instance;

		private App()
		{
			InitializeComponent();

			MainPage = new MainTabbedPage();

			App.sumDistance = Properties.ContainsKey("sumDistance") ? (double)Properties["sumDistance"] : 0;
			App.sumQuestions = Properties.ContainsKey("sumQuestions") ? (int)Properties["sumQuestions"] : 0;
			App.sumGames = Properties.ContainsKey("sumGames") ? (int)Properties["sumGames"] : 0;
		}

		public static App getInstance()
		{
			if (instance == null)
			{
				instance = new App();
			}
			return instance;
		}

		public static double sumDistance;
		public static int sumQuestions, sumGames;

		public void updateScore()
		{
			Properties["sumDistance"] = sumDistance;
			Properties["sumQuestions"] = sumQuestions;
			Properties["sumGames"] = sumGames;
		}
	}
}
