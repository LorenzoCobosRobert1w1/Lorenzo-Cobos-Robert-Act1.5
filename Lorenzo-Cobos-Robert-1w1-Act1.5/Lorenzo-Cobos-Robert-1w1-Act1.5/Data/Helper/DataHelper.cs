using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace _1W1LORENZOCOBOSROBERTNADAMAS.Data.Helper
{
    public class DataHelper
    {
        private static DataHelper _instance;
        private SqlConnection _connection;

        private DataHelper()
        {
            _connection = new SqlConnection(Properties.Resources.CadenaConexionLocal);
        }
        public static DataHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }

        public DataTable ExecuteSPQuery(string sp, List<SpParameter>? param = null)
        {
            DataTable dt = new DataTable();
            try
            {
             
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;

             
                if (param != null)
                {
                    foreach (SpParameter p in param)
                    {
                        cmd.Parameters.AddWithValue(p.Name, p.Valor);
                    }
                }

                dt.Load(cmd.ExecuteReader());
            }
            catch (SqlException ex)
            {
          
                throw ex;
                dt = null;
            }
            finally
            {
                _connection.Close();
            }

            return dt;
        }

                         
        public bool ExecuteSpDml(string sp, List<SpParameter>? param = null)
        {
            bool result;
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;

                if (param != null)
                {
                    foreach (SpParameter p in param)
                    {
                        cmd.Parameters.AddWithValue(p.Name, p.Valor);
                    }
                }

                int affectedRows = cmd.ExecuteNonQuery();

                result = affectedRows > 0;
            }
            catch (SqlException ex)
            {
              
                result = false;
            }
            finally
            {
               
                _connection.Close();
            }

            return result;
        }

        public int ExecuteSPDml(string sp, List<SqlParameter> parametros = null)
        {
            int filasAfectadas = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection (Properties.Resources.CadenaConexionLocal))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (parametros != null)
                        {
                            foreach (SqlParameter p in parametros)
                                cmd.Parameters.Add(p);
                        }

                        filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                filasAfectadas = -1;
                // Log opcional
            }
            return filasAfectadas;
        }
    }
}
