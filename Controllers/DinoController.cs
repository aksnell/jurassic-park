using System;
using System.Collections.Generic;

namespace JurassicPark
{

    public class DinosaurController
    {

        private string User;
        private Zoo Zoo;
        private Logger Logger;

        public DinosaurController(string user, Zoo zoo, bool debug = false)
        {
            User = user;
            Zoo = zoo;
            Logger = new Logger(debug);
            Log("connected.");
        }

        public IEnumerable<Zoo.Dinosaur> View(string key = "", string option = "")
        {
            switch (key)
            {
                case "D":
                    Log($"viewed all Dinosaurs acquired after {option}");
                    return Zoo.FindWhere(dino => dino.Acquired > DateTime.Parse(option));
                case "E":
                    Log($"viewed all Dinosaurs in enclosure {option}");
                    return Zoo.FindWhere(dino => dino.Enclosure == Int32.Parse(option));
                default:
                    Log("viewed all Dinosaurs.");
                    return Zoo.FindWhere();
            }
        }

        public void Add(string name, string diet, int weight, int enclosure)
        {
            Log($"added new Dinosaur named {name} in enclosure {enclosure}");
            Zoo.AddDinosaur(name, diet, weight, enclosure);
        }

        public void Remove(Zoo.Dinosaur dino)
        {
            Log($"removed dinosaur - {dino.Description()}");
            Zoo.RemoveDinosaur(dino);
        }

        public void Transfer(Zoo.Dinosaur dino, int enclosure)
        {
            Log($"transferred - {dino.Description()} to new enclosure {enclosure}");
            Zoo.MoveDinosaur(dino, enclosure);
        }

        public void Quit()
        {
            Log("disconnected.");
            Logger.Save();
        }

        private void Log(string log)
        {
            Logger.Write($"{User}: log");
        }

    }

}
