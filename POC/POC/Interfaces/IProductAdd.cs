using System.Threading.Tasks;

namespace POC
{
    interface IProductAdd
    {
        Task<bool> CreateProductAsync(Product product);

    }
}