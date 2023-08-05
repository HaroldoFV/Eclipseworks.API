namespace Eclipseworks.API.Entities;

public class Offer
{
    public Offer()
    {
    }

    public Offer(decimal unitPrice, int quantity, long userId, long walletId)
    {
        UnitPrice = unitPrice;
        Quantity = quantity;
        DateCreation = DateTime.UtcNow;
        UserId = userId;
        WalletId = walletId;
    }

    public int Id { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public DateTime DateCreation { get; private set; }
    public long UserId { get; private set; }
    public long WalletId { get; private set; }
    public bool IsDeleted { get; set; }
    public User User { get; private set; }
    public Wallet Wallet { get; private set; }
}