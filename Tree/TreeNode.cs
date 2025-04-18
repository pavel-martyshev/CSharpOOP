namespace TreeTask;

public class TreeNode<T>
{
    public T Value { get; set; }

    public TreeNode<T>? Parent { get; set; }

    public TreeNode(T data)
    {
        Value = data;
    }

    public TreeNode(T data, TreeNode<T> parent)
    {
        Value = data;
        Parent = parent;
    }

    public TreeNode<T>? Left { get; set; }

    public TreeNode<T>? Right { get; set; }

    public bool IsLeaf => Left is null && Right is null;

    public bool HasTwoChildren => Left is not null && Right is not null;
}
