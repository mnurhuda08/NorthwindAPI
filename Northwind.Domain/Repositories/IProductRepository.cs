using Northwind.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> FindAllProduct();

        Task<IEnumerable<Product>> FindAllProductAsync();

        Product FindProductByID(int productID);

        void Insert(Product product);

        void Edit(Product product);

        void Remove(Product product);
    }
}