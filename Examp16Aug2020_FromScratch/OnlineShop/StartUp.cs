using System;
using System.IO;
using OnlineShop.Core;
using OnlineShop.IO;
using OnlineShop.Models.Products.Components;

namespace OnlineShop
{
    public class StartUp
    {
        static void Main()
        {
            // Clears output.txt file
            string pathFile = Path.Combine("..", "..", "..", "output.txt");
            File.Create(pathFile).Close();

            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();
            ICommandInterpreter commandInterpreter = new CommandInterpreter();
            IController controller = null; //new Controller();
            //CentralProcessingUnit cp = new CentralProcessingUnit(5, "Intel", "I7", 2000, 200, 3);
            //Console.WriteLine(cp.OverallPerformance);

            IEngine engine = new Engine(reader, writer, commandInterpreter, controller);
            engine.Run();
        }
    }
}
