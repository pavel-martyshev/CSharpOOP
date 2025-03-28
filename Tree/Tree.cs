using System.Text;

namespace TreeTask;

public class Tree<T> where T : IComparable<T>
{
    private TreeNode<T>? _root;

    public int Count { get; private set; }

    private static TreeNode<T> Add(TreeNode<T>? node, T value)
    {
        if (node is null)
        {
            return new TreeNode<T>(value);
        }

        int comparison = value.CompareTo(node!.Value);

        if (comparison < 0)
        {
            node.Left = Add(node!.Left, value);
        }
        else
        {
            node.Right = Add(node!.Right, value);
        }

        return node;
    }

    public void Add(T value)
    {
        if (_root == null)
        {
            _root = new(value);
        }
        else
        {
            Add(_root, value);
        }

        Count++;
    }

    private static TreeNode<T>? GetNode(TreeNode<T>? node, T value)
    {
        if (node is null)
        {
            return null;
        }

        int comparison = value.CompareTo(node!.Value);

        if (comparison < 0)
        {
            return GetNode(node.Left, value);
        }
        else if (comparison > 0)
        {
            return GetNode(node.Right, value);
        }

        return node;
    }

    public TreeNode<T>? GetNode(T value)
    {
        return GetNode(_root, value);
    }

    private static TreeNode<T>? GetParent(TreeNode<T>? node, TreeNode<T>? parent, T value)
    {
        int comparison = value.CompareTo(node!.Value);

        if (comparison < 0)
        {
            return GetParent(node.Left, node, value);
        }
        else if (comparison > 0)
        {
            return GetParent(node.Right, node, value);
        }

        return parent;
    }

    private static void ReplaceParentChild(TreeNode<T> parent, TreeNode<T> child, TreeNode<T>? newChild)
    {
        if (parent.Left is not null && parent.Left.Equals(child))
        {
            parent.Left = newChild;
        }
        else if (parent.Right is not null && parent.Right.Equals(child))
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

        TreeNode<T>? parent = GetParent(_root, null, value);

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

    public string BreadthFirstTraversal()
    {
        if (Count == 0)
        {
            return "[]";
        }

        StringBuilder stringBuilder = new();
        stringBuilder.Append('[');

        Queue<TreeNode<T>> queue = new();
        queue.Enqueue(_root!);

        while (queue.Count > 0)
        {
            TreeNode<T> node = queue.Dequeue();

            stringBuilder.Append(node.Value).Append(", ");

            if (node.Left is not null)
            {
                queue.Enqueue(node.Left);
            }

            if (node.Right is not null)
            {
                queue.Enqueue(node.Right);
            }
        }

        stringBuilder.Length -= 2;
        stringBuilder.Append(']');

        return stringBuilder.ToString();
    }

    private static void RecursiveDepthTraversal(TreeNode<T> node, StringBuilder stringBuilder)
    {
        stringBuilder.Append(node.Value).Append(", ");

        foreach (TreeNode<T> child in node.Children)
        {
            RecursiveDepthTraversal(child, stringBuilder);
        }
    }

    public string RecursiveDepthTraversal()
    {
        if (Count == 0)
        {
            return "[]";
        }

        StringBuilder stringBuilder = new();
        stringBuilder.Append('[');

        RecursiveDepthTraversal(_root!, stringBuilder);

        stringBuilder.Length -= 2;
        stringBuilder.Append(']');

        return stringBuilder.ToString();
    }

    public string DepthTraversal()
    {
        if (Count == 0)
        {
            return "[]";
        }

        StringBuilder stringBuilder = new();
        stringBuilder.Append('[');

        Stack<TreeNode<T>> stack = new();
        stack.Push(_root!);

        while (stack.Count > 0)
        {
            TreeNode<T> node = stack.Pop();

            stringBuilder.Append(node.Value).Append(", ");

            if (node.Right is not null)
            {
                stack.Push(node.Right);
            }

            if (node.Left is not null)
            {
                stack.Push(node.Left);
            }
        }

        stringBuilder.Length -= 2;
        stringBuilder.Append(']');

        return stringBuilder.ToString();
    }
}
