using System;
using System.Linq;
using System.Collections.Generic;

namespace jurassic_park
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Dinosaur> dinosaurs = new List<Dinosaur>();

            Console.WriteLine("Welcome to Jurassic Park!");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("\nPlease select an option!\n\tAdd\n\tRemove\n\tTransfer\n\tSummary\n\tView\n\tQuit\n");

                switch (Console.ReadLine())
                {
                    // Notes:
                    // * In the real world I would split off all of theses cases into seperate functions.
                    // * Instead of affecting all matching names on command or forcing unique names, use WhenAcquired as psuedo ID to create list of options for the User to specify.
                    //      * Is using a map against the spirit of the challenge?
                    // * All paths artisninally hand verified.

                    // Add new Dinosaur to list.
                    case "Add":

                        Console.WriteLine("\nDescribe the new dinosaur:");
                        Console.WriteLine("\tExample: name carnivore|herbivore weight enclosure");
                        Console.WriteLine("\tExample: Fred carnivore 4200 1\n");

                        var newDinoArgs = Console.ReadLine().Split(' ').Where(arg => arg != "").ToArray();

                        if (newDinoArgs[1] != "carnivore" && newDinoArgs[1] != "herbivore")
                        {
                            Console.WriteLine("\n\tERROR: Please specify if the dinosaur is a carnivore OR herbivore exactly!");
                            Console.WriteLine("\tExample: name >> carnivore << weight enclosure\n");
                            break;
                        }

                        int newWeight;
                        bool validNewWeight = Int32.TryParse(newDinoArgs[2], out newWeight);
                        if (!validNewWeight)
                        {
                            Console.WriteLine("\n\tERROR: Please enter a valid number for the dinosaur's weight!");
                            Console.WriteLine("\tExample: name carnivore|herbivore >> 37 <<  enclosure\n");
                            break;
                        }

                        int newEnclosure;
                        bool validNewEnclosure = Int32.TryParse(newDinoArgs[3], out newEnclosure);
                        if (!validNewEnclosure)
                        {
                            Console.WriteLine("\n\tERROR: Please enter a valid number for the dinosaur's enclosure!");
                            Console.WriteLine("\tExample: name carnivore|herbivore weight >> 4 <<\n");
                            break;
                        }

                        dinosaurs.Add(new Dinosaur(newDinoArgs[0], newDinoArgs[1], newWeight, newEnclosure));

                        Console.WriteLine("\nSuccesfully added new Dinosaur!\t");
                        dinosaurs.Last().Description();
                        Console.WriteLine();

                        break;

                    // Remove all matching Dinosaur names.
                    case "Remove":

                        Console.WriteLine("\nPlease enter the name of the Dinosaur to remove.");
                        Console.WriteLine("\tHint: All dinosaurs with that name will be removed!\n");

                        string name = Console.ReadLine();
                        dinosaurs.Where(dino => dino.Name != name).ToList();
                        break;

                    // Transfer all matching Dinosaur names.
                    case "Transfer":
                        Console.WriteLine("\nPlease enter the name and new enclosure of the Dinosaur to move!");
                        Console.WriteLine("\tHint: All dinosaurs with that name will be moved!");
                        Console.WriteLine("\tExample: name enclosure\n");

                        var transferArgs = Console.ReadLine().Split(' ').Where(arg => arg != "").ToArray();

                        int transferEnclosure;
                        bool validTransferEnclosure = Int32.TryParse(transferArgs[1], out transferEnclosure);
                        if (!validTransferEnclosure)
                        {
                            Console.WriteLine("\n\tERROR: Please enter a valid number for the dinosaur's new enclosure!");
                            Console.WriteLine("\tExample: name >> 4 <<\n");
                            break;
                        }

                        foreach(var dinosaur in dinosaurs.Where(dino => dino.Name == transferArgs[0]).ToList()) 
                        {
                            Console.WriteLine($"Transfering {dinosaur.Name} to new Enclosure: {transferEnclosure}\n");
                            dinosaur.EnclosureNumber = transferEnclosure;
                        }
                        break;

                    case "Summary":

                        int carn = dinosaurs.Count(dino => dino.DietType == "carnivore");
                        int herb = dinosaurs.Count(dino => dino.DietType == "herbivore");

                        Console.WriteLine("\nSummary");
                        Console.WriteLine($"\t{carn} Carnivores");
                        Console.WriteLine($"{herb} Herbicores\n");
                        break;

                    case "View":

                        dinosaurs.OrderBy(dino => dino.WhenAcquired).ToList().ForEach(dino => dino.Description());
                        break;

                    case "Quit":
                        return;

                    default:
                        Console.WriteLine("\n\tERROR: That is not a valid command!\n");
                        break;
                }
            }
        }
    }
}
