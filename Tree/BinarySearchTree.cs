using System.Text;

namespace TreeTask;

public class BinarySearchTree<T>
{
    private TreeNode<T>? _root;

    private readonly IComparer<T>? _comparer;

    public int Count { get; private set; }

    public BinarySearchTree() { }

    public BinarySearchTree(IComparer<T> comparer)
    {
        _comparer = comparer;
    }

    private int CompareTo(T value1, T value2)
    {
        if (_comparer is not null)
        {
            return _comparer.Compare(value1, value2);
        }

        return Comparer<T>.Default.Compare(value1, value2);

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

        while (true)
        {
            int comparisonResult = CompareTo(value, node.Value);

            if (comparisonResult < 0)
            {
                if (node.Left is not null)
                {
                    node = node.Left;
                }
                else
                {
                    node.Left = new(value, node);
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
                    node.Right = new(value, node);
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

        while (true)
        {
            if (node is null)
            {
                return null;
            }

            int comparisonResult = CompareTo(value, node.Value);

            if (comparisonResult == 0)
            {
                return node;
            }

            if (comparisonResult < 0)
            {
                node = node.Left;
            }
            else
            {
                node = node.Right;
            }
        }
    }

    public bool Contains(T value)
    {
        return GetNode(value) is null;
    }

    private static void ReplaceParentChild(TreeNode<T> parent, TreeNode<T> child, TreeNode<T>? newChild)
    {
        if (ReferenceEquals(parent.Left, child))
        {
            parent.Left = newChild;
        }
        else if (ReferenceEquals(parent.Right, child))
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

        TreeNode<T>? parent = node.Parent;

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
            return;
        }

        Queue<TreeNode<T>> queue = new();
        queue.Enqueue(_root);

        while (queue.Count > 0)
        {
            TreeNode<T> node = queue.Dequeue();

            action(node.Value);

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

    private static void TraverseDepthFirstRecursive(TreeNode<T>? node, Action<T> action)
    {
        if (node is null)
        {
            return;
        }

        action(node.Value);

        TraverseDepthFirstRecursive(node.Left, action);

        TraverseDepthFirstRecursive(node.Right, action);
    }

    public void TraverseDepthFirstRecursive(Action<T> action)
    {
        if (_root is null)
        {
            return;
        }

        TraverseDepthFirstRecursive(_root, action);
    }

    public void TraverseDepthFirst(Action<T> action)
    {
        if (_root is null)
        {
            return;
        }

        Stack<TreeNode<T>> stack = new();
        stack.Push(_root);

        while (stack.Count > 0)
        {
            TreeNode<T> node = stack.Pop();

            action(node.Value);

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
