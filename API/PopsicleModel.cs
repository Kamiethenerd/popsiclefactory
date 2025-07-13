namespace API;

public class PopsicleModel
{
    public class Popsicle
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Flavor { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? Quantity { get; set; }
    }
    public List<Popsicle> TestPopsicles = new List<Popsicle>
    {
        new Popsicle { Id = 1, Name = "Cherry Delight", Flavor = "Cherry", Description = "A sweet cherry-flavored popsicle.", Quantity = 10 },
        new Popsicle { Id = 2, Name = "Lemon Zest", Flavor = "Lemon", Description = "A refreshing lemon-flavored popsicle.", Quantity = 5 },
        new Popsicle { Id = 3, Name = "Mango Tango", Flavor = "Mango", Description = "A tropical mango-flavored popsicle.", Quantity = 8 },
        new Popsicle { Id = 4, Name = "Blueberry Bliss", Flavor = "Blueberry", Description = "A delightful blueberry-flavored popsicle.", Quantity = 12 }
    };

};

