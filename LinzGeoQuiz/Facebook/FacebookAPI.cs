using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Auth;
using Xamarin.Forms;

namespace LinzGeoQuiz
{
	class FacebookAPI
	{
		private static string FB_API = "https://graph.facebook.com/v2.9/";

		public static Task<String> getUserInfo()
		{
			return makeRequest("POST", FB_API + "me?fields=id,name,email", null);
		}

		public static Task<String> deleteAccessToken()
		{
			return makeRequest("DELETE", FB_API + "me/permissions", null);
		}

		public static Task<String> createPost(string message)
		{
			IDictionary<string, string> body = new Dictionary<string, string>();
			body.Add("message", message);

			return makeRequest("POST", FB_API + "me/feed", body);
		}

		async static Task<String> makeRequest(String command, String url, IDictionary<string, string> body)
		{
			var request = new OAuth2Request(command, new Uri(url), body, (Account)Application.Current.Properties["Account"]);
			var response = await request.GetResponseAsync();
			if (response != null)
			{
				return response.GetResponseText();
			}
			return "[ERROR] Request";
		}
	}
}