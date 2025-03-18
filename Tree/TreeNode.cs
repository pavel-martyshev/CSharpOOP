namespace TreeTask;

class TreeNode<T>(T data)
{
    public T Data { get; set; } = data;

    public TreeNode<T>? Left { get; set; } = default;

    public TreeNode<T>? Right { get; set; } = default;
}
