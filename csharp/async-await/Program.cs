// calling an async function
// must await for the result or otherwise we will get the Task 
// then we will have use .Result or get awaiter to get the results
static async Task<string> GetSecretMessage() // must return Task in place of void or Task<T> for return value prefixed with async keyword
{
    var task = Task.Run(() => "MySecretMessage");
    string result = await task;
    return result;

    // can also be written as: 
    // return await Task.Run(() => "MySecretMessage");
}
string message = await GetSecretMessage();
Console.WriteLine(message);

// async lambda expressions
Func<Task> foo = async () => 
{
    await Task.Delay(1000);
    Console.WriteLine("Foo");
};
await foo();

// Async pattern - CancellationToken
Console.WriteLine("Demonstrating Cancellation token use");
async Task Foo(CancellationToken cancellationToken)
{
    for(int i = 0; i < 10; i++)
    {
        Console.WriteLine(i);
        await Task.Delay(1000);
        cancellationToken.ThrowIfCancellationRequested();
    }
}
CancellationTokenSource cts = new();
Task fooTask = Foo(cts.Token);
await Task.Delay(5000);
cts.Cancel();
if(!cts.IsCancellationRequested)
    await fooTask;

// Async pattern - Progress reporting
// IProgress<T> is an action type delegate for progress reporting
Task Bar(IProgress<int> onProgressPercentChanged)
{
    return Task.Run(async () => {
        for(int i = 0; i < 1001; i++)
        {
            await Task.Delay(10);
            if(i % 10 == 0)
            {
                // call the report method on the IProgress action delegate
                onProgressPercentChanged.Report(i / 10);
            }
        }
    });
}
// progress contructor takes callback method to be executed on report
var progress = new Progress<int>(i => Console.WriteLine(i + " % competed"));
Task barTask = Bar(progress);
await barTask;

// WhenAny Task combinator
async Task<int> Delay1() { await Task.Delay (1000); return 1; }
async Task<int> Delay2() { await Task.Delay (2000); return 2; }
async Task<int> Delay3() { await Task.Delay (3000); return 3; }
Task<int> winningTask = await Task.WhenAny (Delay1(), Delay2(), Delay3());
Console.WriteLine ("Done");
Console.WriteLine (winningTask.Result); // 1

async Task<int> Fetch1() { await Task.Delay (1000); return 1; }
async Task<int> Fetch2() { await Task.Delay (2000); return 2; }
async Task<int> Fetch3() { await Task.Delay (3000); return 3; }
List<Task<int>> fetchTasks = new() { Fetch1(), Fetch2(), Fetch3() };
var results = await Task.WhenAll(fetchTasks);
Console.WriteLine(results[0]);
Console.WriteLine(results[1]);
Console.WriteLine(results[2]);