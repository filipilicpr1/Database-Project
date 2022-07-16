using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projektni_Zadatak.Model
{
    public class Rezultat
    {
        public string Idr { get; set; }
        public int Idv { get; set; }
        public int Ids { get; set; }
        public int Sezona { get; set; }
        public int Plasman { get; set; }
        public int Bodovi { get; set; }
        public double MaksBrzina { get; set; }

        public Rezultat(string idr,int idv,int ids,int sezona,int plasman,int bodovi, double maksBrzina)
        {
            this.Idr = idr;
            this.Idv = idv;
            this.Ids = ids;
            this.Sezona = sezona;
            this.Plasman = plasman;
            this.Bodovi = bodovi;
            this.MaksBrzina = maksBrzina;
        }

        public override string ToString()
        {
            return string.Format("{0,-6} {1,-6} {2,-6} {3,-10} {4,-8} {5,-8} {6,-10}",
                Idr, Idv, Ids, Sezona, Plasman, Bodovi, MaksBrzina);
        }

        public static string GetFormattedHeader()
        {
            return string.Format("{0,-6} {1,-6} {2,-6} {3,-10} {4,-8} {5,-8} {6,-10}",
                "IDR", "IDV", "IDS", "SEZONA", "PLASMAN", "BODOVI", "MAKSBRZINA");
        }


    }
}
