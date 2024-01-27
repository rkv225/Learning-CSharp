using System;

class Test
{
    static readonly object _locker = new();
    bool _done;

    public void Reset() => _done = false;
    public void Go()
    {
        if(!_done)
        {
            Console.WriteLine("Hello");
            Thread.Sleep(1); // just to mimick some task
            _done = true;
        }
    }

    public void SafeGo()
    {
        // acquiring an exclusive lock before entering the ciritcal code block with shared state 
        // but this is not a silver bullet as it introduces a problem of deadlock and starvation
        lock(_locker) 
        {
            if(!_done)
            {
                Console.WriteLine("Hello");
                Thread.Sleep(1);
                _done = true;
            }
        }
    }

    public void Fire()
    {
        throw new Exception("Exception");
    }

    public void FireAndHandle()
    {
        try
        {
            throw new Exception("Exception");
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        } 
    }
}
public class Program
{
    public static void WriteX()
    {
        for(int i = 0; i < 100; i++)
            Console.Write('X');  
    }
    public static void WriteY()
    {
        for(int i = 0; i < 100; i++)
            Console.Write('Y');
    }
    public static void PrintMessage(string message) => Console.WriteLine(message);
    public static void Main(string[] args)
    {
        Thread thread = new(WriteY);
        thread.Start();
        WriteX();
        // the above will print X and Y randomly

        Console.WriteLine("\nWaiting for a thread completion");

        Thread t2 = new(WriteY);
        t2.Start();
        t2.Join(); // using join waits for a thread completion
        Console.WriteLine("\nThread t2 ended execution");
        Console.WriteLine("Main thread is going to sleep for 5 secs now");
        //Thread.Sleep(5000); // using Thread.Sleep pauses the current thread for a specified period
        Console.WriteLine("Main thread woke up after 5 seconds");
        WriteX();

        Console.WriteLine("\nProblem with shared state of memory");
        Test test = new();
        Thread t3 = new(test.Go);
        t3.Start();
        test.Go();

        test.Reset();

        Console.WriteLine("\nExclusive Lock and thread safety");
        Thread t4 = new(test.SafeGo);
        t4.Start();
        test.SafeGo();

        // Passing data to thread //
        //By calling the method via a lamda function
        Thread t5 = new(() => PrintMessage("Custom message from lamda"));
        t5.Start();
        //Via a captured variable
        string mySecretMessage = "Batman killed joker";
        Thread t6 = new(() => PrintMessage(mySecretMessage));
        t6.Start();

        // exception handling when using threads
        try
        {
            Thread t7 = new(test.Fire);
            t7.Start();
        }
        catch(Exception ex)
        {
            // execution will never reach this block of code, instead it will throw exception in it's execution thread 
            Console.WriteLine(ex.Message);
        }

        Thread t8 = new(test.FireAndHandle);
        // Executing this will work as the exception is thrown and catch in the execution thread
        t8.Start();
    }
}

