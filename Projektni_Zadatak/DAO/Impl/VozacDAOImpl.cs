using Projektni_Zadatak.Connection;
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
    public class VozacDAOImpl : IVozacDAO
    {
        public int Count()
        {
            throw new NotImplementedException();
        }

        public int Delete(Vozac entity)
        {
            throw new NotImplementedException();
        }

        public int DeleteAll()
        {
            throw new NotImplementedException();
        }

        public int DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public bool ExistsById(int id)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                return ExistsById(id, connection);
            }
        }

        private bool ExistsById(int id, IDbConnection connection)
        {
            string query = "select * from vozac where idv=:idv";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterUtil.AddParameter(command, "idv", DbType.Int32);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "idv", id);
                return command.ExecuteScalar() != null;
            }
        }

        public IEnumerable<Vozac> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vozac> FindAllById(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Vozac FindById(int id)
        {
            string query = "select idv, imev, prezv, godrodj, brojtit, nvl(drzv,0) " +
                        "from vozac where idv = :idv";
            Vozac vozac = null;

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "idv", DbType.Int32);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "idv", id);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            vozac = new Vozac(reader.GetInt32(0), reader.GetString(1),
                                reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5));
                        }
                    }
                }
            }
            return vozac;
        }

        public List<Vozac> NadjiVozacePoIdDrzave(int idd)
        {
            string query = "select idv, imev, prezv, godrodj, brojtit, nvl(drzv,0) " +
                        "from vozac where drzv = :drzv";
            List<Vozac> vozaci = new List<Vozac>();

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "drzv", DbType.Int32);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "drzv", idd);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Vozac vozac = new Vozac(reader.GetInt32(0), reader.GetString(1),
                                reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5));
                            vozaci.Add(vozac);
                        }
                    }
                }
            }
            return vozaci;
        }

        public int Save(Vozac entity)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                return Save(entity, connection);
            }
        }

        private int Save(Vozac vozac, IDbConnection connection)
        {
            string insertSql = "insert into vozac (imev, prezv, godrodj, brojtit, drzv, idv) " +
                "values (:imev , :prezv, :godrodj, :brojtit, :drzv, :idv)";
            string updateSql = "update vozac set imev=:imev, prezv=:prezv, " +
                "godrodj=:godrodj, brojtit=:brojtit, drzv=:drzv where idv=:idv";
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = ExistsById(vozac.Idv, connection) ? updateSql : insertSql;
                ParameterUtil.AddParameter(command, "imev", DbType.String, 128);
                ParameterUtil.AddParameter(command, "prezv", DbType.String, 128);
                ParameterUtil.AddParameter(command, "godrodj", DbType.Int32);
                ParameterUtil.AddParameter(command, "brojtit", DbType.Int32);
                ParameterUtil.AddParameter(command, "drzv", DbType.Int32);
                ParameterUtil.AddParameter(command, "idv", DbType.Int32);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "imev", vozac.ImeV);
                ParameterUtil.SetParameterValue(command, "prezv", vozac.PrezV);
                ParameterUtil.SetParameterValue(command, "godrodj", vozac.GodRodj);
                ParameterUtil.SetParameterValue(command, "brojtit", vozac.BrojTit);
                ParameterUtil.SetParameterValue(command, "drzv", vozac.Drzv);
                ParameterUtil.SetParameterValue(command, "idv", vozac.Idv);
                return command.ExecuteNonQuery();
            }
        }

        public int SaveAll(IEnumerable<Vozac> entities)
        {
            throw new NotImplementedException();
        }
    }
}
