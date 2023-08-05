namespace Eclipseworks.API.Entities;

public class User
{
    public User()
    {
        
    }
    public User(string name, IList<Wallet> wallets)
    {
        Name = name;
        Wallets = wallets;
    }

    public long Id { get; private set; }
    public string Name { get; private set; }
    public string Cpf { get; private set; }
    public string Email { get; private set; }
    public virtual IList<Wallet> Wallets { get; private set; }
    public IList<Offer> Offers { get; private set; }

    public void Update(string name, IList<Wallet> wallets)
    {
        Name = name;
        Wallets = wallets;
    }
}