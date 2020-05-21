using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

namespace JurassicPark
{

    public class Park
    {

        private List<Dinosaur> Dinosaurs;
        private string FileName = "dinosaurs.json";

        public Park() 
        {
            Dinosaurs = new List<Dinosaur>{};
        }

        public IEnumerable<Dinosaur> FindWhere(Func<Dinosaur, bool> findThunk = null)
        {
            if (findThunk == null)
            {
                return Dinosaurs.OrderBy(dino => dino.Acquired);
            }
            return Dinosaurs.Where(findThunk).OrderBy(dino => dino.Acquired);
        }

        public void AddDinosaur(string name, string diet, int weight, int enclosure)
        {
            Dinosaur newDino = new Dinosaur(name, diet, weight, enclosure);
            Dinosaurs.Add(newDino);
        }

        public void RemoveDinosaur(Dinosaur dino)
        {
            Dinosaurs.Remove(dino);
        }

        public void MoveDinosaur(Dinosaur dino, int newEnclosure)
        {
            dino.Enclosure = newEnclosure;
        }

        // WHY IS JSON SERIALIZATION SO CONVOLUTED IN C#.
        // THIS WAY LEADS TO MADNESS.
        public void Save()
        {
            ParkJson dinoJson = new ParkJson{};
            dinoJson.Dinosaurs = Dinosaurs;
            File.WriteAllText(FileName, JsonSerializer.Serialize(dinoJson));
        }

        public bool Load()
        {
            string jsonString;
            try
            {
                jsonString = File.ReadAllText(FileName);
            }
            catch
            {
                return false;
            }
            ParkJson dinoJson = JsonSerializer.Deserialize<ParkJson>(jsonString);
            Dinosaurs = dinoJson.Dinosaurs;
            return true;
        }

        // This is my last ditch attempt to get json serialization working after the fact.
        // Why in the world can you not just deserialize directly into a list????
        // I must have missed something.
        // If I had known it was this silly I would have saved a good chunk of time and just
        // changed when and were I deseralize from outside the Park class.
        public class ParkJson
        {
            public List<Dinosaur> Dinosaurs { get; set; }
        }

        public class Dinosaur
        {

            public string Name       { get; set; }
            public string Diet       { get; set; }
            public int Weight        { get; set; }
            public int Enclosure     { get; set; }
            public int ID { get; set; }
            public DateTime Acquired { get; set; }

            public Dinosaur() {}

            public Dinosaur(string name, string diet, int weight, int enclosure)
            {
                Name = name;
                Diet = diet;
                Weight = weight;
                Enclosure = enclosure;
                Acquired = DateTime.Now;
            }

            public string Description() {

                return $"{Name} is a {Weight} pound {Diet} acquired on {Acquired} and located in enclosure {Enclosure}.";

            }

        }

    }
}
