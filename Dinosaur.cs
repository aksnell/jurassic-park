using System;

public class Dinosaur
{

    public string Name           { get; set; }
    public string DietType       { get; set; }
    public int Weight            { get; set; }
    public int EnclosureNumber   { get; set; }
    public DateTime WhenAcquired { get; set; }

    public Dinosaur(string name, string diet, int weight, int enclosure)
    {
        Name = name;
        DietType = diet;
        Weight = weight;
        EnclosureNumber = enclosure;
        WhenAcquired = DateTime.Now;
    }

    public void Description()
    {
        Console.WriteLine($"Name: {Name} Diet: {DietType} Acquired: {WhenAcquired} Enclosure: {EnclosureNumber}");
    }
}
