using Eclipseworks.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eclipseworks.API.Data.Mappings;

public class OfferMapping : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.ToTable("offer");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.UnitPrice).HasColumnName("unit_price");
        builder.Property(x => x.Quantity).HasColumnName("quantity");
        builder.Property(x => x.DateCreation).HasColumnName("date_creation");
        builder.Property(x => x.UserId).HasColumnName("user_account_id");
        builder.Property(x => x.WalletId).HasColumnName("wallet_id");
        builder.Property(x => x.IsDeleted).HasColumnName("is_deleted");
        
        builder.HasOne(x => x.User)
            .WithMany(x => x.Offers)
            .HasForeignKey(x => x.UserId);
        
        builder.HasOne(x => x.Wallet)
            .WithMany(x => x.Offers)
            .HasForeignKey(x => x.WalletId);
    }
}