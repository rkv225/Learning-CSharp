// introduced in C# 8 and improvements are going on
using System.Security.Cryptography.X509Certificates;

Object obj = "rishabh";
if (obj is string s)
    Console.WriteLine (s.Length);
if(obj is string { Length : 7 } )
    Console.WriteLine(obj);

// constant pattern
Object num = 3;
if(num is 3)
    Console.WriteLine(num);

// relational pattern introduced in C# 9
if(num is > 0)
    Console.WriteLine("greater than 0");

string result = num switch
{
    0 => "zero",
    1 => "one",
    2 => "two",
    3 => "three",
    4 => "four",
    _ => "out of range"
};
Console.WriteLine(result);

// combination pattern
bool IsJanetOrJohn (string name) => name.ToUpper() is "JANET" or "JOHN";
bool IsVowel (char c) => c is 'a' or 'e' or 'i' or 'o' or 'u';
bool IsLetter (char c) => c is >= 'a' and <= 'z' or >= 'A' and <= 'Z';

Console.WriteLine(IsJanetOrJohn("janet"));
Console.WriteLine(IsVowel('e'));
Console.WriteLine(IsLetter('4'));

bool ShouldAllow (Uri uri) => uri switch
{
 { Scheme: "http", Port: 80 } => true,
 { Scheme: "https", Port: 443 } => true,
 { Scheme: "ftp", Port: 21 } => true,
 { IsLoopback: true } => true,
 _ => false
};

var builder = new UriBuilder("www.google.com");
builder.Port = 443;
builder.Scheme = "https";
Console.WriteLine(ShouldAllow(builder.Uri));