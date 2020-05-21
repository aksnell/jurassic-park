using System;

namespace JurassicPark
{
    class Program
    {
        // Here lies jank and glue.
        static void Main(string[] args)
        {
            Console.WriteLine("What is your name?");
            string name = Console.ReadLine();
            Park zoo = new Park();
            Logger log = new Logger(name, true);
            if (zoo.Load())
            {
                log.Info("loaded database file");
            } else {
                log.Info("could not load database file");
            }
            DinosaurController dinoCon = new DinosaurController("Alex", zoo, log);
            DinosaurView dinoView = new DinosaurView(dinoCon, log);
            dinoView.Menu();
            zoo.Save();
        }
    }
}
