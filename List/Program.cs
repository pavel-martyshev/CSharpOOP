namespace List;

internal class Program
{
    static void Main(string[] args)
    {
        SinglyLinkedList<int> list1 = new();
        list1.AddFirst(1);
        list1.AddByIndex(1, 2);

        SinglyLinkedList<int> list2 = list1.Copy();
        Console.WriteLine(list2);

        list1.Reverse();
        Console.WriteLine(list1);

        list1.RemoveByIndex(0);
        list1.RemoveByValue(1);

        Console.WriteLine(list1);
        Console.WriteLine(list1.Count);
        Console.WriteLine(list2);
    }
}
