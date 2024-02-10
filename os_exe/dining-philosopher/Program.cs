class DiningPhilosopher
{
    int n;
    Mutex mutex;
    List<SemaphoreSlim> chopsticksSem;
    ManualResetEvent manualReset;

    int left(int p) => p; 
    int right(int p) => (p - 1 + n) % n;
    void Think(int p)
    {
        Console.WriteLine($"Philosopher {p} is thinking");
        Thread.Sleep(1000);
    }
    void GrabChopstick(int p)
    {
        // only one philosopher can grab chopstick at a time
        bool success = false;
        mutex.WaitOne();
        SemaphoreSlim lc = chopsticksSem[left(p)];
        SemaphoreSlim rc = chopsticksSem[right(p)];
        if(lc.CurrentCount > 0 && rc.CurrentCount > 0) // we can pick both the chopstics
        {
            Console.WriteLine($"Philosopher {p} is grabbing chopsticks {left(p)} and {right(p)}");
            lc.Wait();
            rc.Wait();
            success = true;
        }
        mutex.ReleaseMutex();
        if(!success)
        {
            Console.WriteLine($"Philosopher {p} is hungry waiting");
            manualReset.WaitOne();
            GrabChopstick(p);
        }
    }
    void Eating(int p)
    {
        Console.WriteLine($"Philosopher {p} is eating");
        Thread.Sleep(2000);
        // put down the chopsticks
        chopsticksSem[left(p)].Release();
        chopsticksSem[right(p)].Release();
        Console.WriteLine($"Philosopher {p} has finished eating");
        // now signal other philosophers so that they can try eating again
        manualReset.Set();
        manualReset.Reset();
    }
    void Run(int p)
    {
        while(true)
        {
            Think(p);
            GrabChopstick(p);
            Eating(p);
        }
    }
    public void Go()
    {
        for(int i = 0; i < n; i++)
        {
            int curr = i;
            Thread t = new(() => Run(curr));
            t.Start();
        }
    }
    public DiningPhilosopher(int num)
    {
        // for n number of philosophers n chopsticks are given
        n = num;
        mutex = new();
        chopsticksSem = new();
        for(int i = 0; i < n; i++)
            chopsticksSem.Add(new (1, 1));
        manualReset = new(false);
    }   
}

class Program
{
    public static void Main(string[] args)
    {
        DiningPhilosopher runner = new(5);
        runner.Go();
    }
}