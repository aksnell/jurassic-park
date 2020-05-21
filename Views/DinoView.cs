using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;

namespace JurassicPark
{
    class DinosaurView
    {
        private DinosaurController DinoCon;
        private TextInfo Formatter;
        private Logger Log;

        public DinosaurView(DinosaurController dinoCon, Logger log)
        {
            DinoCon = dinoCon;
            Log = log;
            Formatter = new CultureInfo("en-Us", false).TextInfo;
        }
        
        // Returns a list of all Dinosaur.Description() in the passed IEnumerable
        public List<string> GetAllDescriptions(IEnumerable<Park.Dinosaur> dinos)
        {
            var dinoDescriptions = new List<string>();

            foreach (Park.Dinosaur dino in dinos) {
                dinoDescriptions.Add(dino.Description());
            }

            return dinoDescriptions;
        }

        public void WriteLabel(string label)
        {
            Console.Write($"{Formatter.ToTitleCase(label)}: ");
        }

        public void WriteList(string header, List<string> choices)
        {

            int ordinal = 1;
            string formattedList = $"{Formatter.ToTitleCase(header)}:";

            foreach (string choice in choices)
            {
                formattedList += $"\n\t({ordinal}). {choice}";
                ordinal++;
            }

            Console.WriteLine(formattedList);

        }

        // Presents a formatted and numbered list from a list of Strings
        // and returns an integer guarenteed to be in range of a non-empty list
        // or -1 on empty list.
        public int PromptFromList(string header, List<string> choices)
        {
            if (choices.Count() == 0) {
                Console.WriteLine("No matching choices!");
                return -1;
            }
            // Will eventually blow the stack;
            // too tired to refactor
            WriteList(header, choices);
            return PromptForInteger("choice", choices.Count);

        }

        public string PromptForString(string label)
        {

            WriteLabel(label);
            var userInput = Console.ReadLine();

            return userInput;

        }
        // Verifies if string is an integer and optionally if its
        // equal to or less than a passed integer.
        // can c# just declare something to be MAXINT32 to minimize some of this logic?
        public int PromptForInteger(string label, int max = 0)
        {

            WriteLabel(label);
            int userInput;
            var validInput = Int32.TryParse(Console.ReadLine(), out userInput);

            if (validInput)
            {
                if (max != 0 && userInput <= max)
                {
                    return userInput;
                } else if (max == 0) {
                    return userInput;
                }
            }
            // Will eventually block the stack
            // too tired to refactor
            Log.Error("Please enter a valid integer!");
            return PromptForInteger(label, max);
        }

        public string PromptForDate(string label)
        {
            WriteLabel(label);
            string userInput =  Console.ReadLine();

            DateTime parsedDate;
            var validInput = DateTime.TryParse(Console.ReadLine(), out parsedDate);

            if (validInput)
            {
                // It was way too late to try and grok c# generics
                // so valid date time is verified, but original string is
                // returned and its reconverted to a Date when needed later.
                return userInput;
            }
            // Stack risk, etc
            Log.Error("Please enter a valid date!");
            return PromptForDate(label);

        }

        public void Menu()
        {
            bool hasQuit = false;
            List<string> validMenu = new List<string> {
                "Add a new Dinosaur",
                "Remove a Dinosaur",
                "Transfer a Dinosaur",
                "Summarize Dinosaurs",
                "See all Dinosaurs in a specific enclosure",
                "See all Dinosaurs obtained since a specific date",
                "See all Dinosaurs",
                "Quit and save changes",
            };
            List<string> validDiets = new List<string> {"Herbivore", "Carnivore"};
            while (!hasQuit)
            {
                int userInput = PromptFromList("Choose one of the following ", validMenu);

                switch (userInput)
                {
                    // Add dino
                    case 1:
                    {
                        var dinoName = PromptForString("name");
                        var dinoDiet = validDiets[PromptFromList("select a diet", validDiets) - 1];
                        var dinoWeight = PromptForInteger("weight");
                        var dinoEnclosure = PromptForInteger("enclosure");
                        DinoCon.Add(dinoName, dinoDiet, dinoWeight, dinoEnclosure);
                        break;
                    }
                    // Remove dino
                    case 2:
                    {
                        var dinoName = PromptForString("name");
                        var dinoList = DinoCon.View("Name", dinoName).ToList();
                        var dinoIndexToRemove = PromptFromList("select a matching dinosaur", GetAllDescriptions(dinoList));
                        if (dinoIndexToRemove != -1)
                        {
                            DinoCon.Remove(dinoList[dinoIndexToRemove-1]);
                        }
                        break;
                    }
                    // Transfer Dino
                    case 3:
                    {
                        var dinoName = PromptForString("name");
                        var dinoList = DinoCon.View("Name", dinoName).ToList();
                        var dinoIndexToMove = PromptFromList("select a matching dinosaur", GetAllDescriptions(dinoList));
                        if (dinoIndexToMove != -1)
                        {
                            var dinoEnclosure = PromptForInteger("new enclosure");
                            DinoCon.Transfer(dinoList[dinoIndexToMove-1], dinoEnclosure);
                        }
                        break;
                    }
                    // Summarize
                    case 4:
                    {
                        var carnivores = DinoCon.View("Diet", "Carnivore").Count();
                        var herbivores = DinoCon.View("Diet", "Herbivore").Count();
                        WriteList("summary", new List<string> {$"Carnivores: {carnivores}", $"Herbivores: {herbivores}"});
                        break;
                    }
                    // See all in enclosure
                    case 5:
                    {
                        var enclosure = PromptForInteger("enclosure");
                        WriteList($"dinosaurs in {enclosure}", GetAllDescriptions(DinoCon.View("Enclosure", enclosure.ToString())));
                        break;
                    }
                    // See all after date
                    case 6:
                    {
                        var date = PromptForString("date");
                        WriteList($"acquired after {date}", GetAllDescriptions(DinoCon.View("Date", date)));
                        break;
                    }
                    // See all
                    case 7:
                        WriteList("dinosaurs", GetAllDescriptions(DinoCon.View()));
                        break;
                    // Quit
                    case 8:
                        hasQuit = true;
                        DinoCon.Quit();
                        break;
                }
            }
        }


    }
}
