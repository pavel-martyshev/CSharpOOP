namespace GraphTask;

internal class Graph<T>
{
    private readonly List<T> _vertices;

    private readonly int[,] _edges;

    public int Count => _vertices.Count;

    public Graph(List<T> vertices, int[,] edges)
    {
        ArgumentNullException.ThrowIfNull(vertices, nameof(vertices));
        ArgumentNullException.ThrowIfNull(edges, nameof(edges));

        if (edges.GetLength(0) != edges.GetLength(1))
        {
            throw new ArgumentException($"The matrix of edges ({edges.GetLength(0)}x{edges.GetLength(1)}) must be square.", nameof(edges));
        }

        if (vertices.Count != edges.GetLength(0))
        {
            throw new ArgumentException($"The number of vertices ({vertices.Count}) must be equal to the number of rows and columns in the matrix ({edges.GetLength(0)}).", nameof(vertices));
        }

        _edges = edges;
        _vertices = vertices;
    }

    public void TraverseBreadthFirst(Action<T> action)
    {
        ArgumentNullException.ThrowIfNull(action, nameof(action));

        bool[] visited = new bool[Count];

        Queue<int> queue = new();

        for (int i = 0; i < Count; i++)
        {
            if (visited[i])
            {
                continue;
            }

            queue.Enqueue(i);
            visited[i] = true;

            while (queue.Count > 0)
            {
                int currentVertexIndex = queue.Dequeue();

                action(_vertices[currentVertexIndex]);

                for (int j = 0; j < Count; j++)
                {
                    if (_edges[currentVertexIndex, j] != 0 && !visited[j])
                    {
                        queue.Enqueue(j);
                        visited[j] = true;
                    }
                }
            }
        }
    }

    private void TraverseDepthFirstRecursive(int vertexIndex, Action<T> action, bool[] visited)
    {
        visited[vertexIndex] = true;
        action(_vertices[vertexIndex]);

        for (int i = 0; i < Count; i++)
        {
            if (_edges[vertexIndex, i] != 0 && !visited[i])
            {
                TraverseDepthFirstRecursive(i, action, visited);
            }
        }
    }

    public void TraverseDepthFirstRecursive(Action<T> action)
    {
        ArgumentNullException.ThrowIfNull(action, nameof(action));

        bool[] visited = new bool[Count];

        for (int i = 0; i < Count; i++)
        {
            if (visited[i])
            {
                continue;
            }

            TraverseDepthFirstRecursive(i, action, visited);
        }
    }

    public void TraverseDepthFirst(Action<T> action)
    {
        ArgumentNullException.ThrowIfNull(action, nameof(action));

        bool[] visited = new bool[Count];

        Stack<int> stack = new();

        for (int i = 0; i < Count; i++)
        {
            if (visited[i])
            {
                continue;
            }

            stack.Push(i);
            visited[i] = true;

            while (stack.Count > 0)
            {
                int currentVertexIndex = stack.Pop();

                action(_vertices[currentVertexIndex]);

                for (int j = Count - 1; j >= 0; j--)
                {
                    if (_edges[currentVertexIndex, j] != 0 && !visited[j])
                    {
                        stack.Push(j);
                        visited[j] = true;
                    }
                }
            }
        }
    }
}
