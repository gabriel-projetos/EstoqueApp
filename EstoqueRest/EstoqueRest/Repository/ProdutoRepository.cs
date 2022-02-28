using EstoqueRest.Data;
using EstoqueRest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueRest.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly Context context;

        public ProdutoRepository(Context context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Produto>> GetProdutos()
        {
            var produtos = await context.Produtos.AsNoTracking().ToListAsync();
            return produtos;
        }

        public async Task AddProduto(Produto produto)
        {
            await context.AddAsync(produto);
            await context.SaveChangesAsync();
        }

        public async Task UpdateProduto(Produto produto)
        {
            //produto.Id = id;
            context.Produtos.Update(produto);
            await context.SaveChangesAsync();
        }
        
        public async Task DeleteProduto(int id)
        {
            var produto = await context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            context.Produtos.Remove(produto);
            await context.SaveChangesAsync();
        }

    }
}
