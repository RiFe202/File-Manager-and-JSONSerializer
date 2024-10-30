namespace File_Manager_and_JSONSerializer_30._10._2024;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Models;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // The filepath for the person.json file
            string filepath = "person.json";
            
            // Reads data that already exists in the JSON file
            List<Person>? people = new List<Person>();
            if (File.Exists(filepath))
            {
                string? existingJSON = File.ReadAllText(filepath);
                Console.WriteLine($"Data that already exists within the file person.json: {existingJSON}");
                
                if (!string.IsNullOrWhiteSpace(existingJSON))
                {
                    try
                    {
                        people = JsonSerializer.Deserialize<List<Person>>(existingJSON) ?? new List<Person>();
                    }
                    catch (JsonException)
                    {
                        var singlePerson = JsonSerializer.Deserialize<Person>(existingJSON);
                        if (singlePerson != null)
                        {
                            people.Add(singlePerson);
                        }
                    }
                }
            }

            // Collect new person's information
            Console.WriteLine("What is your name?");
            string? name = Console.ReadLine();
            Console.WriteLine("How old are you?");
            string? ageInput = Console.ReadLine();
            int age;
            while (!int.TryParse(ageInput, out age))
            {
                Console.WriteLine("There was an error with the data submitted, please input your age using numbers.");
                ageInput = Console.ReadLine();
            }
            Console.WriteLine("What country are you from?");
            string? country = Console.ReadLine();
            Console.WriteLine("What city are you from?");
            string? city = Console.ReadLine();
            
            // Initialize a new Person object
            var newPerson = new Person
            {
                Name = name,
                Age = age,
                Country = country,
                City = city
            };
            
            // Add the new person to the list
            people.Add(newPerson);
            Console.WriteLine($"Your name is: {newPerson.Name} and you are {newPerson.Age} years old. You live in {newPerson.Country}, and you reside in {newPerson.City}.");
            
            // Serialize the entire list of people to JSON
            string json = JsonSerializer.Serialize(people, new JsonSerializerOptions { WriteIndented = true });
            
            // Write the updated list to the JSON file
            File.WriteAllText(filepath, json);
            
            Console.WriteLine("Your data was successfully written to the JSON file!");
        }
        catch (IOException exception)
        {
            Console.WriteLine($"An error occurred while attempting to write to the file person.json: {exception.Message}");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"{exception.Message}\n");
        }
    }
}
