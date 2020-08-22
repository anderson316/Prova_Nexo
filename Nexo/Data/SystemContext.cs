using Microsoft.EntityFrameworkCore;
using Nexo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nexo.Data
{
    public class SystemContext : DbContext
    {
        public SystemContext (DbContextOptions<SystemContext> options) : base (options)
        {
        }
        public DbSet<FornecedorModel> Fornecedores { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
    }
}
