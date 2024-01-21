record Fraction(int Numerator, int Denominator)
{
    public static Fraction operator + (Fraction x, Fraction y)
    {
        if(x.Denominator == y.Denominator)
            return x with { Numerator = x.Numerator + y.Numerator };
        else
            return new Fraction((x.Numerator * y.Denominator) + (y.Numerator * x.Denominator), (x.Denominator * y.Denominator));
    }
}
class Program
{
    public static void Main(string[] args)
    {
        Fraction f1 = new(1, 2);
        Fraction f2 = new(3, 7);
        Fraction f3 = f1 + f2;
        Console.WriteLine($"{f1} + {f2} = {f3}");
    }
}