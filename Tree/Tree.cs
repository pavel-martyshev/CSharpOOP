using System.Text;

namespace TreeTask;

public class Tree<T>
{
    private TreeNode<T>? _root;

    private readonly IComparer<T>? _comparer;

    public int Count { get; private set; }

    public Tree(TreeNode<T>? root)
    {
        _root = root;
        Count++;
    }

    public Tree(TreeNode<T>? root, IComparer<T> comparer)
    {
        _root = root;
        _comparer = comparer;
        Count++;
    }

    private static int CompareTo(T x, T y, IComparer<T>? comparer)
    {
        if (comparer is not null)
        {
            return comparer.Compare(x, y);
        }

        if (x is IComparable<T> comparableT)
        {
            return comparableT.CompareTo(y);
        }

        throw new InvalidOperationException($"Type {typeof(T)} must implement IComparable or a custom IComparer<T> must be provided.");
    }

    public void Add(T value)
    {
        if (_root is null)
        {
            _root = new(value);
            Count++;
            return;
        }

        TreeNode<T>? node = _root;

        for (int i = 0; i < Count; i++)
        {
            int comparisonResult = CompareTo(value, node.Value, _comparer);

            if (comparisonResult < 0)
            {
                if (node.Left is not null)
                {
                    node = node.Left;
                }
                else
                {
                    node.Left = new(value);
                    break;
                }
            }
            else
            {
                if (node.Right is not null)
                {
                    node = node.Right;
                }
                else
                {
                    node.Right = new(value);
                    break;
                }
            }
        }

        Count++;
    }

    private TreeNode<T>? GetNode(T value)
    {
        if (_root is null)
        {
            return null;
        }

        TreeNode<T>? node = _root;

        for (int i = 0; i < Count; i++)
        {
            if (node is null)
            {
                return null;
            }

            int comparisonResult = CompareTo(value, node.Value, _comparer);

            if (comparisonResult == 0)
            {
                return node;
            }

            if (comparisonResult < 0)
            {
                node = node.Left;
            }
            else if (comparisonResult > 0)
            {
                node = node.Right;
            }
        }

        return null;
    }

    public bool Contains(T value)
    {
        TreeNode<T>? node = GetNode(value);

        if (node is null)
        {
            return false;
        }

        return true;
    }

    private TreeNode<T>? GetParent(T value)
    {
        TreeNode<T>? previousNode = null;
        TreeNode<T>? node = _root;

        for (int i = 0; i < Count; i++)
        {
            if (node is null)
            {
                return null;
            }

            int comparisonResult = CompareTo(value, node.Value, _comparer);

            if (comparisonResult == 0)
            {
                return previousNode;
            }

            if (comparisonResult < 0)
            {
                previousNode = node;
                node = node.Left;
            }
            else if (comparisonResult > 0)
            {
                previousNode = node;
                node = node.Right;
            }
        }

        return previousNode;
    }

    private static void ReplaceParentChild(TreeNode<T> parent, TreeNode<T> child, TreeNode<T>? newChild)
    {
        if (parent.Left is not null && ReferenceEquals(parent.Left, child))
        {
            parent.Left = newChild;
        }
        else if (parent.Right is not null && ReferenceEquals(parent.Right, child))
        {
            parent.Right = newChild;
        }
    }

    private void ReplaceNode(TreeNode<T>? parent, TreeNode<T> node, TreeNode<T>? newNode)
    {
        if (parent is null)
        {
            _root = newNode;
        }
        else
        {
            ReplaceParentChild(parent, node, newNode);
        }
    }

    private void RemoveNodeWithTwoChildren(TreeNode<T>? parent, TreeNode<T> node)
    {
        if (node.Right!.Left is null)
        {
            node.Right.Left = node.Left;
            ReplaceNode(parent, node, node.Right);
        }
        else
        {
            TreeNode<T> minChildParent = node;
            TreeNode<T> minChild = node.Right;

            while (minChild.Left is not null)
            {
                minChildParent = minChild;
                minChild = minChild.Left;
            }

            minChildParent.Left = minChild.Right;
            minChild.Left = node.Left;
            minChild.Right = node.Right;

            ReplaceNode(parent, node, minChild);
        }
    }

    public bool Remove(T value)
    {
        if (_root is null)
        {
            return false;
        }

        TreeNode<T>? node = GetNode(value);

        if (node is null)
        {
            return false;
        }

        TreeNode<T>? parent = GetParent(value);

        if (node.IsLeaf)
        {
            ReplaceNode(parent, node, null);
        }
        else if (node.HasTwoChildren)
        {
            RemoveNodeWithTwoChildren(parent, node);
        }
        else
        {
            TreeNode<T>? nextNode = node.Left is not null ? node.Left : node.Right;
            ReplaceNode(parent, node, nextNode);
        }

        Count--;
        return true;
    }

    public void TraverseBreadthFirst(Action<T> action)
    {
        if (_root is null)
        {
            throw new InvalidOperationException("Tree is empty");
        }

        Queue<TreeNode<T>> queue = new();
        queue.Enqueue(_root);

        while (queue.Count > 0)
        {
            TreeNode<T> node = queue.Dequeue();

            action.Invoke(node.Value);

            if (node.Left is not null)
            {
                queue.Enqueue(node.Left);
            }

            if (node.Right is not null)
            {
                queue.Enqueue(node.Right);
            }
        }
    }

    private static void TraverseDepthFirstRecursive(TreeNode<T> node, Action<T> action)
    {
        action.Invoke(node.Value);

        foreach (TreeNode<T> child in node.Children)
        {
            TraverseDepthFirstRecursive(child, action);
        }
    }

    public void TraverseDepthFirstRecursive(Action<T> action)
    {
        if (_root is null)
        {
            throw new InvalidOperationException("Tree is empty");
        }

        TraverseDepthFirstRecursive(_root, action);
    }

    public void TraverseDepthFirst(Action<T> action)
    {
        if (_root is null)
        {
            throw new InvalidOperationException("Tree is empty");
        }

        Stack<TreeNode<T>> stack = new();
        stack.Push(_root);

        while (stack.Count > 0)
        {
            TreeNode<T> node = stack.Pop();

            action.Invoke(node.Value);

            if (node.Right is not null)
            {
                stack.Push(node.Right);
            }

            if (node.Left is not null)
            {
                stack.Push(node.Left);
            }
        }
    }

    public override string ToString()
    {
        if (_root is null)
        {
            return "[]";
        }

        StringBuilder stringBuilder = new();
        stringBuilder.Append('[');

        TraverseBreadthFirst(value => stringBuilder.Append(value).Append(", "));

        stringBuilder.Length -= 2;
        stringBuilder.Append(']');

        return stringBuilder.ToString();
    }
}
