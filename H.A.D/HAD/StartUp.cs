using HAD.Core;
using HAD.IO;

namespace HAD
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var reader = new ConsoleReader();
            var writer = new ConsoleWriter();
            var heroManager = new HeroManager();
            var commandProcessor = new CommandProcessor(heroManager);
            var engine = new Engine(reader, writer, commandProcessor);
            engine.Run();
        }
    }
}
