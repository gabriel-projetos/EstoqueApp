using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IProduto
    {
        Task<IEnumerable<Produto>> GetProducts();
        Task DeleteProducts(int id);
        Task UpdateProducts(Produto produto);
        Task CreateProduct(Produto produto);
    }
}
    