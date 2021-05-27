using DEMO_DDD.DOMAIN.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEMO_DDD.INFRA.DATA.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(e => e.Id).HasColumnName("ProdutoId");
            builder.Property(p => p.Nome).HasColumnType("varchar(250)").IsRequired();
            builder.Property(p => p.Valor).HasColumnType("decimal").IsRequired();
            builder.Property(c => c.DataCadastro).HasColumnType("datetime").IsRequired();

            builder.ToTable("Produtos");

            //Referencia 1:N
            

        }
    }
}
