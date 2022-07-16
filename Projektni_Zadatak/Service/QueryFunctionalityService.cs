using Projektni_Zadatak.DAO;
using Projektni_Zadatak.DAO.Impl;
using Projektni_Zadatak.DTO;
using Projektni_Zadatak.DTO.Query2;
using Projektni_Zadatak.DTO.Query4;
using Projektni_Zadatak.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projektni_Zadatak.Service
{
    public class QueryFunctionalityService
    {
        private static readonly IVozacDAO vozacDAO = new VozacDAOImpl();
        private static readonly IRezultatDAO rezultatDAO = new RezultatDAOImpl();
        private static readonly IDrzavaDAO drzavaDAO = new DrzavaDAOImpl();
        private static readonly IStazaDAO stazaDAO = new StazaDAOImpl();

        // query 1
        public RezultatiVozacaDTO GetRez(int id)
        {
            RezultatiVozacaDTO ret = new RezultatiVozacaDTO();
            Vozac vozac = vozacDAO.FindById(id);
            ret.Vozac = vozac;
            if (vozac != null)
            {
                List<Rezultat> rezultati = rezultatDAO.RezultatiVozaca(vozac.Idv);
                ret.Rezultati = rezultati;
            }
            return ret;
        }

        // query 2
        public List<VozaciDrzaveDTO> GetVozaciDrzave()
        {
            List<VozaciDrzaveDTO> ret = new List<VozaciDrzaveDTO>();
            foreach(Drzava d in drzavaDAO.FindAll())
            {
                VozaciDrzaveDTO dto = new VozaciDrzaveDTO();
                dto.Drzava = d;
                dto.Vozaci = vozacDAO.NadjiVozacePoIdDrzave(d.Idd);
                ret.Add(dto);
            }
            return ret;
        }

        public int GetBrojTitulaNaStazamaSvojeDrzave(VozaciDrzaveDTO dto)
        {
            List<Staza> staze = stazaDAO.NadjiStazePoIdDrzave(dto.Drzava.Idd);
            List<int> idvs = new List<int>();
            foreach(Vozac v in dto.Vozaci)
            {
                idvs.Add(v.Idv);
            }
            List<Rezultat> rezultati = rezultatDAO.TitulePoIdVozaca(idvs);
            int cnt = 0;
            foreach(Staza s in staze)
            {
                foreach(Rezultat rez in rezultati)
                {
                    if(s.Ids == rez.Ids)
                    {
                        cnt++;
                    }
                }
            }
            return cnt;
        }

        // query 3
        public bool DodajNoviRezultat(Rezultat rezultat)
        {
            bool ret = rezultatDAO.Save(rezultat) != 0;
            if (rezultat.Plasman == 1)
            {
                Vozac vozac = vozacDAO.FindById(rezultat.Idv);
                if(vozac != null)
                {
                    vozac.BrojTit++;
                    vozacDAO.Save(vozac);
                }
            }
            return ret;
        }

        // query 4
        public List<StazaDrzaveDTO> StazeDrzave(int idd)
        {
            List<StazaDrzaveDTO> ret = new List<StazaDrzaveDTO>();

            List<Staza> staze = stazaDAO.NadjiStazePoIdDrzave(idd);

            foreach(Staza staza in staze)
            {
                StazaDrzaveDTO dto = new StazaDrzaveDTO();
                dto.Staza = staza;
                dto.UkupnaDuzina = staza.DuzKrug * staza.BrojKrug;
                dto.ProsecnaMaksimalnaBrzina = rezultatDAO.ProsecnaMaksimalnaBrzinaPoStazi(staza.Ids);
                List<PomocniDTO> pomocni = rezultatDAO.PrvoplasiraniVozaciPoStazi(staza.Ids);
                List<PrvoplasiraniVozaciDTO> prvoplasirani = new List<PrvoplasiraniVozaciDTO>();
                foreach(PomocniDTO pom in pomocni)
                {
                    PrvoplasiraniVozaciDTO ppv = new PrvoplasiraniVozaciDTO();
                    ppv.Vozac = vozacDAO.FindById(pom.Idv);
                    ppv.Sezona = pom.Sezona;
                    ppv.Bodovi = pom.Bodovi;
                    prvoplasirani.Add(ppv);
                }
                dto.PrvoplasiraniVozaci = prvoplasirani;
                ret.Add(dto);
            }
            return ret;
        }

        public Drzava NadjiDrzavu(int idd)
        {
            return drzavaDAO.FindById(idd);
        }
    }
}
