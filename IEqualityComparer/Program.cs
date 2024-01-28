public class Person
{
    public string? Name { get; set; }
    public int Age { get; set; }
}

public class PersonEqualityComparer : IEqualityComparer<Person>
{
    public bool Equals(Person? x, Person? y)
    {
        if (ReferenceEquals(x, y))
            return true;
        if (x == null || y == null)
            return false;
        return x.Name == y.Name && x.Age == y.Age;
    }

    public int GetHashCode(Person obj)
    {
        return (obj.Name?.GetHashCode() ?? 0) ^ obj.Age.GetHashCode();
    }
}

class Program
{
    static void Main()
    {
        // trying out the IEqualityComparer
        var person1 = new Person { Name = "John", Age = 30 };
        var person2 = new Person { Name = "John", Age = 30 };

        var comparer = new PersonEqualityComparer();

        Console.WriteLine($"Are person1 and person2 equal? {comparer.Equals(person1, person2)}");
        Console.WriteLine($"Hash code of person1: {comparer.GetHashCode(person1)}");
        Console.WriteLine($"Hash code of person2: {comparer.GetHashCode(person2)}");

        /*
        IEqualityComparer<T> is typically used to provide external custom equality comparison logic for types, often used with collections. 
        IEquatable<T> is used to define custom equality logic within the type itself. 
        Depending on your use case, you may implement one or both interfaces.
        */

        // Trying this with a collection
        Dictionary<Person, string> contactBook = new(comparer);
        contactBook[person1] = person1.Name;

        Console.WriteLine(contactBook.ContainsKey(person2)); // true
    }
}
