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
            FileWriter fWriter = new FileWriter(pathFile);
            ICommandInterpreter commandInterpreter = new CommandInterpreter();
            IController controller = new Controller();

            IEngine engine = new Engine(reader, fWriter, commandInterpreter, controller);

            engine.Run();
        }
    }
}
