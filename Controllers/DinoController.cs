using System;
using System.Collections.Generic;

namespace JurassicPark
{

    public class DinosaurController
    {

        private string User;
        private Park Zoo;
        private Logger Log;

        public DinosaurController(string user, Park zoo, Logger log)
        {
            User = user;
            Zoo = zoo;
            Log = log;
            Log.Info("connected.");
        }

        public IEnumerable<Park.Dinosaur> View(string key = "", string option = "")
        {
            switch (key)
            {
                case "Name":
                    Log.Info($"view all Dinosaurs with name {option}");
                    return Zoo.FindWhere(dino => dino.Name == option);
                case "Diet":
                    Log.Info($"view all Dinosaurs with diet {option}");
                    return Zoo.FindWhere(dino => dino.Diet == option);
                case "Date":
                    Log.Info($"viewed all Dinosaurs acquired after {option}");
                    return Zoo.FindWhere(dino => dino.Acquired > DateTime.Parse(option));
                case "Enclosure":
                    Log.Info($"viewed all Dinosaurs in enclosure {option}");
                    return Zoo.FindWhere(dino => dino.Enclosure == Int32.Parse(option));
                default:
                    Log.Info("viewed all Dinosaurs.");
                    return Zoo.FindWhere();
            }
        }

        public void Add(string name, string diet, int weight, int enclosure)
        {
            Log.Info($"added new Dinosaur named {name} in enclosure {enclosure}");
            Zoo.AddDinosaur(name, diet, weight, enclosure);
        }

        public void Remove(Park.Dinosaur dino)
        {
            Log.Info($"removed a dinosaur {dino.Name} located in enclosure {dino.Enclosure}");
            Zoo.RemoveDinosaur(dino);
        }

        public void Transfer(Park.Dinosaur dino, int enclosure)
        {
            Log.Info($"transferred - {dino.Description()} to new enclosure {enclosure}");
            Zoo.MoveDinosaur(dino, enclosure);
        }

        public void Quit()
        {
            Zoo.Save();
            Log.Info("saved database");
            Log.Info("disconnected");
            Log.Save();

        }


    }

}
