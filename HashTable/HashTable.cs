using System.Collections;
using System.Text;

namespace HashTableTask;

public class HashTable<T> : ICollection<T>
{
    private List<T>?[] _buckets;

    public int Count { get; private set; }

    private int _modCount;

    public bool IsReadOnly => false;

    public HashTable()
    {
        _buckets = new List<T>[10];
    }

    public HashTable(int size)
    {
        if (size <= 0)
        {
            throw new ArgumentException($"The size ({size}) must be greater than 0.", nameof(size));
        }

        _buckets = new List<T>[size];
    }

    private int GetIndex(T item)
    {
        if (item is not null)
        {
            return Math.Abs(item.GetHashCode() % _buckets.Length);
        }

        return 0;
    }

    public void Add(T item)
    {
        int finalIndex = GetIndex(item);
        List<T>? bucket = _buckets[finalIndex];

        if (bucket is not null)
        {
            bucket.Add(item);
        }
        else
        {
            _buckets[finalIndex] = [item];
        }

        Count++;
        _modCount++;
    }

    public void Clear()
    {
        if (Count == 0)
        {
            return;
        }

        foreach (List<T>? bucket in _buckets)
        {
            if (bucket is not null)
            {
                bucket?.Clear();
            }
        }

        Count = 0;
        _modCount++;
    }

    public bool Contains(T item)
    {
        List<T>? bucket = _buckets[GetIndex(item)];
        return bucket?.Contains(item) ?? false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (arrayIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex), $"The index ({arrayIndex}) must be greater or equal to 0.");
        }

        if (array.Length - arrayIndex < Count)
        {
            throw new ArgumentException($"The number of elements in the source list ({Count}) must be less than or equal to available space from arrayIndex ({arrayIndex}) to the end of the destination array ({array.Length}).");
        }

        int arrayIndexCopy = arrayIndex;

        foreach (T? bucket in this)
        {
            array[arrayIndexCopy] = bucket;
            arrayIndexCopy++;
        }
    }

    public bool Remove(T item)
    {
        int finalIndex = GetIndex(item);
        List<T>? bucket = _buckets[finalIndex];

        if (bucket is null)
        {
            return false;
        }

        bool isRemoved = bucket.Remove(item);
        Count--;
        _modCount++;

        return isRemoved;
    }

    public IEnumerator<T> GetEnumerator()
    {
        int initialModCount = _modCount;

        foreach (List<T>? bucket in _buckets)
        {
            if (initialModCount != _modCount)
            {
                throw new InvalidOperationException("The list should not change while the enumerator is running.");
            }

            if (bucket is not null)
            {
                IEnumerator<T> enumerator = bucket.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new();
        stringBuilder.Append('[');

        foreach (T item in this)
        {
            stringBuilder.Append(item).Append(", ");
        }

        if (stringBuilder.Length > 1 & stringBuilder.Length > 0)
        {
            stringBuilder.Length -= 2;
        }

        stringBuilder.Append(']');
        return stringBuilder.ToString();
    }
}
