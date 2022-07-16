using Projektni_Zadatak.DTO;
using Projektni_Zadatak.DTO.Query2;
using Projektni_Zadatak.DTO.Query4;
using Projektni_Zadatak.Model;
using Projektni_Zadatak.Service;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projektni_Zadatak.UIHandler
{
    public class QueryUIHandler
    {
        private static readonly QueryFunctionalityService queryFunctionalityService = new QueryFunctionalityService();

        public void HandleQueryMenu()
        {
            String answer;
            do
            {
                Console.WriteLine("\nOdaberite funkcionalnost:");
                Console.WriteLine(
                        "\n1  - Za odabranog vozaca, prikazati sve njegove rezultate, i njegovu prosecnu maksimalnu briznu");
                Console.WriteLine(
                        "\n2  - Za svaku drzavu prikazati vozace koji su iz te drzave, njihovo prosecno godiste,ukupan broj titula  "
                            + "\n    i broj titula koje su uzeli na stazama svoje drzave");

                Console.WriteLine(
                        "\n3  - Dodavanje novog rezultata ");
                Console.WriteLine(
                        "\n4  - Za odabranu drzavu ispisati staze koje se u njoj nalaze. Za svaku stazu ispisati ukupnu duzinu "
                        + "\n    i prosecnu maksimalnu briznu kretanja, kao i sve vozace koji su bili prvoplasirani na toj stazi, u opadajucem "
                                + "\n    redosledu po sezonama");
                
                Console.WriteLine("\nX  - Izlazak");

                answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        PrikazeVozaceveRezultate();
                        break;
                    case "2":
                        PrikaziDrzaveIVozace();
                        break;
                    case "3":
                        DodajRezultat();
                        break;
                    case "4":
                        PrikaziStazeDrzave(); 
                        break;
                    

                }

            } while (!answer.ToUpper().Equals("X"));
        }

        private void PrikazeVozaceveRezultate()
        {
            

            try
            {
                Console.WriteLine("ID Vozaca: ");
                int id = int.Parse(Console.ReadLine());
                RezultatiVozacaDTO dto = queryFunctionalityService.GetRez(id);
                if(dto.Vozac != null)
                {
                    Console.WriteLine("\n" + Vozac.GetFormattedHeader());
                    Console.WriteLine(dto.Vozac);
                    if(dto.Rezultati.Count > 0)
                    {
                        double sumaMaksbr = 0;
                        int cnt = 0;
                        Console.WriteLine("\n" + Rezultat.GetFormattedHeader());
                        foreach(Rezultat r in dto.Rezultati)
                        {
                            Console.WriteLine(r);
                            sumaMaksbr += r.MaksBrzina;
                            cnt++;
                        }

                        Console.WriteLine("\nProsecna maksimalna brzina je : " + sumaMaksbr/cnt);

                    } else
                    {
                        Console.WriteLine("\n\t\t ----- TRAZENI VOZAC NEMA REZULTATE -----");
                    }

                } else
                {
                    // nema trazenog vozaca
                    Console.WriteLine("\n\t\t----- TRAZENI VOZAC NE POSTOJI -----");
                }
                
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void PrikaziDrzaveIVozace()
        {
            try
            {
                List<VozaciDrzaveDTO> dtos = queryFunctionalityService.GetVozaciDrzave();
                if(dtos.Count != 0)
                {
                    foreach(VozaciDrzaveDTO dto in dtos)
                    {
                        Console.WriteLine("\n\n" + dto.Drzava.Nazivd + ":");
                        if(dto.Vozaci.Count > 0)
                        {
                            int sumaGodista = 0;
                            int cnt = 0;
                            int sumaTitula = 0;
                            Console.WriteLine(Vozac.GetFormattedHeaderWithoutCountry());
                            foreach(Vozac v in dto.Vozaci)
                            {
                                Console.WriteLine(string.Format("{0,-6} {1,-20} {2,-20} {3,-10} {4,-10}",
                                                                    v.Idv, v.ImeV, v.PrezV, v.GodRodj, v.BrojTit));
                                sumaGodista += v.GodRodj;
                                cnt++;
                                sumaTitula += v.BrojTit;
                            }
                            Console.WriteLine(string.Format("Prosecno godiste vozaca : {0}; Ukupno titula : {1}",(Double)sumaGodista/cnt,sumaTitula));
                            Console.WriteLine(string.Format("Vozaci su uzeli {0} titule na stazama svoje drzave", queryFunctionalityService.GetBrojTitulaNaStazamaSvojeDrzave(dto)));
                        } else
                        {
                            Console.WriteLine("\t\t ----- NEMA VOZACA -----");
                        }
                    }

                } else
                {
                    Console.WriteLine("\t\t ----- NEMA DRZAVA -----");
                }
            }
            catch(DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void DodajRezultat()
        {
            try
            {
                Console.WriteLine("IDR: ");
                string idr = Console.ReadLine();

                Console.WriteLine("IDV: ");
                int idv = int.Parse(Console.ReadLine());

                Console.WriteLine("IDS: ");
                int ids = int.Parse(Console.ReadLine());

                Console.WriteLine("SEZONA: ");
                int sezona = int.Parse(Console.ReadLine());

                Console.WriteLine("PLASMAN: ");
                int plasman = int.Parse(Console.ReadLine());

                Console.WriteLine("BODOVI: ");
                int bodovi = int.Parse(Console.ReadLine());

                Console.WriteLine("MAKSBRZINA: ");
                double maksBrzina = double.Parse(Console.ReadLine());
                
                if(queryFunctionalityService.DodajNoviRezultat(new Rezultat(idr,idv,ids,sezona,plasman,bodovi,maksBrzina)))
                {
                    Console.WriteLine("\n\t\t ----- REZULTAT USPESNO DODAT -----");
                }
                else
                {
                    Console.WriteLine("\n\t\t ----- REZULTAT NIJE DODAT -----");
                }

            } 
            catch(DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void PrikaziStazeDrzave()
        {
            try
            {
                Console.WriteLine("DRZS: ");
                int idd = int.Parse(Console.ReadLine());

                Drzava drzava = queryFunctionalityService.NadjiDrzavu(idd);
                if (drzava != null)
                {
                    Console.WriteLine("\n\n" + drzava.Nazivd + ":");
                    List<StazaDrzaveDTO> dtos = queryFunctionalityService.StazeDrzave(idd);
                    if (dtos.Count > 0)
                    {
                        foreach (StazaDrzaveDTO dto in dtos)
                        {
                            Console.WriteLine("\n" + Staza.GetFormattedHeader());
                            Console.WriteLine(dto.Staza);
                            Console.WriteLine(string.Format("Ukupna duzina staze : {0}", dto.UkupnaDuzina));
                            Console.WriteLine(string.Format("Prosecna maksimalna brzina na stazi : {0}", dto.ProsecnaMaksimalnaBrzina));
                            if(dto.PrvoplasiraniVozaci.Count > 0)
                            {
                                Console.WriteLine("\n\t\t ----- PRVOPLASIRANI VOZACI -----\n");
                                Console.WriteLine(string.Format("{0,-20} {1,-20} {2,-10} {3,-8}",
                                                                    "IMEV", "PREZV", "SEZONA", "BODOVI"));
                                foreach(PrvoplasiraniVozaciDTO ppvdto in dto.PrvoplasiraniVozaci)
                                {
                                    Console.WriteLine(string.Format("{0,-20} {1,-20} {2,-10} {3,-8}",
                                                                    ppvdto.Vozac.ImeV, ppvdto.Vozac.PrezV, ppvdto.Sezona, ppvdto.Bodovi));
                                }
                            } else
                            {
                                Console.WriteLine("\n\t\t ----- NEMA REZULTATA O PRVOPLASIRANIM VOZACIMA -----");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n\t\t ----- DRZAVA NEMA STAZE -----");
                    }
                } else
                {
                    Console.WriteLine("\n\t\t ----- DRZAVA NE POSTOJI -----");
                }

            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

    }
}
