class TwoWaySignaling
{
    static EventWaitHandle _waitHandle = new AutoResetEvent(false);

    static EventWaitHandle _ready = new AutoResetEvent(false);
    static EventWaitHandle _go = new AutoResetEvent(false);
    static readonly object _locker = new object();
    static string? _message;

    static ManualResetEvent manualResetEvent = new ManualResetEvent(false);

    // To use the class, instantiate it with the number of threads, or “counts,” that you want to wait on
    static CountdownEvent countdown = new(3); // Initialize with "count" of 3.

    static void Main()
    {
        // AutoResetEvent
        
        // An AutoResetEvent is like a ticket turnstile: inserting a ticket lets exactly one person through.
        // It reset after a single waiting thread is released.
        Console.WriteLine("Auto Reset event Basic");
        new Thread(Waiter).Start();
        Thread.Sleep(1000); // Pause for a second...
        _waitHandle.Set(); // Wake up the Waiter.
        // Output:
        // Waiting... (pause) Notified.

        Thread.Sleep(2000); // added for better separated output to console

        Console.WriteLine("Auto Reset Event 2 way signalling");
        new Thread(Work).Start();
        _ready.WaitOne(); // First wait until worker is ready
        lock (_locker) _message = "ping";
        _go.Set(); // Tell worker to go
        _ready.WaitOne();
        lock (_locker) _message = "pong"; // Give the worker another message
        _go.Set();
        _ready.WaitOne();
        lock (_locker) _message = null; // Signal the worker to exit
        _go.Set();
        // Output:
        // ping
        // pong

        // Manual Reset event
        
        Console.WriteLine("Manual Reset Event");
        // Create multiple threads that will wait for the event
        Thread t1 = new Thread(WaiterThread);
        Thread t2 = new Thread(WaiterThread);
        Thread t3 = new Thread(WaiterThread);

        // Start the threads
        t1.Start();
        t2.Start();
        t3.Start();

        // Simulate some work before signaling the event
        Thread.Sleep(2000);

        // Signal the event, releasing all waiting threads
        Console.WriteLine("Signaling the event.");
        manualResetEvent.Set();

        // Wait for the threads to finish
        t1.Join();
        t2.Join();
        t3.Join();

        Console.WriteLine("All threads have finished.");

        // CountdownEvent
        Console.WriteLine("Countdown Event");
        new Thread (SaySomething).Start ("I am thread 1");
        new Thread (SaySomething).Start ("I am thread 2");
        new Thread (SaySomething).Start ("I am thread 3");
        countdown.Wait(); // Blocks until Signal has been called 3 times
        Console.WriteLine ("All threads have finished speaking!");
    }

    static void Waiter()
    {
        Console.WriteLine("Waiting...");
        _waitHandle.WaitOne(); // Wait for notification
        Console.WriteLine("Notified");
    }
    static void Work()
    {
        while (true)
        {
            _ready.Set(); // Indicate that we're ready
            _go.WaitOne(); // Wait to be kicked off...
            lock (_locker)
            {
                if (_message == null) return; // Gracefully exit
                Console.WriteLine(_message);
            }
        }
    }
    static void WaiterThread()
    {
        Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is waiting for the event.");

        // Wait for the event to be signaled
        manualResetEvent.WaitOne();

        Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} has been released.");

        // Simulate some work after the event is signaled
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is working.");
            Thread.Sleep(1000);
        }
    }
    static void SaySomething(object thing)
    {
        Thread.Sleep(1000);
        Console.WriteLine(thing);
        // Calling Signal decrements the “count”; calling Wait blocks until the count goes down to zero
        countdown.Signal();
    }
}

/*
Notes:

Signaling and spinning are two different approaches to synchronization in multithreaded programming.

Signaling:
Definition:

Signaling involves notifying a waiting thread that a particular condition has been met, and it can proceed with its task.
Usage:

Commonly used with synchronization primitives like mutexes, semaphores, and events.
Threads waiting on an event or condition are blocked until the event is signaled.
Advantages:

Efficient use of system resources because waiting threads are in a dormant state until they are signaled.
Suitable for scenarios where threads need to wait for some condition to be true before proceeding.
Disadvantages:

There might be a latency between the event occurrence and the signaling, which can introduce some delay in the waiting threads' response.
Spinning:
Definition:

Spinning involves repeatedly checking a condition in a loop until it becomes true.
Usage:

Typically used in scenarios where the expected wait time is very short.
A thread keeps checking a condition in a loop instead of entering a blocked state.
Advantages:

Can be more responsive in situations where the expected wait time is very short.
Avoids the overhead of putting threads to sleep and waking them up.
Disadvantages:

Consumes CPU cycles actively, leading to higher CPU usage.
Not suitable for scenarios with long wait times, as it can lead to unnecessary resource consumption.
When to Choose:
Signaling:

Use signaling when threads need to wait for a condition that might take some time to be true.
Suitable for scenarios where threads can remain in a dormant state until the condition is met.
Spinning:

Use spinning when the expected wait time is very short, and the overhead of putting threads to sleep and waking them up is not desirable.
Suitable for scenarios where threads can actively check and respond to a condition quickly.
Hybrid Approaches:
Combining Signaling and Spinning:
In some scenarios, a combination of signaling and spinning might be used. For example, a thread might initially spin for a short duration, and if the condition is not met, it could switch to a signaling mechanism.
It's important to choose the appropriate synchronization approach based on the specific requirements and characteristics of the application. The choice may depend on factors such as the expected wait time, the frequency of condition checking, and the impact on system resources.
*/