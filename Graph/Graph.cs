using System.Text;

namespace GraphTask;

internal class Graph<T>(List<T> vertices, int[,] edges)
{
    public List<T> Vertices { get; } = vertices;

    public int[,] Edges { get; } = edges;

    public int Count => Vertices.Count;

    public string BreadthFirstTraversal()
    {
        if (Count == 0)
        {
            return "[]";
        }

        bool[] visited = new bool[Count];

        StringBuilder stringBuilder = new();
        stringBuilder.Append("[[");

        Queue<T> queue = new();
        queue.Enqueue(Vertices[0]);

        while (queue.Count > 0)
        {
            T vertex = queue.Dequeue();
            stringBuilder.Append(vertex).Append(", ");

            int vertexIndex = Vertices.IndexOf(vertex);
            visited[vertexIndex] = true;

            for (int i = 0; i < Count; i++)
            {
                if (Edges[vertexIndex, i] == 1 && !visited[i])
                {
                    queue.Enqueue(Vertices[i]);
                }
            }

            if (queue.Count == 0 && visited.Contains(false))
            {
                stringBuilder.Length -= 2;
                stringBuilder.Append("] [");
                queue.Enqueue(Vertices[vertexIndex + 1]);
            }
        }

        stringBuilder.Length -= 2;
        stringBuilder.Append("]]");

        return stringBuilder.ToString();
    }

    private void RecursiveDepthTraversal(T vertex, StringBuilder stringBuilder, bool[] visited)
    {
        stringBuilder.Append(vertex).Append(", ");

        int vertexIndex = Vertices.IndexOf(vertex);
        visited[vertexIndex] = true;

        for (int i = 0; i < Count; i++)
        {
            if (Edges[vertexIndex, i] == 1 && !visited[i])
            {
                RecursiveDepthTraversal(Vertices[i], stringBuilder, visited);
            }
        }

        if (visited.Contains(false))
        {
            stringBuilder.Length -= 2;
            stringBuilder.Append("] [");

            RecursiveDepthTraversal(Vertices[vertexIndex + 1], stringBuilder, visited);
        }
    }

    public string RecursiveDepthTraversal()
    {
        if (Count == 0)
        {
            return "[]";
        }

        bool[] visited = new bool[Count];

        StringBuilder stringBuilder = new();
        stringBuilder.Append("[[");

        RecursiveDepthTraversal(Vertices[0], stringBuilder, visited);

        stringBuilder.Length -= 2;
        stringBuilder.Append("]]");

        return stringBuilder.ToString();
    }

    public string DepthTraversal()
    {
        if (Count == 0)
        {
            return "[]";
        }

        bool[] visited = new bool[Count];

        StringBuilder stringBuilder = new();
        stringBuilder.Append("[[");

        Stack<T> stack = new();
        stack.Push(Vertices[0]);

        while (stack.Count > 0)
        {
            T vertex = stack.Pop();

            stringBuilder.Append(vertex).Append(", ");

            int vertexIndex = Vertices.IndexOf(vertex);
            visited[vertexIndex] = true;

            for (int i = Count - 1; i > 0; i--)
            {
                if (Edges[vertexIndex, i] == 1 && !visited[i])
                {
                    stack.Push(Vertices[i]);
                }
            }

            if (stack.Count == 0 && visited.Contains(false))
            {
                stringBuilder.Length -= 2;
                stringBuilder.Append("] [");
                stack.Push(Vertices[vertexIndex + 1]);
            }
        }

        stringBuilder.Length -= 2;
        stringBuilder.Append("]]");

        return stringBuilder.ToString();
    }
}
