using Projektni_Zadatak.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projektni_Zadatak.DAO
{
    public interface IVozacDAO : ICRUDDao<Vozac,int>
    {
        List<Vozac> NadjiVozacePoIdDrzave(int idd);
    }
}
