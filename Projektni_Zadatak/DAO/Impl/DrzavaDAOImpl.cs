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
    class DrzavaDAOImpl : IDrzavaDAO
    {
        public int Count()
        {
            throw new NotImplementedException();
        }

        public int Delete(Drzava entity)
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
            throw new NotImplementedException();
        }

        public IEnumerable<Drzava> FindAll()
        {
            string query = "select idd, nazivd from drzava";
            List<Drzava> drzave = new List<Drzava>();

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Drzava drzava = new Drzava(reader.GetInt32(0), reader.GetString(1));
                            drzave.Add(drzava);
                        }
                    }
                }
            }
            return drzave;
        }

        public IEnumerable<Drzava> FindAllById(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Drzava FindById(int id)
        {
            string query = "select idd, nazivd " +
                        "from drzava where idd = :idd";
            Drzava drzava = null;

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "idd", DbType.Int32);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "idd", id);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            drzava = new Drzava(reader.GetInt32(0), reader.GetString(1));
                        }
                    }
                }
            }
            return drzava;
        }

        public int Save(Drzava entity)
        {
            throw new NotImplementedException();
        }

        public int SaveAll(IEnumerable<Drzava> entities)
        {
            throw new NotImplementedException();
        }
    }
}
