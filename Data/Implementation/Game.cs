using Data.API;

namespace Data.Implementation;

internal class Game : IProduct
{
    public Game(int id, string name, double price)
    {
        Id = id;
        Name = name;
        Price = price;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }

}
