namespace HashTableTask;

internal class Program
{
    static void Main(string[] args)
    {
        HashTable<string> hashTable = [];

        hashTable.Add("Some string 1");
        hashTable.Add("Some string 2");

        Console.WriteLine(hashTable);

        foreach (string element in hashTable.AsEnumerable())
        {
            Console.WriteLine(element);
        }

        Console.WriteLine(hashTable.Remove("Some string"));
        Console.WriteLine(hashTable.Remove("Some string 1"));
    }
}
