using System.ComponentModel.DataAnnotations;

namespace Eclipseworks.API.DTO.Offer;

public class CreateOfferDTO
{
    [Required(ErrorMessage = "Campo {0} é obrigatório.")]
    public decimal UnitPrice { get; set; }

    [Required(ErrorMessage = "Campo {0} é obrigatório.")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Campo {0} é obrigatório.")]
    public long UserId { get; set; }

    [Required(ErrorMessage = "Campo {0} é obrigatório.")]
    public long WalletId { get; set; }
}