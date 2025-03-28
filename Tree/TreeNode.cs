namespace TreeTask;

public class TreeNode<T>(T data)
{
    public T Value { get; set; } = data;

    public TreeNode<T>? Left { get; set; }

    public TreeNode<T>? Right { get; set; }

    public bool IsLeaf => Left is null && Right is null;

    public bool HasTwoChildren => Left is not null && Right is not null;

    public List<TreeNode<T>> Children
    {
        get
        {
            List<TreeNode<T>> children = [];

            if (Left is not null)
            {
                children.Add(Left);
            }

            if (Right is not null)
            {
                children.Add(Right);
            }

            return children;
        }
    }
}
