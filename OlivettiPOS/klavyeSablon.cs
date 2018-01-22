using System.Collections.Generic;

namespace OlivettiPOS
{
    public class klavyeSablon
    {
        public static List<KlavyeTusu> KlavyeList;

        public class KlavyeTusu
        {
            public int sira { get; set; }
            public string kod { get; set; }

            public string tip { get; set; }

            public string deger { get; set; }
        }

        public static List<ListeUrun> UrunList;

        public class ListeUrun
        {
            public int sira { get; set; }
            public string urunBarkod { get; set; }
        }
    }
}