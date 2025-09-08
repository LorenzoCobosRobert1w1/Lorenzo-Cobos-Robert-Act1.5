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
                // Abrimos la conexión
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;

                // Agregamos parámetros si los hay
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
                // En caso de error, "catchamos" el error y retornamos null
                throw ex;
                dt = null;
            }
            finally
            {
                // Cerramos la conexión
                _connection.Close();
            }

            return dt;
        }

        // Método para ejecutar SPs con operaciones DML
        public bool ExecuteSpDml(string sp, List<SpParameter>? param = null)
        {
            bool result;
            try
            {
                // Abrimos la conexión
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Agregamos parámetros si los hay
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
                // En caso de error, retornamos false
                result = false;
            }
            finally
            {
                // Cerramos la conexión
                _connection.Close();
            }

            return result;
        }
    }
}
