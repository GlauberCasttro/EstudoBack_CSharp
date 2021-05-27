using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Data
{
    public class MeuDbContext : DbContext
    {
        public MeuDbContext(DbContextOptions options)
        :base (options)
        {

        }

        public DbSet<Aluno> Alunos { get; set; }
    }
}
