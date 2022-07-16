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
    public class StazaDAOImpl : IStazaDAO
    {
        public int Count()
        {
            string query = "select count(*) from staza";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public int Delete(Staza entity)
        {
            return DeleteById(entity.Ids);
        }

        public int DeleteAll()
        {
            string query = "delete from staza";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();
                    return command.ExecuteNonQuery();
                }
            }
        }

        public int DeleteById(int id)
        {
            string query = "delete from staza where ids=:ids";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "ids", DbType.Int32);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "ids", id);
                    return command.ExecuteNonQuery();
                }
            }
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
            string query = "select * from staza where ids=:ids";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterUtil.AddParameter(command, "ids", DbType.Int32);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "ids", id);
                return command.ExecuteScalar() != null;
            }
        }

        public IEnumerable<Staza> FindAll()
        {
            string query = "select ids, nazivs, brojkrug, duzkrug, drzs from staza";
            List<Staza> staze = new List<Staza>();

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
                            Staza staza = new Staza(reader.GetInt32(0), reader.GetString(1),
                                reader.GetInt32(2), reader.GetDouble(3), reader.GetInt32(4));
                            staze.Add(staza);
                        }
                    }
                }
            }
            return staze;
        }

        public IEnumerable<Staza> FindAllById(IEnumerable<int> ids)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select ids, nazivs, brojkrug, duzkrug, drzs from staza where ids in (");
            foreach (int id in ids)
            {
                sb.Append(":id" + id + ",");
            }
            sb.Remove(sb.Length - 1, 1); 
            sb.Append(")");

            List<Staza> staze = new List<Staza>();

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sb.ToString();
                    foreach (int id in ids)
                    {
                        ParameterUtil.AddParameter(command, "id" + id, DbType.Int32);
                    }
                    command.Prepare();

                    foreach (int id in ids)
                    {
                        ParameterUtil.SetParameterValue(command, "id" + id, id);
                    }
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Staza staza = new Staza(reader.GetInt32(0), reader.GetString(1),
                                reader.GetInt32(2), reader.GetDouble(3), reader.GetInt32(4));
                            staze.Add(staza);
                        }
                    }
                }
            }
            return staze;
        }

        public Staza FindById(int id)
        {
            string query = "select ids, nazivs, brojkrug, duzkrug, drzs " +
                        "from staza where ids = :ids";
            Staza staza = null;

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "ids", DbType.Int32);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "ids", id);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            staza = new Staza(reader.GetInt32(0), reader.GetString(1),
                                reader.GetInt32(2), reader.GetDouble(3), reader.GetInt32(4));
                        }
                    }
                }
            }
            return staza;
        }

        public int Save(Staza entity)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                return Save(entity, connection);
            }
        }

        private int Save(Staza staza, IDbConnection connection)
        {
            string insertSql = "insert into staza (nazivs, brojkrug, duzkrug, drzs, ids) " +
                "values (:nazivs , :brojkrug, :duzkrug, :drzs, :ids)";
            string updateSql = "update staza set nazivs=:nazivs, brojkrug=:brojkrug, " +
                "duzkrug=:duzkrug, drzs=:drzs where ids=:ids";
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = ExistsById(staza.Ids, connection) ? updateSql : insertSql;
                ParameterUtil.AddParameter(command, "nazivs", DbType.String, 128);
                ParameterUtil.AddParameter(command, "brojkrug", DbType.Int32);
                ParameterUtil.AddParameter(command, "duzkrug", DbType.Double);
                ParameterUtil.AddParameter(command, "drzs", DbType.Int32);
                ParameterUtil.AddParameter(command, "ids", DbType.Int32);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "nazivs", staza.Nazivs);
                ParameterUtil.SetParameterValue(command, "brojkrug", staza.BrojKrug);
                ParameterUtil.SetParameterValue(command, "duzkrug", staza.DuzKrug);
                ParameterUtil.SetParameterValue(command, "drzs", staza.Drzs);
                ParameterUtil.SetParameterValue(command, "ids", staza.Ids);
                return command.ExecuteNonQuery();
            }
        }

        public int SaveAll(IEnumerable<Staza> entities)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();

                int numSaved = 0;

                
                foreach (Staza entity in entities)
                {
                    numSaved += Save(entity, connection);
                }

                transaction.Commit();

                return numSaved;
            }
        }

        public List<Staza> NadjiStazePoIdDrzave(int idd)
        {
            string query = "select ids, nazivs, brojkrug, duzkrug, drzs " +
                        "from staza where drzs = :drzs";
            List<Staza> staze = new List<Staza>();

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "drzs", DbType.Int32);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "drzs", idd);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Staza staza = new Staza(reader.GetInt32(0), reader.GetString(1),
                                reader.GetInt32(2), reader.GetDouble(3), reader.GetInt32(4));
                            staze.Add(staza);
                        }
                    }
                }
            }
            return staze;
        }
    }
}
