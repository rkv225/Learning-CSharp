using System;
using System.Collections;
using System.Collections.Generic;

public class MyCollection : IEnumerable<int>
{
    private int[] items;

    public MyCollection()
    {
        // Initialize your collection here
        items = new int[] { 1, 2, 3, 4, 5 };
    }

    public IEnumerator<int> GetEnumerator()
    {
        // Return the enumerator that iterates through the collection
        return new MyEnumerator(items);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        // Explicit interface implementation for non-generic IEnumerable
        return GetEnumerator();
    }

    // Custom enumerator class
    private class MyEnumerator : IEnumerator<int>
    {
        private int[] items;
        private int currentIndex = -1;

        public MyEnumerator(int[] collection)
        {
            items = collection;
        }

        public int Current
        {
            get
            {
                // Implement the logic to return the current element
                return items[currentIndex];
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            // Cleanup resources, if needed
        }

        public bool MoveNext()
        {
            // Move to the next element in the collection
            currentIndex++;
            return currentIndex < 5;
        }

        public void Reset()
        {
            // Reset the enumerator to its initial state
            currentIndex = -1;
        }
    }
}

class Program
{
    static void Main()
    {
        MyCollection myCollection = new MyCollection();

        // Use the collection in foreach
        foreach (var item in myCollection)
        {
            Console.WriteLine(item);
        }
    }
}
