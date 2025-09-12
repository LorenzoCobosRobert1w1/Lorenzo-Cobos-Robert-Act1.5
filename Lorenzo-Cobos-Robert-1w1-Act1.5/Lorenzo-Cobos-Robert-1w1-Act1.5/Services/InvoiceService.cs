using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1W1LORENZOCOBOSROBERTNADAMAS.Data.Implementations;
using _1W1LORENZOCOBOSROBERTNADAMAS.Data.Interfaces;
using _1W1LORENZOCOBOSROBERTNADAMAS.Domain;


using _1W1LORENZOCOBOSROBERTNADAMAS.Data.Helper;

namespace _1W1LORENZOCOBOSROBERTNADAMAS.Services
{
   public class InvoiceService : IInvoiceService
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

         public bool ExecuteTransaction(Product product)
        {
            return DataHelper.GetInstance().ExecuteTransaction(product);
        }


        internal bool ExecuteTransaction(Invoice invoiceWithDetails)
        {
            return DataHelper.GetInstance().ExecuteTransaction(invoiceWithDetails);
        }

       

        public Invoice? GetInvoiceById(int id)
        {
           return  _repository.GetById(id); 
        }
    }
}
