using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1W1LORENZOCOBOSROBERTNADAMAS.Domain;
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

        public bool ExecuteTransaction(Invoice invoice)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Resources.CadenaConexionLocal))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // 1. Guardar la cabecera de la factura
                    var cmdFactura = new SqlCommand("sp_Factura_Save", connection, transaction);
                    cmdFactura.CommandType = CommandType.StoredProcedure;

                    // Parámetros de la factura
                    cmdFactura.Parameters.AddWithValue("@Fecha", invoice.Date);
                    cmdFactura.Parameters.AddWithValue("@Cliente", invoice.Client);
                    cmdFactura.Parameters.AddWithValue("@Id_FormaPago", invoice.PayType.Id);

                    // Si es nueva factura, mandamos NULL
                    if (invoice.InvoiceNo > 0)
                        cmdFactura.Parameters.AddWithValue("@Id_Factura", invoice.InvoiceNo);
                    else
                        cmdFactura.Parameters.AddWithValue("@Id_Factura", DBNull.Value);

                    // IMPORTANTE: el SP ahora devuelve el Id generado o el mismo si se actualiza
                    int idFactura = Convert.ToInt32(cmdFactura.ExecuteScalar());

                    // 2. Guardar los detalles de la factura
                    foreach (var detalle in invoice.Detail)
                    {
                        var cmdDetalle = new SqlCommand("SP_GUARDAR_DETALLE_FACTURA", connection, transaction);
                        cmdDetalle.CommandType = CommandType.StoredProcedure;

                        cmdDetalle.Parameters.AddWithValue("@Id_Factura", idFactura);
                        cmdDetalle.Parameters.AddWithValue("@Id_Articulo", detalle.Product.IdProduct); // solo Id necesario
                        cmdDetalle.Parameters.AddWithValue("@Cantidad", detalle.Quantity);

                        cmdDetalle.ExecuteNonQuery();
                    }

                    // 3. Confirmamos la transacción
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    // Si algo falla, se deshace todo
                    transaction.Rollback();
                    return false;
                }
            }
        }

        


        internal bool ExecuteTransaction(Product product)
        {
            throw new NotImplementedException();
        }
    }


}



