namespace TreeTask;

public class Tree<T>
{
    public TreeNode<T> Root { get; set; }

    public Tree(T data)
    {
        Root = new(data);
    }

    public Tree(T[] values)
    {
        throw new NotImplementedException();
    }

     public void Add(T item)
    {
        IComparer<T> comparer;
    }
}
