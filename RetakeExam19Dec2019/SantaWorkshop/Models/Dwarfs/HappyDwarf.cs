namespace SantaWorkshop.Models.Dwarfs
{
    public class HappyDwarf : Dwarf
    {
        private const int INITIAL_ENERGY = 100;
        public HappyDwarf(string name) 
            : base(name, INITIAL_ENERGY)
        {
        }
    }
}