using Eclipseworks.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eclipseworks.API.Data.Mappings;

public class CoinMapping : IEntityTypeConfiguration<Coin>
{
    public void Configure(EntityTypeBuilder<Coin> builder)
    {
        builder.ToTable("coin");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Token).HasColumnName("token");
        builder.Property(x => x.Saldo).HasColumnName("saldo");
        builder.Property(x => x.Type).HasColumnName("type");
    }
}