using System;

public class Person : IEquatable<Person>
{
    public string? Name { get; set; }
    public int Age { get; set; }

    // Implementing Equals method for IEquatable<T>
    public bool Equals(Person? other)
    {
        if (other == null)
            return false;
        return Name == other.Name && Age == other.Age;
    }

    // Overriding Equals method from the Object class
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj?.GetType())
            return false;
        return Equals((Person)obj);
    }

    // Overriding GetHashCode to generate a unique hash code for instances
    public override int GetHashCode()
    {
        return (Name?.GetHashCode() ?? 0) ^ Age.GetHashCode();
    }
}

class Program
{
    static void Main()
    {
        // Creating instances of the Person class
        Person person1 = new Person { Name = "John", Age = 30 };
        Person person2 = new Person { Name = "John", Age = 30 };
        Person person3 = new Person { Name = "Jane", Age = 25 };

        // Using Equals method for comparison
        Console.WriteLine($"person1 equals person2: {person1.Equals(person2)}");  // Output: True
        Console.WriteLine($"person1 equals person3: {person1.Equals(person3)}");  // Output: False
        Console.WriteLine(person1 == person2); // this doesn't work on == operator as this only checks referential equality

        // Using GetHashCode for hash code comparison
        Console.WriteLine($"Hash code of person1: {person1.GetHashCode()}");
        Console.WriteLine($"Hash code of person2: {person2.GetHashCode()}");
        Console.WriteLine($"Hash code of person3: {person3.GetHashCode()}");
    }
}
