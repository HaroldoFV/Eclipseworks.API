namespace Eclipseworks.API.Entities;

public class Wallet
{
    public long Id { get; private set; }
    public long CoinId { get; private set; }
    public long UserId { get; private set; }

    public  virtual Coin Coin { get; private set; }
    public virtual User User { get; private set; }

    public IList<Offer> Offers { get; private set; }
}