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
            IWriter writer = new FileWriter(pathFile);
            ICommandInterpreter commandInterpreter = new CommandInterpreter();
            IController controller = new Controller();



            IEngine engine = new Engine(reader, writer, commandInterpreter, controller);
            engine.Run();
        }
    }
}
