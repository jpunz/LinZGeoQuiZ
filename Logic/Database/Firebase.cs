using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Database
{
    class Firebase
    {
        private const string AUTHSECRET = "tqbbj0jnqp04G3LfRzptLBL82pSvBDW374GeXJEl";
        private const string URL = "https://linzgeoquiz.firebaseio.com";

        private IFirebaseClient client;

        public Firebase()
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = AUTHSECRET,
                BasePath = URL

            };

            client = new FirebaseClient(config);
        }

        public ICollection<GeoObject> getStreets()
        {
            return client.Get("streets").ResultAs<IDictionary<int, GeoObject>>().Values;
        }
    }
}
