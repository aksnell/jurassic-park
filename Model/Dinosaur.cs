using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

namespace JurassicPark
{

    public class Zoo
    {

        private List<Dinosaur> Dinosaurs;
        private string FileName = "dinosaurs.json";

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

        public async void Save()
        {
            using (FileStream fs = File.Create(FileName))
            {
                await JsonSerializer.SerializeAsync(fs, Dinosaurs);
            }
        }

        public void Load()
        {
            string jsonString = File.ReadAllText(FileName);
            Dinosaurs = JsonSerializer.Deserialize<List<Dinosaur>>(jsonString);
        }

        public class Dinosaur
        {

            protected internal string Name       { get; set; }
            protected internal string Diet       { get; set; }
            protected internal int Weight        { get; set; }
            protected internal int Enclosure     { get; set; }
            protected internal int ID { get; set; }
            protected internal DateTime Acquired { get; set; }

            protected internal Dinosaur(string name, string diet, int weight, int enclosure)
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
