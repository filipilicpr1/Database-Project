using Projektni_Zadatak.DTO.Query4;
using Projektni_Zadatak.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projektni_Zadatak.DAO
{
    public interface IRezultatDAO : ICRUDDao<Rezultat,string>
    {
        List<Rezultat> RezultatiVozaca(int idv);
        List<Rezultat> TitulePoIdVozaca(IEnumerable<int> idvs);
        double ProsecnaMaksimalnaBrzinaPoStazi(int ids);
        List<PomocniDTO> PrvoplasiraniVozaciPoStazi(int ids);
    }
}
