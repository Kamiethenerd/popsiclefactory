using System.Collections.Generic;

namespace API
{
     public interface IPopsicleRepository
    {
        List<PopsicleModel.Popsicle> Popsicles { get; }
    }
    public class InMemoryPopsicleRepository : IPopsicleRepository
    {
        public List<PopsicleModel.Popsicle> Popsicles { get; } = new List<PopsicleModel.Popsicle>();
    }
}