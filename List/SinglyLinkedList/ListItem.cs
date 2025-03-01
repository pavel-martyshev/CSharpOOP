namespace List;

public class ListItem<T>
{
    public T Value { get; set; }

    public ListItem<T>? Next { get; set; }

    public ListItem(T data)
    {
        Value = data;
        Next = null;
    }

    public ListItem(T data, ListItem<T> next)
    {
        Value = data;
        Next = next;
    }
}
