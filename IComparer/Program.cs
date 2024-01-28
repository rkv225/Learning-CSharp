using System;
using System.Collections.Generic;

public class Person
{
    public string? Name { get; set; }
    public int Age { get; set; }
}

public class AgeComparer : IComparer<Person>
{
    public int Compare(Person? x, Person? y)
    {
        if(x == null || y == null)
            throw new NullReferenceException();
        // Compare based on the Age property
        return x.Age.CompareTo(y.Age);
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

        // Use the custom comparison (IComparer<T>)
        persons.Sort(new AgeComparer());
        Console.WriteLine("\nSorted by age using IComparer<T>:");
        foreach (var person in persons)
        {
            Console.WriteLine($"{person.Name}, Age: {person.Age}");
        }
    }
}
