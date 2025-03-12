using System.Collections;

namespace HashTableTask;

class HashTable<T> : ICollection<T>
{
    private List<T>?[] _buckets;

    public int Count { get; private set; }

    private int Capacity
    {
        set
        {
            Array.Resize(ref _buckets, value);
        }
    }

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

            if (Count >= _buckets.Length)
            {
                ExpandAndRehash();
            }

            if (value is not null)
            {
                List<T>? values = _buckets[hashIndex];

                if (values is null)
                {
                    _buckets[hashIndex] = value;
                    Count++;
                }
                else if (!values.Equals(value))
                {
                    values.AddRange(value);
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

    private void ExpandAndRehash()
    {
        int oldLength = _buckets.Length;
        Capacity = _buckets.Length * 2;

        for (int i = 0; i < oldLength; i++)
        {
            List<T>? values = _buckets[i];

            if (values is null || values.Count <= 1)
            {
                continue;
            }

            foreach (T value in values)
            {
                int newHashIndex = GetHashIndex(value!.GetHashCode());

                if (newHashIndex != i)
                {
                    _buckets[newHashIndex] = [value];
                }
            }
        }
    }

    public void Add(T item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        if (Count >= _buckets.Length)
        {
            ExpandAndRehash();
        }

        int hashIndex = GetHashIndex(item.GetHashCode());

        List<T>? values = _buckets[hashIndex];

        if (values is not null)
        {
            if (!values.Contains(item))
            {
                values.Add(item);
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

        List<T>? values = _buckets[GetHashIndex(item.GetHashCode())];

        if (values is null)
        {
            return false;
        }

        return values.Contains(item);
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
        List<T>? values = _buckets[hashIndex];

        if (values is null)
        {
            return false;
        }

        bool isDeleted = values.Remove(item);

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
            List<T>? values = _buckets[i];

            if (values is not null)
            {
                List<T>.Enumerator enumerator = values.GetEnumerator();

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
