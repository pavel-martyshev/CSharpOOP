using System.Collections;
using System.Text;

namespace ArrayList;

public class List<T> : IList<T>
{
    private T[] _items;

    private int _modCount;

    public int Count { get; private set; }

    public int Capacity
    {
        get => _items.Length;

        set
        {
            if (value < Count)
            {
                throw new ArgumentException($"The capacity ({value}) cannot be less than the elements count ({Count}).", nameof(value));
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
            throw new ArgumentOutOfRangeException(nameof(capacity), $"The capacity ({capacity}) must not be less than 0.");
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
            ValidateIndex(index);

            return _items[index];
        }
        set
        {
            ValidateIndex(index);

            _items[index] = value;
            _modCount++;
        }
    }

    private void ValidateIndex(int index)
    {
        if (index < 0 || index >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), $"The index ({index}) must be greater than or equal to 0 and less than the elements count ({Count}).");
        }
    }

    public void TrimExcess()
    {
        if (Count <= _items.Length * 0.9)
        {
            Capacity = Count;
        }
    }

    public int IndexOf(T item)
    {
        return Array.IndexOf(_items, item, 0, Count);
    }

    public void Insert(int index, T item)
    {
        if (index < 0 || index > Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), $"The index ({index}) must be greater than or equal to 0 and less than or equal to the elements count ({Count}).");
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
        ValidateIndex(index);

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
        _modCount++;
    }

    public bool Contains(T item)
    {
        return IndexOf(item) != -1;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        ArgumentNullException.ThrowIfNull(array, nameof(array));

        if (arrayIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex), $"The index ({arrayIndex}) must be greater or equal to 0.");
        }

        if (array.Length - arrayIndex < Count)
        {
            throw new ArgumentException($"The number of elements in the source list ({Count}) must be less than or equal to available space from arrayIndex ({arrayIndex}) to the end of the destination array ({array.Length}).", nameof(array));
        }

        Array.Copy(_items, 0, array, arrayIndex, Count);
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
        if (Count == 0)
        {
            return "[]";
        }

        StringBuilder stringBuilder = new();
        stringBuilder.Append('[');

        foreach (T item in _items)
        {
            stringBuilder.Append(item is null ? "null" : item).Append(", ");
        }

        stringBuilder.Length -= 2;
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

        for (int i = 0; i < Count; i++)
        {
            if (!Equals(_items[i], list._items[i]))
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

        for (int i = 0; i < Count; i++)
        {
            hash = prime * hash + (_items[i]?.GetHashCode() ?? 0);
        }

        return hash;
    }
}
