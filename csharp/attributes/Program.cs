/*
In C#, attributes provide a way to add metadata or additional information to program entities like classes, methods, properties, parameters, etc. Attributes are a form of declarative information that can be retrieved at runtime using reflection. They can be used to convey information to tools, libraries, or other code elements about how to treat the annotated code element.
*/

using System;

// Custom attribute for validation
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class RangeAttribute : Attribute
{
    public int Minimum { get; }
    public int Maximum { get; }

    public RangeAttribute(int minimum, int maximum)
    {
        Minimum = minimum;
        Maximum = maximum;
    }
}

// Class with properties annotated with custom validation attributes
class Person
{
    [Range(1, 100)]
    public int Age { get; set; }

    [Range(1000, 9999)]
    public int EmployeeId { get; set; }
}

class ValidationHelper
{
    public static bool Validate(object obj)
    {
        Type type = obj.GetType();
        foreach (var property in type.GetProperties())
        {
            var rangeAttribute = (RangeAttribute)Attribute.GetCustomAttribute(property, typeof(RangeAttribute));
            if (rangeAttribute != null)
            {
                int value = (int)property.GetValue(obj);
                if (value < rangeAttribute.Minimum || value > rangeAttribute.Maximum)
                {
                    Console.WriteLine($"{property.Name} is not within the specified range.");
                    return false;
                }
            }
        }
        return true;
    }
}

class Program
{
    static void Main()
    {
        // Create an instance of the class
        Person person = new Person
        {
            Age = 25,
            EmployeeId = 5000
        };

        // Validate the object using the custom validation framework
        bool isValid = ValidationHelper.Validate(person);

        // Display the result
        if (isValid)
        {
            Console.WriteLine("Object is valid.");
        }
        else
        {
            Console.WriteLine("Object is not valid.");
        }
    }
}