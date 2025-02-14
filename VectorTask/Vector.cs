namespace VectorTask;

public class Vector
{
    public List<double> Components { get; set; }

    public static Vector AdditionTwoVectors(Vector vector1, Vector vector2)
    {
        Vector resultVector = new(vector1);
        resultVector.Add(vector2);

        return resultVector;
    }

    public static Vector SubtractionTwoVectors(Vector vector1, Vector vector2)
    {
        Vector resultVector = new(vector1);
        resultVector.Subtract(vector2);

        return resultVector;
    }

    public static double GetDotProduct(Vector vector1, Vector vector2)
    {
        int smallestComponentsSize = Math.Min(vector1.GetSize(), vector2.GetSize());
        double dotProduct = 0;

        for (int i = 0; i < smallestComponentsSize; i++)
        {
            dotProduct += vector1[i] * vector2[i];
        }

        return dotProduct;
    }

    public Vector(int size)
    {
        if (size <= 0)
        {
            throw new ArgumentException("Size must be > 0", nameof(size));
        }

        Components = Enumerable.Repeat(0.0, size).ToList();
    }

    public Vector(Vector vector)
    {
        Components = [.. vector.Components];
    }

    public Vector(double[] values)
    {
        Components = [.. values];
    }

    public Vector(int size, double[] values)
    {
        if (size <= 0)
        {
            throw new ArgumentException("Size must be > 0", nameof(size));
        }

        Components = [.. values];

        if (size > values.Length)
        {
            Components.AddRange(Enumerable.Repeat(0.0, size - values.Length));
        }
    }

    public double this[int index]
    {
        get
        {
            if (index < 0)
            {
                throw new ArgumentException("index must be > 0", nameof(index));
            }

            return Components[index];
        }
        set
        {
            if (index < 0)
            {
                throw new ArgumentException("index must be > 0", nameof(index));
            }

            Components[index] = value;
        }
    }

    public void Add(Vector vector)
    {
        int smallestComponentsSize = Math.Min(Components.Count, vector.GetSize());
        List<double> componentsCopy = [.. Components.Count > vector.GetSize() ? Components : vector.Components];

        for (int i = 0; i < smallestComponentsSize; i++)
        {
            componentsCopy[i] = Components[i] + vector.Components[i];
        }

        Components = componentsCopy;
    }

    public void Subtract(Vector vector)
    {
        List<double> subtrahendComponentsCopy = [.. vector.Components];

        if (Components.Count < subtrahendComponentsCopy.Count)
        {
            Components.AddRange(Enumerable.Repeat(0.0, subtrahendComponentsCopy.Count - Components.Count));
        }
        else
        {
            subtrahendComponentsCopy.AddRange(Enumerable.Repeat(0.0, Components.Count - subtrahendComponentsCopy.Count));
        }

        for (int i = 0; i < Components.Count; i++)
        {
            Components[i] = Components[i] - subtrahendComponentsCopy[i];
        }
    }

    public void MultiplyByScalar(double scalar)
    {
        for (int i = 0; i < Components.Count; i++)
        {
            Components[i] = Components[i] * scalar;
        }
    }

    public void Reverse() => MultiplyByScalar(-1);

    public int GetSize() => Components.Count;

    public double GetLength() => Math.Round(Math.Sqrt(Components.Select(component => Math.Pow(component, 2)).ToList().Sum()), 2, MidpointRounding.AwayFromZero);

    public override string ToString() => $"{{{string.Join(", ", Components)}}}";

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(obj, this))
        {
            return true;
        }

        if (obj is null || GetType() != obj.GetType())
        {
            return false;
        }

        Vector vector = (Vector)obj;

        if (Components.Count != vector.GetSize())
        {
            return false;
        }

        for (int i = 0; i < Components.Count; i++)
        {
            if (Components[i] != vector.Components[i])
            {
                return false;
            }
        }

        return true;
    }

    public override int GetHashCode()
    {
        const int prime = 31;
        int hash = 1;

        foreach (double component in Components)
        {
            hash += prime * hash + component.GetHashCode();
        }

        hash = prime * hash + Components.Count;
        return hash;
    }
}
