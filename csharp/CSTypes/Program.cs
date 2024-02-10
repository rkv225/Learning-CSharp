using System;

class Rectangle
{
    protected int Height { get; set; }
    protected int Width { get; set; }

    public Rectangle(int h, int w)
    {
        this.Height = h;
        this.Width = w;
    }

    // introduced in c# 7.0
    public void Deconstruct(out int h, out int w)
    {
        h = Height;
        w = Width;
    }
}

// From C# 9, you can declare a property accessor with init instead of set
// These init-only properties act like read-only properties, except that they can also be set via an object initializer
// Init-only properties cannot even be set from inside their class, except via their
// property initializer, the constructor, or another init-only accessor.
class Note
{
    private int[] BasicPitch = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
    public int Pitch { get; init; }
    public int Duration { get; init; }

    // implementing an indexer
    public int this [int index]
    {
        get {
            if(index < 10)
                return BasicPitch[index];
            return 0;
        }
        set {
            if(index < 10)
                BasicPitch[index] = value;
        }
    }
}

public class Asset
{
    public string Name;
    public virtual Asset Clone() => new Asset { Name = Name };
}
public class House : Asset
{
    public decimal Mortgage;
    // covariant return types C# 9; we can return return a subclass on override method
    public override House Clone() => new House { Name = Name, Mortgage = Mortgage };
}

public class BaseClass
{
    public virtual void Foo() { Console.WriteLine ("BaseClass.Foo"); }
}
public class Overrider : BaseClass
{
    public override void Foo() { Console.WriteLine ("Overrider.Foo"); }
}
public class Hider : BaseClass
{
    public new void Foo() { Console.WriteLine ("Hider.Foo"); }
}

interface ITest
{
    public void SayHello() => Console.WriteLine("Hello dear user");  
}

class Test1: ITest
{
    public string Name { get; init; }
    // default implementation to an interface member introduced in C# 8. 
    // This is useful when we are adding new methods to an interface and don't want to break all the implementations. 
    public void SayHello() => Console.WriteLine($"Hello dear {this.Name}");
}

class Test2: ITest
{
    public string Name { get; init; }
    // shouldn't have compiled as all interface methods are not implemented
}

class Program
{
    public static void Main(string[] args)
    {
        // Target-Typed new Expressions C# 9, calling new without specifying a type
        Rectangle r = new(4, 10);
        var (height, width) = r;
        Console.WriteLine($"Height: {height} and width {width}");

        Note n = new() { Duration = 10, Pitch = 40 };
        // n.Duration = 3; // should not compile as we can't assign value after init to init type
        int x = n[5]; // referecing an indexer; return x = 60
        Console.WriteLine(x);

        House house = new() { Name = "robert's creek", Mortgage = 10000 };
        var newHouse = house.Clone();
        Console.WriteLine(newHouse.GetType());

        // new on overridden method hides the method
        Overrider over = new Overrider();
        BaseClass b1 = over;
        over.Foo(); // Overrider.Foo
        b1.Foo(); // Overrider.Foo
        Hider h = new Hider();
        BaseClass b2 = h;
        h.Foo(); // Hider.Foo
        b2.Foo(); // BaseClass.Foo

        // testing default interface member
        ITest t1 = new Test1(){ Name = "Rishabh" };
        ITest t2 = new Test2(){ Name = "Rishabh" };
        t1.SayHello();
        t2.SayHello();
    }
}