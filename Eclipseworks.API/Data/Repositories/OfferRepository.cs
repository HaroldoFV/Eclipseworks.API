using Eclipseworks.API.Data.Contexts;
using Eclipseworks.API.Domain.Interfaces;
using Eclipseworks.API.DTO.Offer;
using Eclipseworks.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eclipseworks.API.Data.Repositories;

public class OfferRepository : IOfferRepository
{
    public OfferRepository(EclipseworksContext eclipseworksContext)
    {
        _eclipseworksContext = eclipseworksContext;
    }

    private readonly EclipseworksContext _eclipseworksContext;


    public async Task<List<Offer>> ListOffersOfTheDay(int page, int pageSize)
    {
        var query = _eclipseworksContext
            .Offers
            .AsNoTracking()
            .Where(x => x.DateCreation.Date == DateTime.Today)
            .OrderByDescending(x => x.DateCreation);

        return await query
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<bool> HasBalanceInWallet(CreateOfferDTO dto)
    {
        var query = await _eclipseworksContext
            .Users
            .Include(x => x.Wallets)
            .ThenInclude(x => x.Coin)
            .FirstOrDefaultAsync(x => x.Id == dto.UserId);

        var wallet = query?.Wallets
            .FirstOrDefault(x => x.Id == dto.WalletId);

        return wallet != null
               && wallet.Coin.Saldo > 0
               && wallet.Coin.Saldo >= (dto.UnitPrice * dto.Quantity)
               && dto.Quantity == 1;
    }

    public async Task<bool> ReachedTheOfferLimit()
    {
        var count = await _eclipseworksContext
            .Offers
            .AsNoTracking()
            .Where(x => x.DateCreation.Date == DateTime.Today)
            .CountAsync();

        return count >= 5;
    }

    public async Task CreateOffer(Offer offer)
    {
        await _eclipseworksContext.Offers.AddAsync(offer);
        await _eclipseworksContext.SaveChangesAsync();
    }

    public async Task DeleteOffer(Offer offer)
    {
        _eclipseworksContext.Offers.Remove(offer);
        await _eclipseworksContext.SaveChangesAsync();
    }

    public async Task<Offer?> GetById(long id)
    {
        var query = await _eclipseworksContext
            .Offers
            .Include(x=>x.User)
            .FirstOrDefaultAsync(x => x.Id == id);

        return query;
    }
}