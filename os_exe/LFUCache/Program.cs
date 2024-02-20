/*
Problem:
Design and implement a data structure for a Least Frequently Used (LFU) cache.

Implement the LFUCache class:
LFUCache(int capacity) Initializes the object with the capacity of the data structure.
int get(int key) Gets the value of the key if the key exists in the cache. Otherwise, returns -1.
void put(int key, int value) Update the value of the key if present, or inserts the key if not already present. When the cache reaches its capacity, it should invalidate and remove the least frequently used key before inserting a new item. For this problem, when there is a tie (i.e., two or more keys with the same frequency), the least recently used key would be invalidated.
To determine the least frequently used key, a use counter is maintained for each key in the cache. The key with the smallest use counter is the least frequently used key.
When a key is first inserted into the cache, its use counter is set to 1 (due to the put operation). The use counter for a key in the cache is incremented either a get or put operation is called on it.
The functions get and put must each run in O(1) average time complexity.

Example 1:

Input
["LFUCache", "put", "put", "get", "put", "get", "get", "put", "get", "get", "get"]
[[2], [1, 1], [2, 2], [1], [3, 3], [2], [3], [4, 4], [1], [3], [4]]
Output
[null, null, null, 1, null, -1, 3, null, -1, 3, 4]

Explanation
// cnt(x) = the use counter for key x
// cache=[] will show the last used order for tiebreakers (leftmost element is  most recent)
LFUCache lfu = new LFUCache(2);
lfu.put(1, 1);   // cache=[1,_], cnt(1)=1
lfu.put(2, 2);   // cache=[2,1], cnt(2)=1, cnt(1)=1
lfu.get(1);      // return 1
                 // cache=[1,2], cnt(2)=1, cnt(1)=2
lfu.put(3, 3);   // 2 is the LFU key because cnt(2)=1 is the smallest, invalidate 2.
                 // cache=[3,1], cnt(3)=1, cnt(1)=2
lfu.get(2);      // return -1 (not found)
lfu.get(3);      // return 3
                 // cache=[3,1], cnt(3)=2, cnt(1)=2
lfu.put(4, 4);   // Both 1 and 3 have the same cnt, but 1 is LRU, invalidate 1.
                 // cache=[4,3], cnt(4)=1, cnt(3)=2
lfu.get(1);      // return -1 (not found)
lfu.get(3);      // return 3
                 // cache=[3,4], cnt(4)=1, cnt(3)=3
lfu.get(4);      // return 4
                 // cache=[4,3], cnt(4)=2, cnt(3)=3

Solution:
Keep element key, value and frequency  in a dictionary.
Keep a frequency dictionary to with frequency as key and a list of values with the frequency in it's value.
If value is present we remove it from freq map and reinsert it after incrementing the frequency
if the capacity is full, we get the first value from freq map and then remove the first element as it will be the least frequently used.

Complexity: As it not possible to solve this in O(1). Sorted dictionary has O(log n) complexity and a list remove works in O(capacity).
So we can assume that it totally depends on whether log n or capcity of individual frequency

Link: https://leetcode.com/problems/lfu-cache
*/

public class LFUCache {
    Dictionary<int, CacheElement> cache;
    SortedDictionary<int, List<int>> freqKeysMap;
    int capacity;

    public LFUCache(int capacity) {
        this.capacity = capacity;
        cache = new();
        freqKeysMap = new();
    }
    
    public int Get(int key) {
        if(cache.ContainsKey(key)){
            RemoveFreqKey(cache[key].Frequency, key);
            AddFreqKey(++cache[key].Frequency, key);
            return cache[key].Value;
        }
        return -1;
    }
    
    public void Put(int key, int value) {
        if(capacity == 0)
            return;

        if(cache.ContainsKey(key)){
            RemoveFreqKey(cache[key].Frequency, key);
            AddFreqKey(++cache[key].Frequency, key);
            cache[key].Value = value;
            return;
        }

        if(cache.Count < capacity) {
            cache.Add(key, new CacheElement(key, value, 1));
            AddFreqKey(1, key);
            return;
        }

        var elementToRemove = freqKeysMap.First();
        var keyToRemove = elementToRemove.Value[0];
        cache.Remove(keyToRemove);
        RemoveFreqKey(elementToRemove.Key, keyToRemove);
        cache.Add(key, new CacheElement(key, value, 1));
        AddFreqKey(1, key);
    }
    
    private void RemoveFreqKey(int freq, int key){
        if(freqKeysMap[freq].Count == 1){
            freqKeysMap.Remove(freq);
        }
        else{
            freqKeysMap[freq].Remove(key);
        }
    }

    private void AddFreqKey(int freq, int key){
        if(freqKeysMap.ContainsKey(freq)){
            freqKeysMap[freq].Add(key);
        }
        else{
            freqKeysMap.Add(freq, new List<int>(){ key });
        }
    }
}

public class CacheElement{
    public int Key;
    public int Value;
    public int Frequency;

    public CacheElement(int key, int value, int frequency){
        Key = key;
        Value = value;
        Frequency = frequency;
    }
}

class Program
{
    public static void Main(string[] args)
    {
        LFUCache lFUCache = new(2);
    }
}
