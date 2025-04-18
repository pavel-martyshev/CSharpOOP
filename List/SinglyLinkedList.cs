using System.Text;

namespace List;

public class SinglyLinkedList<T>
{
    private ListNode<T>? _head;

    public int Count { get; private set; }

    public T this[int index]
    {
        get
        {
            ValidateIndex(index);

            return GetNodeByIndex(index).Value;
        }

        set
        {
            ValidateIndex(index);

            GetNodeByIndex(index).Value = value;
        }
    }

    private void ValidateIndex(int index)
    {
        if (index < 0 || index >= Count)
        {
            throw new IndexOutOfRangeException($"The index ({index}) must be greater than or equal to 0 and less than the elements count ({Count}).");
        }
    }

    public T GetFirst()
    {
        if (_head is null)
        {
            throw new InvalidOperationException("List is empty.");
        }

        return _head.Value;
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
        ValidateIndex(index);

        if (index == 0)
        {
            return RemoveFirst();
        }

        ListNode<T> previousNode = GetNodeByIndex(index - 1);
        ListNode<T> node = previousNode.Next!;

        previousNode.Next = node.Next;
        Count--;

        return node.Value;
    }

    public bool RemoveByValue(T value)
    {
        if (_head is null)
        {
            return false;
        }

        if (Equals(value, _head.Value))
        {
            RemoveFirst();
            return true;
        }

        ListNode<T> previousNode = _head;

        for (ListNode<T>? node = _head.Next; node is not null; node = node.Next)
        {
            if (Equals(value, node.Value))
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
        if (_head is null)
        {
            throw new InvalidOperationException("List is empty.");
        }

        T oldValue = _head.Value;
        _head = _head.Next;
        Count--;

        return oldValue;
    }

    public void AddFirst(T value)
    {
        _head = new(value, _head);
        Count++;
    }

    public void AddByIndex(int index, T value)
    {
        if (index < 0 || index > Count)
        {
            throw new IndexOutOfRangeException($"The index ({index}) must be greater than or equal to 0 and less than or equal to the elements count ({Count}).");
        }

        if (index == 0)
        {
            AddFirst(value);
            return;
        }

        ListNode<T> previousNode = GetNodeByIndex(index - 1);
        previousNode.Next = new(value, previousNode.Next);

        Count++;
    }

    public void Reverse()
    {
        ListNode<T>? previousNode = null;
        ListNode<T>? currentNode = _head;

        while (currentNode != null)
        {
            ListNode<T>? nextNode = currentNode.Next;
            currentNode.Next = previousNode;

            previousNode = currentNode;
            currentNode = nextNode!;
        }

        _head = previousNode;
    }

    public SinglyLinkedList<T> Copy()
    {
        SinglyLinkedList<T> listCopy = new();

        if (_head is null)
        {
            return listCopy;
        }

        listCopy._head = new(_head.Value);

        ListNode<T>? previousCopyNode = listCopy._head;
        ListNode<T>? currentNode = _head.Next;

        while (currentNode != null)
        {
            ListNode<T> newCopyNode = new(currentNode.Value);
            previousCopyNode.Next = newCopyNode;

            previousCopyNode = newCopyNode;
            currentNode = currentNode.Next;
        }

        listCopy.Count = Count;
        return listCopy;
    }

    public override string ToString()
    {
        if (_head is null)
        {
            return "[]";
        }

        StringBuilder stringBuilder = new();

        stringBuilder.Append('[');

        for (ListNode<T>? node = _head; node != null; node = node.Next)
        {
            stringBuilder.Append(node.Value).Append(", ");
        }

        stringBuilder.Length -= 2;

        stringBuilder.Append(']');

        return stringBuilder.ToString();
    }
}
