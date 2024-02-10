class Program
{
    static Queue<int> buffer = new();
    static Mutex mutex = new(); // mutex for exclusive operations on buffer
    static SemaphoreSlim full = new(0, 3); // initial 0 and max 3
    static SemaphoreSlim empty = new(3, 3); // initial 3 and max 3
    static int dd = 1;
    class Producer
    {
        public void Start()
        {
            while(true)
            {
                Put(dd);
                dd++;
                if(dd > 10) break;
            }
        }
        private void Put(int x)
        {
            empty.Wait(); // wait on buffer increment the count and put the data to buffer till it's not full
            mutex.WaitOne();
            Thread.Sleep(1000);
            Console.WriteLine($"Writing {x} to buffer");
            buffer.Enqueue(x);
            Thread.Sleep(1000);
            mutex.ReleaseMutex();
            full.Release();
        }
    }
    class Consumer
    {
        public void Start()
        {
            while(true)
            {
                Get();
                if(dd > 10 && buffer.Count == 0) break;
            }
        }
        private void Get()
        {
            full.Wait(); // wait until we have some value in buffer
            mutex.WaitOne();
            Thread.Sleep(1000);
            Console.WriteLine($"Reading {buffer.Dequeue()} from buffer");
            Thread.Sleep(1000);
            mutex.ReleaseMutex();
            empty.Release();
        }
    }
    public static void Main(string[] args) 
    {
        Producer producer = new();
        Consumer consumer = new();
        Thread pt = new(producer.Start);
        Thread ct = new(consumer.Start);
        pt.Start();
        ct.Start();
    }
}