namespace Lambdas;

internal class Person(string name, int value)
{
    public string Name { get; private set; } = name;

    private int _age = value;

    public int Age
    {
        get => _age;

        set
        {
            if (value <= 0)
            {
                throw new ArgumentException($"Age ({value}) cannot be less or equal to 0", nameof(value));
            }

            _age = value;
        }
    }

    public override string ToString()
    {
        return $"Name = {Name}, age = {Age}";
    }
}
