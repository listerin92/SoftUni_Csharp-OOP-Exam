using System.Collections.Generic;
using System.Linq;
using SantaWorkshop.Models.Presents.Contracts;
using SantaWorkshop.Repositories.Contracts;

namespace SantaWorkshop.Repositories
{
    public class PresentRepository : IRepository<IPresent>
    {
        private readonly List<IPresent> models;

        public PresentRepository()
        {
            this.models = new List<IPresent>();
        }

        public IReadOnlyCollection<IPresent> Models 
            => this.models.AsReadOnly();
        public void Add(IPresent model) 
            => this.models.Add(model);

        public bool Remove(IPresent model) 
            => this.models.Remove(model);

        public IPresent FindByName(string name) 
            => models.FirstOrDefault(x => x.Name == name);
    }
}
