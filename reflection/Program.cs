/*
Reflection in C# is a powerful feature that allows you to inspect and interact with the metadata of types, assemblies, and objects at runtime. With reflection, you can dynamically query and manipulate types, retrieve information about their members (fields, properties, methods, etc.), and even create and invoke objects dynamically.
*/
using System.Reflection;

namespace DevLearning;
internal class Person
{
    public string? Name { get; set; }
    public int Age { get; set; }

    public Person(string n, int a)
    {
        Name = n;
        Age = a;
    }

    public void PrintPerson() => Console.WriteLine($"{Name} is {Age} years old");
} 

internal class Program
{
    public static void Main(string[] args)
    {
        Person p = new("Harry", 20);
        
        // fetching class
        Type myclass = typeof(Person);
        Console.WriteLine(myclass.Name);
        Type myClass2 = p.GetType();
        Console.WriteLine(myClass2.Name);

        // fetching namespace and assembly
        Console.WriteLine("Assembly: " + myclass.Assembly.FullName);
        Console.WriteLine("Namespace: " + myclass.Namespace?.ToString());

        // fetching and executing methods of an object
        MethodInfo[] methods = myclass.GetMethods();
        foreach(var method in methods) 
        {
            Console.WriteLine(method.Name);
            if(method.Name == "PrintPerson")
            {
                Console.WriteLine("Invoking method via reflection");
                method.Invoke(p, null);
            }
        }

        // fetching properties and values of an object 
        PropertyInfo[] props = myclass.GetProperties();
        foreach(var prop in props)
        {
            Console.WriteLine($"Property name: {prop.Name}, value: {prop.GetValue(p)}");
        }
    }
} 
