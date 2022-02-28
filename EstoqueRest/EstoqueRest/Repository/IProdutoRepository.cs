using EstoqueRest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueRest.Repository
{
    public interface IProdutoRepository
    {
        public Task<IEnumerable<Produto>> GetProdutos();
        public Task AddProduto(Produto produto);
        public Task UpdateProduto(Produto produto);
        public Task DeleteProduto(int id);
    }
}
