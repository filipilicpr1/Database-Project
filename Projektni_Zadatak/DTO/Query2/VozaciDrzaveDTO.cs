using Projektni_Zadatak.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projektni_Zadatak.DTO.Query2
{
    public class VozaciDrzaveDTO
    {
        public Drzava Drzava { get; set; }
        public List<Vozac> Vozaci { get; set; }
    }
}
