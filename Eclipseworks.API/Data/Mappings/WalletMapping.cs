using Eclipseworks.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eclipseworks.API.Data.Mappings;

public class WalletMapping : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("wallet");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.CoinId).HasColumnName("coin_id");
        builder.Property(x => x.UserId).HasColumnName("user_id");

        builder.HasOne(x => x.Coin)
            .WithMany(x => x.Wallets)
            .HasForeignKey(x => x.CoinId);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Wallets)
            .HasForeignKey(x => x.UserId);
    }
}