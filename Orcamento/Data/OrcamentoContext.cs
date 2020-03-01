using Microsoft.EntityFrameworkCore;

namespace Orcamento.Models
{
    public class OrcamentoContext: DbContext
    {
        public OrcamentoContext(DbContextOptions<OrcamentoContext> options)
            : base(options)
        {
        }

        public DbSet<Materiais> Materiais { get; set; }
        public DbSet<Empresa> Empresa { get; set; }


    }
}
