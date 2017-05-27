using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Database
{
    public class Firebase
    {
        private const string AUTHSECRET = "tqbbj0jnqp04G3LfRzptLBL82pSvBDW374GeXJEl";
        private const string URL = "https://linzgeoquiz.firebaseio.com";
        //private readonly string[] CATEGORIES = { "busstops", "nursinghomes", "streets", "hospitals" };

        private IFirebaseClient client;

        public Firebase()
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = AUTHSECRET,
                BasePath = URL,
            };

            client = new FirebaseClient(config);
        }

        public List<KeyValuePair<String, GeoObject>> getGeoObjects(String category)
        {
            List<KeyValuePair<String, GeoObject>> objects = new List<KeyValuePair<String, GeoObject>>();

            if (category.Equals("mixed"))
            {
                /*foreach(string cat in CATEGORIES)
                {
                    objects.AddRange(getGeoObjects(cat));
                }*/

                // Get all categories
                var geoObjects = client.Get("geoobjects").ResultAs<IDictionary<String, ICollection<GeoObject>>>();

                foreach (String key in geoObjects.Keys)
                {
                    foreach(GeoObject geoObject in geoObjects[key])
                    {
                        objects.Add(new KeyValuePair<String, GeoObject>(key, geoObject));
                    }
                }
            }
            else
            {
                foreach (GeoObject geoObject in client.Get("geoobjects/" + category).ResultAs<ICollection<GeoObject>>())
                {
                    objects.Add(new KeyValuePair<String, GeoObject>(category, geoObject));
                }
            }

            return objects;
        }
    }
}
