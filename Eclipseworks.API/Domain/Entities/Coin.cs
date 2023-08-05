namespace Eclipseworks.API.Entities;

public class Coin
{
    public long Id { get; private set; }
    public Guid Token { get; private set; }
    public decimal Saldo { get; private set; }
    public string Type { get; private set; }
    
    public virtual IList<Wallet> Wallets { get; private set; }
}