namespace TreeTask;

internal class Program
{
    static void Main(string[] args)
    {
        Tree<int> tree = new();

        tree.Add(8);
        tree.Add(3);
        tree.Add(1);
        tree.Add(10);
        tree.Add(14);
        tree.Add(6);
        tree.Add(7);
        tree.Add(13);
        tree.Add(4);
        tree.Add(5);
        tree.Add(9);

        tree.Remove(3);

        Console.WriteLine(tree.BreadthFirstTraversal());
        Console.WriteLine(tree.RecursiveDepthTraversal());
        Console.WriteLine(tree.DepthTraversal());

        Console.WriteLine($"Count = {tree.Count}");
    }
}
