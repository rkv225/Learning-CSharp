using System;

Stack<int> st = new(10);
st.Push(1);
st.Push(2);
Console.WriteLine(st.Pop());
Console.WriteLine(st.Pop());

Stack<string> ss = new(5);
ss.Push("ram");
ss.Push("sita");
Console.WriteLine(ss.Pop());
Console.WriteLine(ss.Pop());
public class Stack<T>
{
    public int Size { get; }
    private int position;
    private T[] values;
    public Stack(int siz)
    {
        Size = siz;
        position = -1;
        values = new T[siz];
    }
    public void Push(T x) => values[++position] = x;
    public T Pop() => values[position--];
}