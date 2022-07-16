using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projektni_Zadatak.Model
{
    public class Drzava
    {
        public int Idd { get; set; }
        public string Nazivd { get; set; }

        public Drzava(int idd,string nazivd)
        {
            this.Idd = idd;
            this.Nazivd = nazivd;
        }

        public override string ToString()
        {
            return string.Format("{0,-5} {1,-20}",
                Idd, Nazivd);
        }

        public static string GetFormattedHeader()
        {
            return string.Format("{0,-5} {1,-20}",
                "IDD", "NAZIVD");
        }

    }
}
