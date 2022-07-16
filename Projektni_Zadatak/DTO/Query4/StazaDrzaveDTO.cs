using Projektni_Zadatak.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projektni_Zadatak.DTO.Query4
{
    public class StazaDrzaveDTO
    {
        public Staza Staza { get; set; }
        public double UkupnaDuzina { get; set; }
        public double ProsecnaMaksimalnaBrzina { get; set; }
        public List<PrvoplasiraniVozaciDTO> PrvoplasiraniVozaci { get; set; }
    }
}
