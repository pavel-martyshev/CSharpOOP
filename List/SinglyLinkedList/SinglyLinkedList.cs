namespace List;

class SinglyLinkedList<T>
{
    private ListItem<T> head;
    public int Count { get; private set; }

    public SinglyLinkedList(T data)
    {
        head = new ListItem<T>(data, head!);
        Count += 1;
    }

    public SinglyLinkedList(ListItem<T> node)
    {
        head = node;
        Count += 1;
    }

    private void ValidateIndex(int index)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(index, Count - 1);
    }

    public T GetFirstElement()
    {
        ArgumentOutOfRangeException.ThrowIfEqual(Count, 0);
        return head.Value;
    }

    private ListItem<T> GetNodeByIndex(int index)
    {
        ValidateIndex(index);

        ListItem<T> current = head;

        for (int i = 0; i < index; i++)
        {
            current = current.Next!;
        }

        return current;
    }

    public T GetValueByIndex(int index)
    {
        ValidateIndex(index);
        return GetNodeByIndex(index).Value;
    }

    public T SetValueByIndex(int index, T value)
    {
        ValidateIndex(index);

        ListItem<T> node = GetNodeByIndex(index);
        (T oldData, node.Value) = (node.Value, value);

        return oldData;
    }

    public T RemoveValueByIndex(int index)
    {
        ValidateIndex(index);

        ListItem<T> node = GetNodeByIndex(index);
        GetNodeByIndex(index - 1).Next = node.Next;
        Count -= 1;

        return node.Value;
    }

    public bool RemoveNodeByValue(T value)
    {
        ListItem<T> previousNode = head;

        for (ListItem<T> node = head.Next!; node != null; node = node.Next!)
        {
            if (node.Value!.Equals(value))
            {
                previousNode.Next = node.Next;
                Count -= 1;

                return true;
            }

            previousNode = node;
        }

        return false;
    }

    public T RemoveFirst()
    {
        (T oldValue, head) = (head.Value, head.Next!);
        Count -= 1;

        return oldValue;
    }

    public void AddFirst(T value)
    {
        head = new(value, head);
        Count += 1;
    }

    public void AddFirst(ListItem<T> node)
    {
        node.Next = head;
        head = node;
        Count += 1;
    }

    public void AddByIndex(int index, T value)
    {
        ValidateIndex(index);

        GetNodeByIndex(index - 1).Next = new ListItem<T>(value, GetNodeByIndex(index));
        Count += 1;
    }

    public void AddByIndex(int index, ListItem<T> node)
    {
        ValidateIndex(index);

        node.Next = GetNodeByIndex(index);
        GetNodeByIndex(index - 1).Next = node;
        Count += 1;
    }

    public void Expand()
    {
        ArgumentOutOfRangeException.ThrowIfEqual(Count, 0);
        ArgumentOutOfRangeException.ThrowIfEqual(Count, 1);

        ListItem<T>? previousNode = null;
        ListItem<T> currentNode = head;

        while (currentNode != null)
        {
            ListItem<T> nextNode = currentNode.Next!;
            currentNode.Next = previousNode;

            previousNode = currentNode;
            currentNode = nextNode!;
        }

        head = previousNode!;
    }

    public SinglyLinkedList<T> Copy()
    {
        SinglyLinkedList<T> listCopy = new(new ListItem<T>(head.Value));

        for (ListItem<T> node = head.Next!; node != null; node = node.Next!)
        {
            listCopy.AddFirst(new ListItem<T>(node.Value));
        }

        listCopy.Expand();
        return listCopy;
    }
}
