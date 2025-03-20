using System.Collections;
using System.Text;

namespace ArrayList;

public class List<T> : IList<T>
{
    private T[] _items;

    private int _modCount;

    public int Count { get; private set; }

    private int Capacity
    {
        get
        {
            return _items.Length;
        }

        set
        {
            if (value < Count)
            {
                throw new ArgumentException($"The capacity cannot be less than the elements count ({Count}).", nameof(value));
            }

            Array.Resize(ref _items, value);
        }
    }

    public bool IsReadOnly => false;

    public List()
    {
        _items = new T[10];
    }

    public List(int capacity)
    {
        if (capacity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity), "The capacity must not be less than 0.");
        }

        _items = new T[capacity];
    }

    public List(T[] items)
    {
        _items = new T[items.Length];
        Array.Copy(items, _items, items.Length);

        Count = items.Length;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"The index ({index}) must be greater than 0 and less than the length ({Count}).");
            }

            return _items[index];
        }
        set
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"The index ({index}) must be greater than 0 and less than the length ({Count}).");
            }

            _items[index] = value;
            _modCount++;
        }
    }

    public void TrimExcess()
    {
        if ((double)Count / _items.Length * 100 <= 10)
        {
            Capacity = Count;
        }
    }

    public int IndexOf(T item)
    {
        for (int i = 0; i < Count; i++)
        {
            if (item.Equals(_items[i]))
            {
                return i;
            }
        }

        return -1;
    }

    public void Insert(int index, T item)
    {
        if (index < 0 || index > Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), $"The index ({index}) must be greater than 0 and less than or equal to the length ({Count}).");
        }

        if (Count >= _items.Length)
        {
            Capacity *= 2;
        }

        Array.Copy(_items, index, _items, index + 1, Count - index);
        _items[index] = item;

        Count++;
        _modCount++;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), $"The index ({index}) must be greater than 0 and less than the length ({Count}).");
        }

        Array.Copy(_items, index + 1, _items, index, Count - index - 1);

        _items[Count - 1] = default!;
        Count--;
        _modCount++;
    }

    public void Add(T item)
    {
        if (Count >= _items.Length)
        {
            Capacity *= 2;
        }

        _items[Count] = item;
        Count++;
        _modCount++;
    }

    public void Clear()
    {
        if (Count == 0)
        {
            return;
        }

        Array.Clear(_items, 0, Count);
        Count = 0;
    }

    public bool Contains(T item)
    {
        if (IndexOf(item) == -1)
        {
            return false;
        }

        return true;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        ArgumentNullException.ThrowIfNull(array, nameof(array));

        if (arrayIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex), $"The index ({arrayIndex}) must be greater 0.");
        }

        if (array.Length - arrayIndex < Count)
        {
            throw new ArgumentException($"The number of elements in the source list ({Count}) must be less than or equal to available space from arrayIndex ({arrayIndex}) to the end of the destination array ({array.Length}).");
        }

        int arrayIndexCopy = arrayIndex;

        foreach (T item in this)
        {
            array[arrayIndexCopy] = item;
            arrayIndexCopy++;
        }
    }

    public bool Remove(T item)
    {
        int itemIndex = IndexOf(item);

        if (itemIndex >= 0)
        {
            RemoveAt(itemIndex);
            return true;
        }

        return false;
    }

    public IEnumerator<T> GetEnumerator()
    {
        int initialModCount = _modCount;

        for (int i = 0; i < Count; ++i)
        {
            if (initialModCount != _modCount)
            {
                throw new InvalidOperationException("The list should not change while the enumerator is running.");
            }

            yield return _items[i];
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

        foreach (T item in _items)
        {
            if (item is not null)
            {
                stringBuilder.Append(item).Append(", ");
            }
        }

        if (stringBuilder.Length > 1 & stringBuilder.Length > 0)
        {
            stringBuilder.Length -= 2;
        }

        stringBuilder.Append(']');

        return stringBuilder.ToString();
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(obj, this))
        {
            return true;
        }

        if (obj is null || GetType() != obj.GetType())
        {
            return false;
        }

        List<T> list = (List<T>)obj;

        foreach (T item in list)
        {
            if (!Contains(item))
            {
                return false;
            }
        }

        return true;
    }

    public override int GetHashCode()
    {
        const int prime = 31;
        int hash = 1;

        foreach (T item in _items)
        {
            if ((item is not null))
            {
                hash = prime * hash + item.GetHashCode();
            }
        }

        return hash;
    }
}
