using System.Data.Common;

var a = new MyClass(1);
var b = new MyClass(1);
Console.WriteLine(a == b);
Console.WriteLine(a.Equals(b));

class MyClass
{
    public int val { get; set; }
    public MyClass(int x)
    {
        val = x;
    }
    public override int GetHashCode()
    {
        return val;
    }
    /*
    public override bool Equals(object? obj)
    {
        if(obj is null) return false;
        int x = ((MyClass)obj).val;
        return val == x;
    }
    */
    public static bool operator == (MyClass x, MyClass y) => x.val == y.val;
    public static bool operator != (MyClass x, MyClass y) => x.val != y.val; 
}