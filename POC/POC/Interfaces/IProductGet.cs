using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace POC.Interfaces
{
    interface IProductGet
    {
        Task<Product[]> GetAllProductsAsync();
    }
}
