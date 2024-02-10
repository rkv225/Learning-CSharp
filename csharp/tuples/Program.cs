using System;
using System.Data.Common;
// tuples were introduced in C# 7.0
// declaring and initializing tuples 
Tuple<int, string> ram = new Tuple<int, string>(1, "Ram");
Console.WriteLine($"{ram.Item1} {ram.Item2}");

// implicitly defined tuples
var sita = (2, "Sita");
Console.WriteLine($"{sita.Item1} {sita.Item2}");

// naming tuple values
var shyam = (id: 3, name: "Shyam");
Console.WriteLine($"{shyam.id} {shyam.name}");
Console.WriteLine($"{shyam.Item1} {shyam.Item2}"); // this is also perfectly valid

// tuples using factory method
var geeta = Tuple.Create(4, "Geeta");
Console.WriteLine($"{geeta.Item1} {geeta.Item2}");

// using deconstructor syntax
(int id, string name) = geeta;
Console.WriteLine($"{id} {name}");