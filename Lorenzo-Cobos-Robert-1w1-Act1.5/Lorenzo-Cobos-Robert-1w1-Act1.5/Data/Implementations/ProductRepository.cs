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
            throw new NotImplementedException();
        }

     

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save(Product product)
        {
            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter("@codigo", product.IdProduct),
                new SpParameter("@nombre", product.Name),
                new SpParameter("@precio", product.UnitPrice),          
                new SpParameter("@stock", product.Stock),
                new SpParameter("@activo", product.Active ? 1 : 0)  
            };

            return DataHelper.GetInstance().ExecuteSpDml("SP_GUARDAR_PRODUCTO", param);
        }

        public List<Product> GetAll()
        {
            List<Product> lst = new List<Product>();

            // Traer registros de la BD
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_PRODUCTOS");

            // Mapear cada DataRow a un Product
            foreach (DataRow row in dt.Rows)
            {
                Product p = new Product();
                p.IdProduct = (int)row["codigo"];
                p.Name = (string)row["n_producto"];
                p.Stock = (int)row["stock"];
                p.Active = ((int)row["esta_activo"]) == 1;


                // Manejamos el valor nulo para el precio
                if (row["precio"] != DBNull.Value)
                {
                    p.UnitPrice = (decimal)row["precio"];
                }
                // Si es nulo, agregamos un 0
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
