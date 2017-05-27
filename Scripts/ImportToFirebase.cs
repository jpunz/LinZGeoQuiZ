using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts
{
    class ImportToFirebase
    {
        public static void execute()
        {
            Dictionary<string, List<Street>> content = JsonConvert.DeserializeObject<Dictionary<string, List<Street>>>(File.ReadAllText("../../Linz.json"));

            content["streets"].RemoveAll(s => string.IsNullOrEmpty(s.is_in) || string.IsNullOrEmpty(s.name) || !s.is_in.ToLower().Contains("linz") || s.is_in.ToLower().Contains("linz-land"));

            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "tqbbj0jnqp04G3LfRzptLBL82pSvBDW374GeXJEl",
                BasePath = "https://linzgeoquiz.firebaseio.com"

            };

            IFirebaseClient client = new FirebaseClient(config);

            client.Set("geoobjects", content);
        }
    }
}
