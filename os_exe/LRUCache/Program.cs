// In this cache repacement policy, the key that is least recently used will be evicted in case of page fault occurs and no free frame is present.
public class Node<T>
{
    public T Key;
    public T Value;
    public Node<T>? Next;
    public Node<T>? Prev;
}
public class DoublyLinkedList<T>
{
    Node<T>? _head;
    Node<T>? _tail;
    public DoublyLinkedList()
    {
        _head = null;
    }
    public Node<T> PushFront(T k, T v)
    {
        var node = new Node<T>(){ Key = k, Value = v, Next = _head, Prev = null};
        if(_head != null)
            _head.Prev = node;
        _head = node;
        if(_tail == null)
            _tail = _head;
        return node;
    }
    public void RemoveNode(Node<T> node)
    {
        Node<T>? prev = node.Prev;
        Node<T>? next = node.Next;
        if(prev != null) prev.Next = next;
        if(next != null) next.Prev = prev;
        if(_tail == node)
            _tail = prev;
        if(_head == node)
            _head = next;
    }
    public T RemoveTail()
    {
        if(_tail != null)
        {
            T k = _tail.Key;
            _tail = _tail.Prev;
            return k;
        }
        return (T)(Object)(-1);
    }
}

public class LRUCache {
    Dictionary<int, Node<int>> _cache;
    DoublyLinkedList<int> _list;
    int _maxCapacity;
    public LRUCache(int capacity) {
        _maxCapacity = capacity;
        _cache = new();
        _list = new();
    }
    
    public int Get(int key) {
        if(_cache.TryGetValue(key, out Node<int> node))
        {
             // delete from list
            _list.RemoveNode(node);
            // push to front of list and update the dictionary
            _cache[key] = _list.PushFront(key, node.Value);
            return node.Value;
        }
        else
            return -1;
    }
    
    public void Put(int key, int value) {
        if(_cache.TryGetValue(key, out Node<int> node)) // key exisits in cache
        {
            // delete from list
            _list.RemoveNode(node);
            // push to front of list and update the dictionary
            _cache[key] = _list.PushFront(key, value);
        }
        else if(_cache.Count < _maxCapacity) // cache has space
        {
            _cache[key] = _list.PushFront(key, value);
        }
        else // page replacement
        {
            // replace the LRU page
            int k = _list.RemoveTail();
            _cache.Remove(k);

            // push to front of list and update the dictionary
            _cache[key] = _list.PushFront(key, value);
        }
    }
}

class Program
{
    public static void Main(string[] args)
    {
        LRUCache lRUCache = new(2);
        lRUCache.Put(1,1);
        lRUCache.Put(2,2);
        Console.WriteLine(lRUCache.Get(1));
        lRUCache.Put(3,3);
        Console.WriteLine(lRUCache.Get(2));
        lRUCache.Put(4,4);
        Console.WriteLine(lRUCache.Get(1));
        Console.WriteLine(lRUCache.Get(3));
        Console.WriteLine(lRUCache.Get(4));
    }
}