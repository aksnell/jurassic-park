namespace JurassicPark
{
    class Program
    {
        static void Main(string[] args)
        {
            Park zoo = new Park();
            Logger log = new Logger("Alex", true);
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
