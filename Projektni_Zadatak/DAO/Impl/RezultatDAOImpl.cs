using Projektni_Zadatak.Connection;
using Projektni_Zadatak.DTO.Query4;
using Projektni_Zadatak.Model;
using Projektni_Zadatak.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projektni_Zadatak.DAO.Impl
{
    public class RezultatDAOImpl : IRezultatDAO
    {
        public int Count()
        {
            throw new NotImplementedException();
        }

        public int Delete(Rezultat entity)
        {
            throw new NotImplementedException();
        }

        public int DeleteAll()
        {
            throw new NotImplementedException();
        }

        public int DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public bool ExistsById(string id)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                return ExistsById(id, connection);
            }
        }

        private bool ExistsById(string id, IDbConnection connection)
        {
            string query = "select * from rezultat where idr=:idr";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterUtil.AddParameter(command, "idr", DbType.String, 3);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "idr", id);
                return command.ExecuteScalar() != null;
            }
        }

        public IEnumerable<Rezultat> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rezultat> FindAllById(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public Rezultat FindById(string id)
        {
            throw new NotImplementedException();
        }

        public List<Rezultat> RezultatiVozaca(int idv)
        {
            string query = "select idr, idv, ids, sezona, plasman, bodovi, maksbrzina " +
                "from rezultat " +
                "where idv = :idv";

            List<Rezultat> result = new List<Rezultat>();

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "idv", DbType.Int32);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "idv", idv);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Rezultat rez = new Rezultat(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5),reader.GetDouble(6));
                            result.Add(rez);
                        }
                    }
                }

            }
            return result;
        }

        public int Save(Rezultat entity)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                return Save(entity, connection);
            }
        }

        private int Save(Rezultat rezultat, IDbConnection connection)
        {
            string insertSql = "insert into rezultat (idv, ids, sezona, plasman, bodovi, maksbrzina, idr) " +
                "values (:idv , :ids, :sezona, :plasman, :bodovi, :maksbrzina, :idr)";
            string updateSql = "update rezultat set idv=:idv, ids=:ids, sezona=:sezona " +
                "plasman=:plasman, bodovi=:bodovi, maksbrzina=:maksbrzina where idr=:idr";
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = ExistsById(rezultat.Idr, connection) ? updateSql : insertSql;
                ParameterUtil.AddParameter(command, "idv", DbType.Int32);
                ParameterUtil.AddParameter(command, "ids", DbType.Int32);
                ParameterUtil.AddParameter(command, "sezona", DbType.Int32);
                ParameterUtil.AddParameter(command, "plasman", DbType.Int32);
                ParameterUtil.AddParameter(command, "bodovi", DbType.Int32);
                ParameterUtil.AddParameter(command, "maksbrzina", DbType.Double);
                ParameterUtil.AddParameter(command, "idr", DbType.String, 3);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "idv", rezultat.Idv);
                ParameterUtil.SetParameterValue(command, "ids", rezultat.Ids);
                ParameterUtil.SetParameterValue(command, "sezona", rezultat.Sezona);
                ParameterUtil.SetParameterValue(command, "plasman", rezultat.Plasman);
                ParameterUtil.SetParameterValue(command, "bodovi", rezultat.Bodovi);
                ParameterUtil.SetParameterValue(command, "maksbrzina", rezultat.MaksBrzina);
                ParameterUtil.SetParameterValue(command, "idr", rezultat.Idr);
                return command.ExecuteNonQuery();
            }
        }

        public int SaveAll(IEnumerable<Rezultat> entities)
        {
            throw new NotImplementedException();
        }

        public List<Rezultat> TitulePoIdVozaca(IEnumerable<int> idvs)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select idr, idv, ids, sezona, plasman, bodovi, maksbrzina from rezultat where plasman = '1' and idv in (");
            int br = 0;
            foreach (int id in idvs)
            {
                sb.Append(":id" + br++ + ",");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")");

            List<Rezultat> rezultati = new List<Rezultat>();

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sb.ToString();
                    br = 0;
                    foreach (int id in idvs)
                    {
                        ParameterUtil.AddParameter(command, "id" + br++, DbType.Int32);
                    }
                    command.Prepare();
                    br = 0;
                    foreach (int id in idvs)
                    {
                        ParameterUtil.SetParameterValue(command, "id" + br++, id);
                    }
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Rezultat rez = new Rezultat(reader.GetString(0), reader.GetInt32(1),
                                reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5),reader.GetDouble(6));
                            rezultati.Add(rez);
                        }
                    }
                }
            }
            return rezultati;
        }

        public double ProsecnaMaksimalnaBrzinaPoStazi(int ids)
        {
            string query = "select avg(maksbrzina) " +
                "from rezultat " +
                "where ids = :ids";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "ids", DbType.Int32);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "ids", ids);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetDouble(0);
                        }
                    }
                }

            }
            return -1;
        }

        public List<PomocniDTO> PrvoplasiraniVozaciPoStazi(int ids)
        {
            string query = "select idv, sezona, bodovi " +
                "from rezultat " +
                "where plasman = '1' and ids = :ids " +
                "order by sezona desc";

            List<PomocniDTO> ret = new List<PomocniDTO>();

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "ids", DbType.Int32);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "ids", ids);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PomocniDTO pom = new PomocniDTO(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
                            ret.Add(pom);
                        }
                    }
                }

            }
            return ret;
        }
    }
}
