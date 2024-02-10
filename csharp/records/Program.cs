
// records introduced in C# 9 to work well with immutable(readonly) data
// best suited for data where we want to preserve immutability
// Records are purely a C# compile-time construct
// records support non-destructive mutation In order to “modify” an immutable object, you must create a new one and copy over the data while incorporating your modifications
// Records give you structural equality by default. Structural equality means that two instances are the same if their data is the same (as with tuples).

using System.Data;
using System.Runtime;

record RPoint
{
    public RPoint (double x, double y) => (X, Y) = (x, y);
    public double X { get; init; }
    public double Y { get; init; }  
}

// creating a record does additional tasks
// 1. it creates a protected copy constructor and a hidden clone method to support non destructive mutation
// 2. it iverrides/overloads equality operators/functions
// 3. it overrides ToString() method to print the definition of the record

// A record definition can also include a parameter list:
record Point (double X, double Y);
record Test (int A, int B, int C, int D, int E, int F, int G, int H);
/*
 If a parameter list is specified, the compiler performs the following extra steps:
• It writes an init-only property per parameter.
• It writes a primary constructor to populate the properties.
• It writes a deconstru(ctor.
*/

// record types also support inheritance
abstract record Icecream (string flavor, string topping);

record IcecreamServing(string flavor, string topping, string size):Icecream(flavor, topping);

class Program
{
    public static void Main(string[] args)
    {
        Point p1 = new Point (3, 3);
        Point p2 = p1 with { Y = 4 };
        Console.WriteLine (p2); // Point { X = 3, Y = 4 }

        Test t1 = new Test (1, 2, 3, 4, 5, 6, 7, 8);
        Test t2 = t1 with { A = 10, C = 30 };
        Console.WriteLine (t2);

        // equality comparison
        Point px = new (5, 2);
        Point py = new (5, 2);
        Console.WriteLine (px.Equals (py)); // True

        IcecreamServing ic = new("chocolate", "almonds", "medium");
        Console.WriteLine(ic);
    }
}