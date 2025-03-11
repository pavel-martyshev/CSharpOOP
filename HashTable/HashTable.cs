using System.Collections;

namespace HashTableTask;

class HashTable<T> : ICollection<T>
{
    private List<T>?[] _buckets;

    public int Count { get; private set; }

    public bool IsReadOnly => false;

    public HashTable()
    {
        _buckets = new List<T>[10];
        Count = 0;
    }

    public HashTable(int size)
    {
        if (size < 0)
        {
            throw new ArgumentException("The size must be greater than 0.", nameof(size));
        }

        _buckets = new List<T>[size];
        Count = 0;
    }

    public List<T>? this[int hash]
    {
        get
        {
            int hashIndex = GetHashIndex(hash);

            if (hashIndex < 0 || hashIndex >= _buckets.Length)
            {
                throw new IndexOutOfRangeException($"The index must be greater than 0 and less than the length of the list ({_buckets.Length}).");
            }

            return _buckets[hashIndex];
        }

        set
        {
            int hashIndex = GetHashIndex(hash);

            if (hashIndex < 0 || hashIndex > _buckets.Length)
            {
                throw new IndexOutOfRangeException($"The index must be greater than 0 and less than or equal to the length of the list ({_buckets.Length}).");
            }

            if (value is not null)
            {
                List<T>? list = _buckets[hashIndex];

                if (list is null)
                {
                    _buckets[hashIndex] = value;
                    Count++;
                }
                else if (!list.Equals(value))
                {
                    list.AddRange(value);
                    Count++;
                }
            }
            else
            {
                _buckets[hashIndex] = default;
                Count--;
            }
        }
    }

    private int GetHashIndex(int hash) => Math.Abs(hash % _buckets.Length);

    public void Add(T item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        int hashIndex = GetHashIndex(item.GetHashCode());

        List<T>? list = _buckets[hashIndex];

        if (list is not null)
        {
            if (!list.Contains(item))
            {
                list.Add(item);
                Count++;
            }
        }
        else
        {
            _buckets[hashIndex] = [item];
            Count++;
        }
    }

    public void Clear()
    {
        _buckets = new List<T>[10];
        Count = 0;
    }

    public bool Contains(T item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        List<T>? list = _buckets[GetHashIndex(item.GetHashCode())];

        if (list is null)
        {
            return false;
        }

        return list.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (arrayIndex < 0 || arrayIndex >= array.Length)
        {
            throw new IndexOutOfRangeException($"The index must be greater 0 and less than the length of the array ({array.Length}).");
        }

        for (int i = 0; i < Count; i++)
        {
            array.SetValue(_buckets[i], arrayIndex++);
        }
    }

    public bool Remove(T item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        int hashIndex = GetHashIndex(item.GetHashCode());
        List<T>? list = _buckets[hashIndex];

        if (list is null)
        {
            return false;
        }

        bool isDeleted = list.Remove(item);

        if (isDeleted == true)
        {
            _buckets[hashIndex] = default;
            Count--;
        }

        return isDeleted;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _buckets.Length; ++i)
        {
            List<T>? list = _buckets[i];

            if (list is not null)
            {
                List<T>.Enumerator enumerator = list.GetEnumerator();

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
}
