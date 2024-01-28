// Nuget package system.linq.async supports linq with asynenumerables
// asynchronous streams introduced in C# 8
// this internally implements IAsyncDisposable, IAsyncEnumerator<out T> which return a newly introduced type of task ValueTask<T>
static async IAsyncEnumerable<int> RangeAsync (int start, int count, int delay)
{
    for (int i = start; i < start + count; i++)
    {
        await Task.Delay (delay);
        yield return i;
    }
}

await foreach (var number in RangeAsync (0, 10, 2000))
    Console.WriteLine (number);

// Querying IAsyncEnumerable<T>

IAsyncEnumerable<int> query =
    from i in RangeAsync (0, 10, 500)
    where i % 2 == 0 // Even numbers only.
    select i * 10; // Multiply by 10.
await foreach (var number in query)
    Console.WriteLine (number);