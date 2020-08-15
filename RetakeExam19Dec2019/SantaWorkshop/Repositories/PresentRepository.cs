using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SantaWorkshop.Models.Presents.Contracts;
using SantaWorkshop.Repositories.Contracts;

namespace SantaWorkshop.Repositories
{
    public class PresentRepository : IRepository<IPresent>
    {
        private readonly ICollection<IPresent> models;

        public PresentRepository()
        {
            this.models = new List<IPresent>();
        }

        public IReadOnlyCollection<IPresent> Models 
            => (IReadOnlyCollection<IPresent>)this.models;

        public void Add(IPresent model) 
            => this.models.Add(model);

        public bool Remove(IPresent model) 
            => this.models.Remove(model);

        public IPresent FindByName(string name) 
            => models.FirstOrDefault(x => x.Name == name);
    }
}
