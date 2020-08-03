using AquaShop.Core;
using AquaShop.Core.Contracts;

namespace AquaShop
{
    public class StartUp
    {
        public static void Main()
        {
            
            IEngine engine = new Engine();
            engine.Run();

        }
    }
}
