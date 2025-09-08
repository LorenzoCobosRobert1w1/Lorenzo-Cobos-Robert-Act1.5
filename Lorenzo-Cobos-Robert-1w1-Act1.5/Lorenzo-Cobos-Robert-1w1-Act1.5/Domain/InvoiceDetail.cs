using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1W1LORENZOCOBOSROBERTNADAMAS.Domain
{
    public class InvoiceDetail
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public double CalcularSubtotal()
        {
            return (double)(Product.UnitPrice * Quantity);
        }

        public override string ToString()
        {
            return "Product: [" + Product + "], Quantity: " + Quantity + ", Subtotal: " + CalcularSubtotal();
        }
    }
}
