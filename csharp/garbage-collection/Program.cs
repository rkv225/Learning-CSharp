using System;

public class ResourceHolder : IDisposable
{
    // to ensure that the dispose don't run multiple times even if called
    private bool disposed = false;
    private int resource;

    public ResourceHolder()
    {
        resource = 10;
        Console.WriteLine("ResourceHolder created.");
    }

    // finalizer method defined by class name prefixed with a ~ . Also this can't have any args
    // finalizer is called on garbage collection
    ~ResourceHolder()
    {
        // Finalizer (destructor)
        Dispose(false);
        Console.WriteLine("Finalizer called.");
    }

    // dispose is called immidiately upon closing using statement or on explicit call
    public void Dispose()
    {
        // Dispose method
        Dispose(true);
        GC.SuppressFinalize(this); // as we have cleaned up the resources so we don't need to run finalizer for this. This will ensure cleanup in single pass
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                // Dispose managed resources (if any) i.e. call dispose on any props or fields implementing IDisposable
                Console.WriteLine("Disposing managed resources.");
            }
            disposed = true;
            Console.WriteLine("ResourceHolder disposed.");
        }
    }
}

class Program
{
    static void CreateAndDispose()
    {
        // Explicitly calling Dispose (not a recommended approach)
        ResourceHolder anotherResourceHolder = new ResourceHolder();
        anotherResourceHolder.Dispose(); // as we have called dispose finalizer won't be called
    }

    static void CreateAndLeave()
    {
        ResourceHolder anotherResourceHolder = new ResourceHolder();
    }
    static void Main()
    {
        // by encapsulating your code in using blocks we need not to worry about calling the dispose after the use. 
        // CLR automatically takes care of the resources by calling dispose on objects implementing IDisposable
        // Creating an instance of ResourceHolder
        // recommended approach
        Console.WriteLine("creating and disposing within using statements");
        using (ResourceHolder resourceHolder = new ResourceHolder())
        {
            // Using the resource holder
            Console.WriteLine("Using the resource holder.");
        }

        Console.WriteLine("\nCalling create and dispose");
        CreateAndDispose();
        
        Console.WriteLine("\nCalling another create and not disposing");
        CreateAndLeave();
        Console.WriteLine("garbage collector called"); // just for demonstration -- not recommended
        GC.Collect(); 
        GC.WaitForPendingFinalizers(); // Ensure finalizers are executed

        // adding an additional statement just because the console will get closed when the gc will run
        Console.WriteLine("\nThe End");
    }
}

/*
Additional notes:
The garbage collector (GC) in C# is responsible for automatically managing memory by reclaiming memory occupied by objects that are no longer in use or reachable by the application. The garbage collector operates as a background process within the .NET runtime, and its execution involves several phases. Here is an overview of how the garbage collector runs in C#:
1. Memory Allocation:
When an application allocates memory for objects, the memory is allocated on the managed heap, which is divided into segments. Objects are created in the younger generation (Gen 0). The garbage collector assumes that most objects have short lifetimes, so it frequently collects and reclaims memory in the younger generation.
2. Generational Garbage Collection:
The garbage collector uses a generational approach, dividing objects into three generations: Gen 0, Gen 1, and Gen 2. New objects are initially allocated in Gen 0. If an object survives a garbage collection in Gen 0, it is promoted to Gen 1. Similarly, objects surviving Gen 1 collections are promoted to Gen 2. The older generations are collected less frequently. Additionally there is a large object heap (LOH) which is used storing large objects which are greaer than a specific threshold value.
3. Garbage Collection Triggers: 
Garbage collection is triggered when the system determines that there is a need to reclaim memory. Triggers include: 
    - Allocation Failure: When the application tries to allocate memory for a new object, and there is not enough space in the current generation, a garbage collection is triggered.
    - Idle Time: The garbage collector may run during periods of low application activity or when the system is idle.
4. Mark and Sweep Phases: 
The garbage collection process involves two main phases: marking and sweeping.
    - Marking Phase: The garbage collector identifies and marks reachable objects by starting from known roots (such as global variables, local variables, and static fields) and recursively traversing object references. Unreachable objects are left unmarked.
    - Sweeping Phase: The garbage collector frees memory occupied by unmarked (unreachable) objects. Objects in the younger generations that survive the collection are promoted to the next generation.
5. Compact Phase (Optional):
In addition to marking and sweeping, the garbage collector may perform a compacting phase in which it compacts the memory by moving objects closer together. Compaction helps reduce fragmentation and can improve memory locality.
6. Finalization:
Objects with finalizers (destructors) are not immediately reclaimed. Instead, they are put in a finalization queue. The garbage collector runs finalizers on objects in the finalization queue before reclaiming the objects. However, relying on finalizers for resource cleanup is discouraged, and the recommended approach is to implement the IDisposable pattern.
7.  GC.Collect Method (Optional):
While the garbage collector operates automatically, developers can explicitly request garbage collection using the GC.Collect method. However, manually invoking garbage collection is generally discouraged unless there are specific reasons for doing so.
// Explicitly trigger garbage collection (not recommended in most cases)
GC.Collect();

In summary, the garbage collector in C# operates in the background, using generational garbage collection to efficiently manage memory. It employs a combination of automatic and deterministic processes to reclaim memory and ensure that the application runs efficiently and without memory leaks. Developers typically don't need to intervene with the garbage collector, as it is designed to work automatically and efficiently.
*/