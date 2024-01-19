using System;

class Program
{
    delegate int Transformer(int x);
    delegate void Greeter(string x);
    static void Transform (int[] values, Transformer t)
    {
        for (int i = 0; i < values.Length; i++)
        values[i] = t (values[i]);
    }
    static void TransformFunc (int[] values, Func<int, int> t)
    {
        for (int i = 0; i < values.Length; i++)
        values[i] = t (values[i]);
    }
    public static void Main(string[] args)
    {
        // defining methods
        int Square(int x) =>  x * x;
        int Cube(int x) => x * x * x;

        // invoking delegates
        Transformer t = Square;
        Console.WriteLine(t(3));
        t = Cube;
        Console.WriteLine(t(2));

        // delegates as plugin methods  
        int[] values = { 1, 2, 3 };
        Transform (values, Square); // Hook in the Square method
        foreach (int i in values)
            Console.Write (i + " "); // 1 4 9
        Console.WriteLine();

        // some more methods
        void SayGoodMorning(string name) => Console.WriteLine($"Good Morning {name}");
        void SayGoodNight(string name) => Console.WriteLine($"Good Night {name}");

        // multicast delegates
        Greeter g = SayGoodMorning;
        g += SayGoodNight;

        g("Rishabh");

        // rewriting above delegates using act and func delegates
        Action<string> actGreeter = SayGoodMorning;
        actGreeter += SayGoodNight;
        actGreeter("action delegate");

        Func<int, int> funcTransfomer = Cube;
        TransformFunc(values, funcTransfomer);
        foreach (int i in values)
            Console.Write (i + " "); // 1 4 9
        Console.WriteLine();
    }
}