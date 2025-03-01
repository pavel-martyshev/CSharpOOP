namespace List;

internal class Program
{
    static void Main(string[] args)
    {
        SinglyLinkedList<int> list1 = new(1);
        list1.AddFirst(new ListItem<int>(3));
        list1.AddByIndex(1, 2);

        SinglyLinkedList<int> list2 = list1.Copy();

        Console.WriteLine(list2.GetValueByIndex(0));
        Console.WriteLine(list2.GetValueByIndex(1));
        Console.WriteLine(list2.GetValueByIndex(2));

        list1.Expand();

        Console.WriteLine(list1.GetValueByIndex(0));
        Console.WriteLine(list1.GetValueByIndex(1));
        Console.WriteLine(list1.GetValueByIndex(2));
    }
}
