using FireSharp;
using FireSharp.Config;
using FireSharp.Exceptions;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts
{
    class ContentFilter
    {
        public static void execute()
        {
            try
            {
                IFirebaseConfig config = new FirebaseConfig
                {
                    AuthSecret = "tqbbj0jnqp04G3LfRzptLBL82pSvBDW374GeXJEl",
                    BasePath = "https://linzgeoquiz.firebaseio.com"
                    
                };

                IFirebaseClient client = new FirebaseClient(config);


                //var getResponse = client.Get("streets");
                FirebaseResponse response = client.Get("streets");
                IDictionary<int, Street> streets = response.ResultAs<IDictionary<int, Street>>();

                Console.WriteLine("Nr. of streets: " + streets.Count);

                foreach(int i in streets.Keys)
                {
                    if(streets[i] == null || string.IsNullOrEmpty(streets[i].is_in) || string.IsNullOrEmpty(streets[i].name) || !streets[i].is_in.ToLower().Contains("linz") || streets[i].is_in.ToLower().Contains("linz-land"))
                    {
                        var deleteResponse = client.Delete("streets/" + i);
                    }
                }
            }
            catch(FirebaseException ex)
            {
                Console.WriteLine(ex.ToString());
                ContentFilter.execute();
            }
        }
    }
}
