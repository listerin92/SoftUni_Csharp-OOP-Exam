using System;
using System.IO;
using OnlineShop.Core;
using OnlineShop.IO;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Computers;

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
            IController controller = new Controller();

            ////tests
            //IComponent cp = new CentralProcessingUnit(5, "Intel", "I7", 2000, 200, 3);
            //Computer computer = new DesktopComputer(1, "Intel", "Qkiq", 100);
            //computer.AddComponent(cp);
            //computer.RemoveComponent(nameof(CentralProcessingUnit));

            ////end tests

            IEngine engine = new Engine(reader, writer, commandInterpreter, controller);
            engine.Run();
        }
    }
}
