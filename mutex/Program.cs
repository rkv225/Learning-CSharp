using System;
using System.Threading;

class Program
{
    static Mutex mutex = new Mutex(); // Global mutex

    static void Main()
    {
        // Create multiple threads that will try to access a shared resource
        Thread t1 = new Thread(WorkerThread);
        Thread t2 = new Thread(WorkerThread);

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();

        Console.WriteLine("All threads have finished.");
    }

    static void WorkerThread()
    {
        Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is waiting to enter the critical section.");

        // Wait for mutex (enter the critical section)
        mutex.WaitOne();

        try
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} has entered the critical section.");

            // Simulate some work in the critical section
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is working.");
                Thread.Sleep(1000);
            }
        }
        finally
        {
            // Release the mutex (exit the critical section)
            mutex.ReleaseMutex();
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} has exited the critical section.");
        }
    }
}

/*
Notes:
- Mutex:
A Mutex can be system-wide or application-wide, allowing synchronization across different processes. It can be used for inter-process synchronization.
- Lock:
The lock statement is limited to the scope of the current process or application domain. The lock statement is not designed for cross-process synchronization.
*/
