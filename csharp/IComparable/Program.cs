using System;
using System.Collections.Generic;

public class Person : IComparable<Person>
{
    public string? Name { get; set; }
    public int Age { get; set; }

    // Implementation of IComparable<T>
    public int CompareTo(Person? other)
    {
        // Compare based on the Age property
        return Age.CompareTo(other?.Age);
    }
}

class Program
{
    static void Main()
    {
        // Create a list of persons
        List<Person> persons = new List<Person>
        {
            new Person { Name = "John", Age = 30 },
            new Person { Name = "Jane", Age = 25 },
            new Person { Name = "Bob", Age = 40 }
        };

        // Use the default comparison (IComparable<T>)
        persons.Sort();
        Console.WriteLine("Sorted by age using IComparable<T>:");
        foreach (var person in persons)
        {
            Console.WriteLine($"{person.Name}, Age: {person.Age}");
        }
    }
}
