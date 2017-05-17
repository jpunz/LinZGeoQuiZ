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
        static void Main(string[] args)
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
                List<Street> streets = response.ResultAs<List<Street>>();

                Console.WriteLine("Nr. of streets: " + streets.Count);

                for(int i = 0; i < streets.Count; i++)
                {
                    if(string.IsNullOrEmpty(streets[i].is_in) || string.IsNullOrEmpty(streets[i].name) || !streets[i].is_in.ToLower().Contains("linz") || streets[i].is_in.ToLower().Contains("linz-land"))
                    {
                        var deleteResponse = client.Delete("streets/" + i);
                        //Street st = deleteResponse.ResultAs<Street>();
                    }
                }
            }
            catch(FirebaseException ex)
            {
                Console.WriteLine(ex.ToString());
                Main(null);
            }
        }
    }
}
