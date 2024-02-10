// C# top level statements C# 9.0

using System;

int x = 2 + 3;
Console.WriteLine(x);

int a = int.MaxValue;
int b = 1;
int uc = unchecked (a + b);
Console.WriteLine(uc); // this doesn't throw exception but gives out wrong value
int c = checked (a + b); // Checks just the expression. // this throws exception
Console.WriteLine(c);

