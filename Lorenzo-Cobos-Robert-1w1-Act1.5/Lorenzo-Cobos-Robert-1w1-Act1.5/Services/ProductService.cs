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
   public class ProductService
    {
        private IProductRepository _repository;
        public ProductService()
        {
            _repository = new ProductRepository();
        }

        public List<Product> GetProducts()
        {
         
            return _repository.GetAll();
        }

        public Product? GetProductById(int id)
        {
            return _repository.GetById(id);
        }

        public bool SaveProduct(Product product)
        {
           

            return _repository.Save(product);
        }

        public bool DeleteProduct(int id)
        {
    
            return _repository.Delete(id);
        }
   
    }
}
