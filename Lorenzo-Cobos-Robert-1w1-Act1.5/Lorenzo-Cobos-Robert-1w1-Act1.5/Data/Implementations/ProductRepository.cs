using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1W1LORENZOCOBOSROBERTNADAMAS.Data.Helper;
using _1W1LORENZOCOBOSROBERTNADAMAS.Data.Interfaces;
using _1W1LORENZOCOBOSROBERTNADAMAS.Domain;

namespace _1W1LORENZOCOBOSROBERTNADAMAS.Data.Implementations
{
    internal class ProductRepository : IProductRepository
    {
        public bool Delete(int id)
        {
            try
            {
                List<SpParameter> param = new List<SpParameter>()
        {
            new SpParameter() { Name = "@codigo", Valor = id }
        };

                DataHelper.GetInstance().ExecuteSpDml("SP_BAJA_ARTICULO", param);
                return true; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Delete: {ex.Message}");
                return false; 
            }
        }




        public Product GetById(int id)
        {
            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter()
                {
                    Name = "@codigo",
                    Valor = id
                }
            };

           
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_PRODUCTO_POR_CODIGO", param);

            
            if (dt != null && dt.Rows.Count > 0)
            {
                Product p = new Product()
                {
                    IdProduct = (int)dt.Rows[0]["codigo"],
                    Name = (string)dt.Rows[0]["n_producto"],
                    UnitPrice = (decimal)dt.Rows[0]["precio"],
                   
                };

                return p;
            }

            return null;
        }

        public bool Save(Product product)
        {
            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter("@codigo", product.IdProduct),
                new SpParameter("@nombre", product.Name),
                new SpParameter("@precio", product.UnitPrice),          
                
            };

            return DataHelper.GetInstance().ExecuteSpDml("SP_GUARDAR_ARTICULO", param);
        }

        public List<Product> GetAll()
        {
            List<Product> lst = new List<Product>();

           
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_PRODUCTOS");

     
            foreach (DataRow row in dt.Rows)
            {
                Product p = new Product();
                p.IdProduct = (int)row["codigo"];
                p.Name = (string)row["n_producto"];
              


               
                if (row["precio"] != DBNull.Value)
                {
                    p.UnitPrice = (decimal)row["precio"];
                }
              
                else
                {
                    p.UnitPrice = 0m;
                }
                lst.Add(p);
            }

            return lst;
        }



    }
}
