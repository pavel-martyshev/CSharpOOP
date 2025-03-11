namespace HashTableTask;

internal class Program
{
    static void Main(string[] args)
    {
        HashTable<string> hashTable = [];

        hashTable["Some string 1".GetHashCode()] = ["Some string 1"];

        Console.WriteLine(string.Join(", ", hashTable["Some string 1".GetHashCode()]!));

        hashTable.Add("Some string 2");

        IEnumerator<string> enumerator = hashTable.GetEnumerator();

        while (enumerator.MoveNext())
        {
            Console.WriteLine(enumerator.Current);
        }

        Console.WriteLine(hashTable.Remove("Some string"));
        Console.WriteLine(hashTable.Remove("Some string 1"));
    }
}
