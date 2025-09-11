using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1W1LORENZOCOBOSROBERTNADAMAS.Data.Helper;
using _1W1LORENZOCOBOSROBERTNADAMAS.Data.Interfaces;
using _1W1LORENZOCOBOSROBERTNADAMAS.Domain;
using Microsoft.Data.SqlClient;

namespace _1W1LORENZOCOBOSROBERTNADAMAS.Data.Implementations
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public bool Delete(int id)
        {
            List<SpParameter> param = new List<SpParameter>
            {
                new SpParameter("@Id_Factura", id)
            };

            return DataHelper.GetInstance().ExecuteSpDml("sp_Factura_Delete", param);
        }

        public List<Invoice> GetAll()
        {
            List<Invoice> lst = new List<Invoice>();

            var dt = DataHelper.GetInstance().ExecuteSPQuery("sp_Factura_GetAll");

            foreach (DataRow row in dt.Rows)
            {
                Invoice I = new Invoice
                {
                    InvoiceNo = (int)row["Id_Factura"],
                    Date = (DateTime)row["Fecha"],
                    Client = (string)row["Cliente"],
                    PayType = new PaymentMethod
                    {
                        Name = (string)row["FormaPago"]
                    }
                };

                lst.Add(I);
            }

            return lst;
        }

        public Invoice? GetById(int id)
        {
            List<SpParameter> param = new List<SpParameter>
            {
                new SpParameter("@Id_Factura", id)
            };

            var dt = DataHelper.GetInstance().ExecuteSPQuery("sp_Factura_GetById", param);

            if (dt != null && dt.Rows.Count > 0)
            {
                Invoice f = new Invoice
                {
                    InvoiceNo = (int)dt.Rows[0]["Id_Factura"],
                    Date = (DateTime)dt.Rows[0]["Fecha"],
                    Client = (string)dt.Rows[0]["Cliente"],
                    PayType = new PaymentMethod
                    {
                        Name = (string)dt.Rows[0]["FormaPago"]
                    }
                };

                return f;
            }

            return null;
        }

        public bool Save(Invoice factura)
        {
            return DataHelper.GetInstance().ExecuteTransaction(factura);
        }

    }
}
