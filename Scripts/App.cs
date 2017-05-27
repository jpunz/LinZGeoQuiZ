using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts
{
    class App
    {
        static void Main(string[] args)
        {
            ImportToFirebase.execute(); // Import to firebase from Linz.json
            //ContentFilter.execute(); // Filtering empty street-names and streets outside of Linz
        }
    }
}
