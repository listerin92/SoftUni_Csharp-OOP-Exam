using System.Collections.Generic;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;

namespace AquaShop.Models.Aquariums
{
    public class FreshwaterAquarium : Aquarium
    {
        private const int FreshwaterAquariumCapacity = 50;
        public FreshwaterAquarium(string name) 
            : base(name, FreshwaterAquariumCapacity)

        {
            
        }
    }
}