using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1W1LORENZOCOBOSROBERTNADAMAS.Data.Implementations;
using _1W1LORENZOCOBOSROBERTNADAMAS.Data.Interfaces;
using _1W1LORENZOCOBOSROBERTNADAMAS.Domain;

namespace _1W1LORENZOCOBOSROBERTNADAMAS.Services
{
    internal class InvoiceService
    {

        private IInvoiceRepository _repository;
        public InvoiceService()
        {
            _repository = new InvoiceRepository();
        }

        public List<Invoice> GetInvoice()
        {
            return _repository.GetAll();
        }

        public bool SaveInvoice(Invoice invoice)
        {
            return _repository.Save(invoice);
        }

        public bool DeleteInvoice(int id)
        {
            return _repository.Delete(id);
        }
    }
}
