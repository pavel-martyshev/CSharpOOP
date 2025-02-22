namespace VectorTask;

public class Vector
{
    private double[] components;

    public int Size { get; private set; }

    public Vector(int size)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(size, 0);

        components = [.. Enumerable.Repeat(0.0, size)];
        Size = size;
    }

    public Vector(Vector vector)
    {
        components = [.. vector.components];
        Size = components.Length;
    }

    public Vector(double[] components)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(components.Length, 0);

        this.components = [.. components];
        Size = this.components.Length;
    }

    public Vector(int size, double[] components)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(size, 0);

        ArgumentOutOfRangeException.ThrowIfLessThan(size, components.Length);

        this.components = new double[size];
        Size = size;

        Array.Copy(components, 0, this.components, 0, components.Length);
    }

    public double this[int index]
    {
        get
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);

            ArgumentOutOfRangeException.ThrowIfGreaterThan(index, Size - 1);

            return components[index];
        }
        set
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);

            ArgumentOutOfRangeException.ThrowIfGreaterThan(index, Size - 1);

            components[index] = value;
        }
    }

    public static Vector GetVectorsAddition(Vector vector1, Vector vector2)
    {
        Vector resultVector = new(vector1);
        resultVector.Add(vector2);

        return resultVector;
    }

    public static Vector GetVectorsSubtraction(Vector vector1, Vector vector2)
    {
        Vector resultVector = new(vector1);
        resultVector.Subtract(vector2);

        return resultVector;
    }

    public static double GetDotProduct(Vector vector1, Vector vector2)
    {
        int smallestComponentsSize = Math.Min(vector1.Size, vector2.Size);
        double dotProduct = 0;

        for (int i = 0; i < smallestComponentsSize; i++)
        {
            dotProduct += vector1.components[i] * vector2.components[i];
        }

        return dotProduct;
    }

    private void ExpandComponents(int size)
    {
        double[] temp = new double[size];
        Array.Copy(components, 0, temp, 0, Size);

        components = temp;
        Size = components.Length;
    }

    public void Add(Vector vector)
    {
        if (Size < vector.Size)
        {
            ExpandComponents(vector.Size);
        }

        for (int i = 0; i < vector.Size; i++)
        {
            components[i] += vector.components[i];
        }
    }

    public void Subtract(Vector vector)
    {
        if (Size < vector.Size)
        {
            ExpandComponents(vector.Size);
        }

        for (int i = 0; i < vector.Size; i++)
        {
            components[i] -= vector.components[i];
        }
    }

    public void MultiplyByScalar(double scalar)
    {
        for (int i = 0; i < Size; i++)
        {
            components[i] *= scalar;
        }
    }

    public void Reverse() => MultiplyByScalar(-1);

    public double GetLength() => Math.Sqrt(components.Select(component => component * component).Sum());

    public override string ToString() => $"{{{string.Join(", ", components)}}}";

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

        if (Size != vector.Size)
        {
            return false;
        }

        for (int i = 0; i < Size; i++)
        {
            if (components[i] != vector.components[i])
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

        foreach (double component in components)
        {
            hash = prime * hash + component.GetHashCode();
        }

        return hash;
    }
}
