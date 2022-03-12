using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace POC.Interfaces
{
    interface IProductDelete
    {
        Task<bool> DeleteProductAsync(String ProductID);
    }
}
