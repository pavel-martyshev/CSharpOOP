namespace TreeTask;

public class TreeNode<T>(T value)
{
    public T Value { get; set; } = value;

    public TreeNode<T>? Left { get; set; }

    public TreeNode<T>? Right { get; set; }

    public bool IsLeaf => Left is null && Right is null;

    public bool HasTwoChildren => Left is not null && Right is not null;
}
