using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projektni_Zadatak.Model
{
    public class Vozac
    {
        public int Idv { get; set; }
        public string ImeV { get; set; }
        public string PrezV { get; set; }
        public int GodRodj { get; set; }
        public int BrojTit { get; set; }
        public int Drzv { get; set; }


        public Vozac(int idv, string imev,string prezv, int godrodj, int brojtit, int drzv)
        {
            this.Idv = idv;
            this.ImeV = imev;
            this.PrezV = prezv;
            this.GodRodj = godrodj;
            this.BrojTit = brojtit;
            this.Drzv = drzv;
        }


        public override string ToString()
        {
            return string.Format("{0,-6} {1,-20} {2,-20} {3,-10} {4,-10} {5,-8}",
                Idv, ImeV, PrezV, GodRodj, BrojTit, Drzv);
        }

        public static string GetFormattedHeader()
        {
            return string.Format("{0,-6} {1,-20} {2,-20} {3,-10} {4,-10} {5,-8}",
                "IDV", "IMEV", "PREZV", "GODRODJ", "BROJTIT", "DRZV");
        }

        public static string GetFormattedHeaderWithoutCountry()
        {
            return string.Format("{0,-6} {1,-20} {2,-20} {3,-10} {4,-10}",
                "IDV", "IMEV", "PREZV", "GODRODJ", "BROJTIT");
        }

    }
}
