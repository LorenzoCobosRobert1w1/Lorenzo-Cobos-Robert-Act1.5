using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1W1LORENZOCOBOSROBERTNADAMAS.Domain
{
    public class Invoice
    {
        public int InvoiceNo { get; set; }
        public DateTime Date { get; set; }
        public string Client { get; set; }
        public PaymentMethod PayType { get; set; }
        public List<InvoiceDetail> Detail { get; } = new List<InvoiceDetail>();

        public void AddDetail(InvoiceDetail detalle)
        {
            Detail.Add(detalle);
        }

        public double CalcularTotal()
        {
            double total = 0;
            foreach (var detalle in Detail)
            {
                total += detalle.CalcularSubtotal();
            }
            return total;
        }

        public override string ToString()
        {
            return "InvoiceNo: " + InvoiceNo + ", Date: " + Date.ToShortDateString() + ", Client: " + Client +
                   ", PayType: " + PayType + ", Total: " + CalcularTotal();
        }
    }
}
