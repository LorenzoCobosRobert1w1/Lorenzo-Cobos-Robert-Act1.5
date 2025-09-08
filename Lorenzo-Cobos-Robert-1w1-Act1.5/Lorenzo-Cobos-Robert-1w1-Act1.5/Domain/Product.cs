using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1W1LORENZOCOBOSROBERTNADAMAS.Domain
{
    public class Product
    {
        public int IdProduct { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int Stock { get; internal set; }
        public bool Active { get; internal set; }

        public override string ToString()
        {
            return  " " + IdProduct + " " + Name + " $ " + UnitPrice;
        }
    }
}
