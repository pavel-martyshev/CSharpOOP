namespace List;

public class ListNode<T>(T value)
{
    public T Value { get; set; } = value;

    public ListNode<T>? Next { get; set; }
}
