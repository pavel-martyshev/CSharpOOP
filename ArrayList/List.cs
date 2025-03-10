using System.Collections;

namespace ArrayList;

public class List<T> : IList<T>
{
    private T[] _items = new T[10];

    private int _modCount;

    public int Count { get; private set; }

    private int Capacity
    {
        set
        {
            if (value < Count)
            {
                throw new ArgumentException("The capacity cannot be less than the elements count.", nameof(value));
            }

            Array.Resize(ref _items, value);
        }
    }

    public bool IsReadOnly => false;

    public List()
    {
        Count = 0;
        _modCount = 0;
    }

    public List(int capacity)
    {
        if (capacity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity), "The capacity must not be less than 0.");
        }

        Capacity = capacity;
        Count = 0;
        _modCount = 0;
    }

    public List(T[] items)
    {
        if (items.Length > _items.Length)
        {
            Capacity = _items.Length * 2;
        }

        Array.Copy(items, _items, items.Length);
        Count = items.Length;
        _modCount = 0;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException($"The index must be greater than 0 and less than the length of the list ({Count}).");
            }

            return _items[index];
        }
        set
        {
            if (index < 0 || index > Count)
            {
                throw new IndexOutOfRangeException($"The index must be greater than 0 and less than or equal to the length of the list ({Count}).");
            }

            if (index == Count)
            {
                Count++;
            }

            _items[index] = value;
            _modCount++;
        }
    }

    public void TrimExcess()
    {
        if (Count / _items.Length * 100 <= 10)
        {
            Capacity = Count;
        }
    }

    public int IndexOf(T item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        for (int i = 0; i < Count; i++)
        {
            if (item!.Equals(_items[i]))
            {
                return i;
            }
        }

        return -1;
    }

    public void Insert(int index, T item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        if (index < 0 || index > Count)
        {
            throw new IndexOutOfRangeException();
        }

        Count++;

        if (Count > _items.Length)
        {
            Capacity = _items.Length * 2;
        }

        for (int i = Count; i > index; i--)
        {
            _items[i] = _items[i - 1];
        }

        _items[index] = item;
        _modCount++;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= Count)
        {
            throw new IndexOutOfRangeException();
        }

        Array.Copy(_items, index + 1, _items, index, Count - index - 1);

        _items[Count - 1] = default;
        Count--;
        _modCount++;
    }

    public void Add(T item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        if (Count >= _items.Length)
        {
            Capacity = _items.Length * 2;
        }

        _items[Count] = item;
        Count++;
        _modCount++;
    }

    public void Clear()
    {
        _items = new T[10];
        Count = 0;
        _modCount = 0;
    }

    public bool Contains(T item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        foreach (T internalItem in _items)
        {
            if (item.Equals(internalItem))
            {
                return true;
            }
        }

        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (arrayIndex < 0 || arrayIndex >= array.Length)
        {
            throw new IndexOutOfRangeException();
        }

        for (int i = 0; i < Count; i++)
        {
            array.SetValue(_items[i], arrayIndex++);
        }
    }

    public bool Remove(T item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

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
        int modCountCopy = _modCount;

        for (int i = 0; i < Count; ++i)
        {
            if (modCountCopy != _modCount)
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

    public override string ToString() => string.Join(", ", _items[..Count]);
}
