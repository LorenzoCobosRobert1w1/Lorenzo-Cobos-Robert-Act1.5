using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1W1LORENZOCOBOSROBERTNADAMAS.Domain;

namespace _1W1LORENZOCOBOSROBERTNADAMAS.Services
{
   public interface IInvoiceService
    {
        List<Invoice> GetInvoice();
        Invoice? GetInvoiceById(int id);
        bool SaveInvoice(Invoice invoice);
        bool DeleteInvoice(int id);
    }
}
