namespace GraphTask;

internal class Program
{
    static void Main(string[] args)
    {
        List<string> vertices = ["A", "B", "C", "D", "E", "F", "G"];

        int[,] edges =
        {
            { 0, 1, 0, 0, 0, 0, 0 },
            { 1, 0, 1, 0, 0, 0, 0 },
            { 0, 1, 0, 1, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 1 },
            { 0, 0, 0, 0, 0, 1, 0 }
        };

        Graph<string> graph = new(vertices, edges);

        Console.WriteLine("Breadth first traversal:");
        graph.TraverseBreadthFirst(Console.WriteLine);

        Console.WriteLine();

        Console.WriteLine("Depth first traversal recursive:");
        graph.TraverseDepthFirstRecursive(Console.WriteLine);

        Console.WriteLine();

        Console.WriteLine("Depth first traversal:");
        graph.TraverseDepthFirst(Console.WriteLine);
    }
}
