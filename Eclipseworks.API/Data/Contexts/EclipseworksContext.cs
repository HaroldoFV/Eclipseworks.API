using Eclipseworks.API.Entities;
using Microsoft.EntityFrameworkCore;


namespace Eclipseworks.API.Data.Contexts;

public class EclipseworksContext : DbContext
{
    public EclipseworksContext(DbContextOptions<EclipseworksContext> options) : base(options)
    {
    }

    #region DBSets

    public DbSet<User> Users { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Coin> Coins { get; set; }
    public DbSet<Offer> Offers { get; set; }

    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EclipseworksContext).Assembly);
        modelBuilder.Entity<Offer>().HasQueryFilter(p => !p.IsDeleted);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var item in ChangeTracker.Entries()
                     .Where(e => e.State == EntityState.Deleted &&
                                 e.Metadata.GetProperties().Any(x => x.Name == "IsDeleted")))
        {
            item.State = EntityState.Unchanged;
            item.CurrentValues["IsDeleted"] = true;
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        //Soft-Delete
        foreach (var item in ChangeTracker.Entries()
                     .Where(e => e.State == EntityState.Deleted &&
                                 e.Metadata.GetProperties().Any(x => x.Name == "IsDeleted")))
        {
            item.State = EntityState.Unchanged;
            item.CurrentValues["IsDeleted"] = true;
        }

        return base.SaveChanges();
    }
}