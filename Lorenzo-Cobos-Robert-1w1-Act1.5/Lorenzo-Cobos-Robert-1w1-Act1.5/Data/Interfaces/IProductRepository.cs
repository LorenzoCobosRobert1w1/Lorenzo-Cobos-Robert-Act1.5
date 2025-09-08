using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1W1LORENZOCOBOSROBERTNADAMAS.Domain;

namespace _1W1LORENZOCOBOSROBERTNADAMAS.Data.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product GetById(int id);
        bool Save(Product product);
        bool Delete(int id);
    }
}
