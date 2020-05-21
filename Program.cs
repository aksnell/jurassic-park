using System;

namespace JurassicPark
{
    class Program
    {
        static void Main(string[] args)
        {
            Park park = new Park(); 
            
            Console.WriteLine("What is your name?");
            string name = Console.ReadLine();
            
            Logger log = new Logger(name, true);
            DinosaurController dinoCon = new DinosaurController(name, zoo, log);
            DinosaurView dinoView = new DinosaurView(dinoCon, log);
            
            // Attempt to load database.
            if (zoo.Load())
            {
                log.Info("loaded database file");
            } else {
                log.Info("could not load database file");
            }
            
            // Run the view.          
            dinoView.Menu();
            
            // Save after Menu returns.
            zoo.Save();
        }
    }
}
