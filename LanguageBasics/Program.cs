using System;

// Indices and Ranges in C# 8.0
char[] vowels = new char[] {'a','e','i','o','u'};
char lastElement = vowels [^1]; // 'u'
char secondToLast = vowels [^2]; // 'o'
Index first = 0;
Console.WriteLine($"{lastElement} {secondToLast} {vowels[first]}");

Range firstThree = 0..3;
char[] ftchars = vowels[firstThree];
for(int i = 0; i < ftchars.Length; i++)
    Console.Write($"{ftchars[i]} ");

// NULL operators introduces in C# 8.0

// NULL-coalescing operator
string s1 = null;
string s2 = s1 ?? "nothing"; // s2 evaluates to "nothing"

// Null-Coalescing Assignment Operator

string myVariable = null;
myVariable ??= "default"; // this is equivalent to -> if (myVariable == null) myVariable = "default";

// Null-Conditional Operator
System.Text.StringBuilder sb = null;
string s = sb?.ToString(); // No error; s instead evaluates to null
string t = sb?.ToString() ?? "nothing"; // s evaluates to "nothing"

// switch expressions introduced in C# 8.0
int cardNumber = 11;
string cardName = cardNumber switch
{
 13 => "King",
 12 => "Queen",
 11 => "Jack",
 _ => "Pip card" // equivalent to 'default'
};
string suite = "spades";
string newCard = (cardNumber, suite) switch
{
    (11, "spades") => "Jack of Spades",
    (11, "club") => "Jack of Clubs",
    _ => "Invalid Card"
};
Console.WriteLine(newCard);