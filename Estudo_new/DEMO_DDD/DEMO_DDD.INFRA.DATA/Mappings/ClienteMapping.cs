using DEMO_DDD.DOMAIN.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEMO_DDD.INFRA.DATA.Mappings
{

    /// <summary>
    /// Utilizando Fluent Api para configurar a base de dados corretamente
    /// </summary>
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(e => e.Id).HasColumnName("ClienteId");

            builder.Property(c => c.Nome)
                .HasColumnType("varchar(200)")
                 .IsRequired();
            builder.Property(c => c.Email)
                .HasColumnType("varchar(200)")
                 .IsRequired();
            builder.Property(c => c.DataCadastro)
           .HasColumnType("datetime")
            .IsRequired();

            //Configurando relação 1:N
            builder.HasMany(p => p.Produtos).WithOne(c => c.Cliente).HasForeignKey(p => p.ClienteId);
            builder.ToTable("Clientes");
        }
    }
}
