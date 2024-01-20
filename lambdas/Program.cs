using System;

// A lambda expression has the following form:
// (parameters) => expression-or-statement-block

int Square(int x)
{
    return x * x;
}
Transformer delegateTransformer = Square;
Console.WriteLine(delegateTransformer(4));

// can also be written as lambda expression
delegateTransformer = x => x * x;
Console.WriteLine(delegateTransformer(4));

// can also be written as func delegate
Func<int, int> funcTransformer = x => x * x;
Console.WriteLine(funcTransformer(4));

Func<string> greetor = () => "Hello, world";
Console.WriteLine(greetor());
// can be written with implicit typing introduced in C# 10
var sayHello = () => "Hello World!";
Console.WriteLine(sayHello());

// From C# 10, you can also specify the lambda return type
var cub = int (int x) => x * x * x;
Console.WriteLine(cub(2));
delegate int Transformer (int i);