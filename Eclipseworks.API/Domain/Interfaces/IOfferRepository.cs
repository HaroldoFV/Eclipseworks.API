using Eclipseworks.API.DTO.Offer;
using Eclipseworks.API.Entities;

namespace Eclipseworks.API.Domain.Interfaces;

public interface IOfferRepository
{
    Task<List<Offer>> ListOffersOfTheDay(int page, int pageSize);
    Task CreateOffer(Offer offer);
    Task DeleteOffer(Offer offer);
    Task<bool> HasBalanceInWallet(CreateOfferDTO dto);
    Task<bool> ReachedTheOfferLimit();
    Task<Offer?> GetById(long id);
}