using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projektni_Zadatak.Model
{
    public class Staza
    {
        public int Ids { get; set; }
        public string Nazivs { get; set; }
        public int BrojKrug { get; set; }
        public double DuzKrug { get; set; }
        public int Drzs { get; set; }

        public Staza(int ids, string nazivs,int brojkrug,double duzkrug,int drzs)
        {
            this.Ids = ids;
            this.Nazivs = nazivs;
            this.BrojKrug = brojkrug;
            this.DuzKrug = duzkrug;
            this.Drzs = drzs;
        }

        public override string ToString()
        {
            return string.Format("{0,-6} {1,-30} {2,-10} {3,-10} {4,-8}",
                Ids, Nazivs, BrojKrug, DuzKrug, Drzs);
        }

        public static string GetFormattedHeader()
        {
            return string.Format("{0,-6} {1,-30} {2,-10} {3,-10} {4,-8}",
                "IDS", "NAZIVS", "BROJKRUG", "DUZKRUG", "DRZS");
        }

    }
}
