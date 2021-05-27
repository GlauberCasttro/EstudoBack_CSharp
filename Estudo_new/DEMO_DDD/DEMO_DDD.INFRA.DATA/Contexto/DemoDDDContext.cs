using DEMO_DDD.DOMAIN.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMO_DDD.INFRA.DATA.Contexto
{
    public class DemoDDDContext : DbContext
    {
        public DemoDDDContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DemoDDDContext).Assembly);

            //caso esqueca de mapear um campo da sua entidade e o mesmo na entre como valor maximo no banco.
            //setando como padrao varchar(100) para campos do tipo string
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");


         

            //aplicando as configurações acima para campos varchar
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DemoDDDContext).Assembly);


            //desabilitando o cascade delete que por padrao do entity é do tipo cascade
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);


        }

        /// <summary>
        /// Configrando a data cadastro pra sempre setar data atual ao adicionar um registro novo Added
        /// </summary>
        /// <returns></returns>
        public override async  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.GetType().GetProperty("DataCadastro") == null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return await base.SaveChangesAsync();
        }

    }
}
