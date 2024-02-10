using System;
using System.Dynamic;

class Program
{
    public static string FetchSecretMessage()
    {
        return "Mysecretmessage";
    }
    public static void Main(string[] args)
    {
        Task.Run(() => Console.WriteLine("Foo"));

        // you can also wait for the completion
        var someTask = Task.Run(() => Console.WriteLine("Bar"));
        someTask.Wait();

        // Returning values from task
        Task<int> t1 = Task.Run (() => 
        { 
            Console.WriteLine ("Returning three"); 
            return 3; 
        });

        Console.WriteLine(t1.Result); // calling .results block the main thread, and waits for the results

        // continuation of tasks
        var secretMessageTask = Task.Run(() => FetchSecretMessage());
        var secretMessageTaskAwaiter = secretMessageTask.GetAwaiter(); // you can get the awaiter of the tasks
        // If a synchronization context is present, OnCompleted automatically captures it and posts the continuation to that context
        secretMessageTaskAwaiter.OnCompleted(() => {
            // If an antecedent task faults, the exception is rethrown when the continuation code calls awaiter.GetResult().
            string message = secretMessageTaskAwaiter.GetResult(); // you can also fetch the results from the awaiter
            Console.WriteLine(message);
        });

        // If a synchronization context is missing or if we use ConfigureAwait(false) continuation is resume on any available thread
        // Note that ConfigureAwait only affects how the immediate continuation after the await is executed.

        // Use Thread.Sleep when you want to block the current thread.
        // Use await Task.Delay when you want a logical delay without blocking the current thread.
        var t2 = Task.Delay (5000).ContinueWith (ant => Console.WriteLine (42));
        t2.Wait();
    }
}