using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projektni_Zadatak.DTO.Query4
{
    public class PomocniDTO
    {
        public int Idv { get; set; }
        public int Sezona { get; set; }
        public int Bodovi { get; set; }

        public PomocniDTO(int idv, int sezona, int bodovi)
        {
            this.Idv = idv;
            this.Sezona = sezona;
            this.Bodovi = bodovi;
        }
    }
}
