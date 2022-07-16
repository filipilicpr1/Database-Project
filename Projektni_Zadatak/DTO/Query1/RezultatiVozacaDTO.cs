using Projektni_Zadatak.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projektni_Zadatak.DTO
{
    public class RezultatiVozacaDTO
    {
        public Vozac Vozac { get; set; }
        public List<Rezultat> Rezultati { get; set; }
    }
}
