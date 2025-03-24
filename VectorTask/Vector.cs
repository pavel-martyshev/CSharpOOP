namespace VectorTask;

public class Vector
{
    private double[] _components;

    public int Size => _components.Length;

    public Vector(int size)
    {
        if (size <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(size), $"Size ({size}) must be greater than 0.");
        }

        _components = new double[size];
    }

    public Vector(Vector vector)
    {
        _components = new double[vector.Size];

        Array.Copy(vector._components, _components, vector.Size);
    }

    public Vector(double[] components)
    {
        if (components.Length == 0)
        {
            throw new ArgumentException("The array cannot be empty.", nameof(components));
        }

        _components = new double[components.Length];
        Array.Copy(components, _components, components.Length);
    }

    public Vector(int size, double[] components)
    {
        if (size <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(size), $"Size ({size}) must be greater than 0");
        }

        _components = new double[size];
        Array.Copy(components, _components, Math.Min(components.Length, size));
    }

    public double this[int index]
    {
        get
        {
            if (index < 0 || index >= _components.Length)
            {
                throw new IndexOutOfRangeException($"The index ({index}) must be greater than or equal to 0 and less than the length ({_components.Length}).");
            }

            return _components[index];
        }

        set
        {
            if (index < 0 || index >= _components.Length)
            {
                throw new IndexOutOfRangeException($"The index ({index}) must be greater than or equal to 0 and less than the length ({_components.Length}).");
            }

            _components[index] = value;
        }
    }

    public static Vector GetSum(Vector vector1, Vector vector2)
    {
        Vector resultVector = new(vector1);
        resultVector.Add(vector2);

        return resultVector;
    }

    public static Vector GetDifference(Vector vector1, Vector vector2)
    {
        Vector resultVector = new(vector1);
        resultVector.Subtract(vector2);

        return resultVector;
    }

    public static double GetDotProduct(Vector vector1, Vector vector2)
    {
        int minSize = Math.Min(vector1.Size, vector2.Size);

        double dotProduct = 0;

        for (int i = 0; i < minSize; i++)
        {
            dotProduct += vector1._components[i] * vector2._components[i];
        }

        return dotProduct;
    }

    public void Add(Vector vector)
    {
        if (_components.Length < vector.Size)
        {
            Array.Resize(ref _components, vector.Size);
        }

        for (int i = 0; i < vector.Size; i++)
        {
            _components[i] += vector._components[i];
        }
    }

    public void Subtract(Vector vector)
    {
        if (_components.Length < vector.Size)
        {
            Array.Resize(ref _components, vector.Size);
        }

        for (int i = 0; i < vector.Size; i++)
        {
            _components[i] -= vector._components[i];
        }
    }

    public void MultiplyByScalar(double scalar)
    {
        for (int i = 0; i < _components.Length; i++)
        {
            _components[i] *= scalar;
        }
    }

    public void Reverse() => MultiplyByScalar(-1);

    public double GetLength() => Math.Sqrt(_components.Sum(component => component * component));

    public override string ToString() => $"{{{string.Join(", ", _components)}}}";

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

        if (_components.Length != vector.Size)
        {
            return false;
        }

        for (int i = 0; i < _components.Length; i++)
        {
            if (_components[i] != vector._components[i])
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

        foreach (double component in _components)
        {
            hash = prime * hash + component.GetHashCode();
        }

        return hash;
    }
}
