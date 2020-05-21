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

        public DinosaurView(DinosaurController dinoCon)
        {
            DinoCon = dinoCon;
            Formatter = new CultureInfo("en-Us", false).TextInfo;
        }

        public void DinoWriteLine(string line)
        {
            Console.WriteLine($"\n{line}");
        }

        public void WriteNumericList(string header, IEnumerable<Zoo.Dinosaur> list)
        {

            int ordinal = 1;
            string result = $"{Formatter.ToTitleCase(header)}:\n";

            foreach (Zoo.Dinosaur dino in list)
            {
                result += ($"\t({ordinal}). {dino.Description()}\n");
            }

            DinoWriteLine(result);
        }

        public Zoo.Dinosaur PromptForChoice(IEnumerable<Zoo.Dinosaur> dinos)
        {
            Zoo.Dinosaur[] dinoArray = dinos.ToArray();
            WriteNumericList("choose which dinosaur", dinos.ToArray());

            int userChoice = PromptForInteger(dinoArray.Count());

            while (userChoice == -1)
            {
                DinoWriteLine("Please enter a valid choice!");
                userChoice = PromptForInteger(dinoArray.Count());
            }

            return dinoArray[userChoice-1];


        }

        public string PromptForString()
        {

            DinoWriteLine("Answer: ");
            var userInput = Console.ReadLine();

            return userInput;

        }

        public int PromptForInteger(int max)
        {

            DinoWriteLine("Number: ");

            int userInput;
            var validInput = Int32.TryParse(Console.ReadLine(), out userInput);

            if (validInput && userInput <= max)
            {
                return userInput;
            }
            return -1;
        }
    }
}
