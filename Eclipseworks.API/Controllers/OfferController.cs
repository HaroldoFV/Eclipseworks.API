using Eclipseworks.API.Domain.Interfaces;
using Eclipseworks.API.DTO.Offer;
using Eclipseworks.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Eclipseworks.API.Controllers;

[ApiController]
[Route("api/offers")]
public class OfferController : ControllerBase
{
    private readonly IOfferRepository _OfferRepository;

    public OfferController(IOfferRepository offerRepository)
    {
        _OfferRepository = offerRepository;
    }

    [HttpGet]
    public async Task<IActionResult> ListOffersOfTheDay(int page, int pageSize)
    {
        var query = await _OfferRepository.ListOffersOfTheDay(page, pageSize);

        var resultList = new List<object>();

        foreach (var item in query)
        {
            var result = new
            {
                Id = item.Id,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity,
                DateCreation = item.DateCreation.ToString("dd/MM/yyyy HH:mm:ss")
            };
            resultList.Add(result);
        }

        return Ok(resultList);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOffer(CreateOfferDTO offerDto)
    {
        if (await _OfferRepository.ReachedTheOfferLimit())
            return BadRequest("É possível criar no máximo 5 ofertas por dia.");

        var hasBalanceInWallet = await _OfferRepository.HasBalanceInWallet(offerDto);

        if (!hasBalanceInWallet) return BadRequest("saldo inválido.");

        var offer = new Offer(offerDto.UnitPrice, offerDto.Quantity, offerDto.UserId, offerDto.WalletId);

        await _OfferRepository.CreateOffer(offer);

        return Ok(offer.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOffer(long id, [FromHeader(Name = "user")] string user)
    {
        var offer = await _OfferRepository.GetById(id);

        if (offer is null) return NotFound("Oferta inválida.");

        if (user != offer?.UserId.ToString()) return BadRequest("Somente o criador de uma oferta pode deletá-la.");

        await _OfferRepository.DeleteOffer(offer);

        return NoContent();
    }
}