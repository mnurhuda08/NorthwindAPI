using Northwind.Domain.Base;
using Northwind.Domain.Repositories;
using Northwind.Persistence.Repositories;
using Northwind.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Persistence.Base
{
    public class RepositoryManager : IRepositoryManager
    {
        private AdoDbContext _adoContext;
        private RegionRepository _regionRepository;

        private ProductRepository _productRepository;

        public RepositoryManager(AdoDbContext adoContext)
        {
            _adoContext = adoContext;
        }

        public IRegionRepository RegionRepository
        {
            get
            {
                if (_regionRepository == null)
                {
                    _regionRepository = new RegionRepository(_adoContext);
                }
                return _regionRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_adoContext);
                }
                return _productRepository;
            }
        }

        /*public IRegionRepository RegionRepository => throw new NotImplementedException();*/
    }
}