using System.Text;

namespace List;

class SinglyLinkedList<T>
{
    private ListNode<T>? _head;

    public int Count { get; private set; }

    public SinglyLinkedList()
    {
        _head = null;
        Count = 0;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException($"The index must be greater than 0 and less than the length of the list ({Count}).");
            }

            return GetNodeByIndex(index).Value;
        }

        set
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));

            if (index < 0 || index > Count)
            {
                throw new IndexOutOfRangeException($"The index must be greater than 0 and less than or equal to the length of the list ({Count}).");
            }

            if (Count == 0)
            {
                AddFirst(value);
            }

            if (index == Count)
            {
                GetNodeByIndex(index - 1).Next = new ListNode<T>(value);
                Count++;
            }
            else
            {
                GetNodeByIndex(index).Value = value;
            }
        }
    }

    public T GetFirst()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException("List is empty.");
        }

        return _head!.Value;
    }

    private ListNode<T> GetNodeByIndex(int index)
    {
        ListNode<T> currentNode = _head!;

        for (int i = 0; i < index; i++)
        {
            currentNode = currentNode.Next!;
        }

        return currentNode;
    }

    public T RemoveByIndex(int index)
    {
        if (index < 0 || index >= Count)
        {
            throw new IndexOutOfRangeException();
        }

        ListNode<T> node;

        if (index != 0)
        {
            ListNode<T> previousNode = GetNodeByIndex(index - 1);

            node = previousNode.Next!;
            previousNode.Next = node.Next;
        }
        else
        {
            node = _head!;
            _head = _head.Next;
        }

        Count--;
        return node!.Value;
    }

    public bool RemoveByValue(T value)
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));

        if (Count == 0)
        {
            throw new InvalidOperationException("List is empty.");
        }

        if (value.Equals(_head!.Value))
        {
            _head = _head.Next;
            Count--;

            return true;
        }

        ListNode<T> previousNode = _head;

        for (ListNode<T> node = _head.Next!; node != null; node = node.Next!)
        {
            if (node.Value!.Equals(value))
            {
                previousNode.Next = node.Next;
                Count--;

                return true;
            }

            previousNode = node;
        }

        return false;
    }

    public T RemoveFirst()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException("List is empty.");
        }

        T oldValue = _head!.Value;
        _head = _head.Next;
        Count--;

        return oldValue;
    }

    public void AddFirst(T value)
    {
        ListNode<T> newNode = new(value)
        {
            Next = _head
        };

        _head = newNode;
        Count++;
    }

    public void AddByIndex(int index, T value)
    {
        if (index < 0 || index > Count)
        {
            throw new IndexOutOfRangeException();
        }

        if (index == 0)
        {
            AddFirst(value);
        }
        else
        {
            ListNode<T> previousNode = GetNodeByIndex(index - 1);
            ListNode<T> newNode = new(value);

            if (index != Count)
            {
                newNode.Next = previousNode.Next;
            }

            previousNode.Next = newNode;

            Count++;
        }
    }

    public void Reverse()
    {
        if (Count > 1)
        {
            ListNode<T>? previousNode = null;
            ListNode<T> currentNode = _head!;

            while (currentNode != null)
            {
                ListNode<T> nextNode = currentNode.Next!;
                currentNode.Next = previousNode;

                previousNode = currentNode;
                currentNode = nextNode!;
            }

            _head = previousNode!;
        }
    }

    public SinglyLinkedList<T> Copy()
    {
        SinglyLinkedList<T> listCopy = new();

        if (Count == 1)
        {
            listCopy.AddFirst(_head!.Value);
        }

        if (Count > 1)
        {
            for (int i = 0; i < Count; i++)
            {
                listCopy[i] = this[i];
            }
        }

        return listCopy;
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();

        for (ListNode<T> node = _head; node != null; node = node.Next)
        {
            stringBuilder.Append(node.Value).Append(", ");
        }

        if (stringBuilder.Length > 0)
        {
            stringBuilder.Length -= 2;
        }

        return stringBuilder.ToString();
    }
}
