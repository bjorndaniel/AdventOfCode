namespace AoC2022;
public static class Day20
{
    public static DoublyLinkedList ParseInput(string filename, bool part2 = false)
    {
        var lines = File.ReadAllLines(filename);
        var list = new DoublyLinkedList();
        var counter = 0;
        foreach (var l in lines)
        {
            list.AddLast(long.Parse(l) * (part2 ? 811589153 : 1), counter);
            counter++;
        }
        return list;
    }

    public static long SolvePart1(string filename, IPrinter printer)
    {
        var keys = ParseInput(filename);
        //printer.Print(keys.Print());
        //printer.Flush();
        //printer.Flush();
        return Solver(printer, keys);
    }

    private static long Solver(IPrinter printer, DoublyLinkedList keys, int nrOfRounds = 1)
    {
        //    printer
        //.Print(keys.Print());
        //    printer.Flush();
        //    printer.Flush();
        var mod = keys.Count - 1;
        for (int mix = 0; mix < nrOfRounds; mix++)
        {
            for (int i = 0; i < keys.Count; i++)
            {
                var k = keys.GetOriginalElementAt(i);
                //printer.Print($"Mix {mix} - {k.Value} - {k.Next?.Value} - {k.Previous?.Value}");
                //printer.Flush();
                var current = keys.GetPosition(k);
                if (k.Value == 0)
                {
                    continue;
                }
                var value = k.Value + current;
                value = (value % mod + mod) % mod;
                if (value == 0)
                {
                    value = keys.Count - 1;
                }
                //if (value >= keys.Count)
                //{
                //    value = 0;
                //}
                keys.Move(current, value);
                //printer
                //    .Print(keys.Print());
                //printer.Flush();
                //printer.Flush();
                var head = keys.GetElementAt(0);
                if (head.Next!.Previous!.Id != head.Id)
                {
                    head.Next.Previous = head;
                }
            }
            //printer
            //.Print(keys.Print());
            //printer.Flush();
        }

        //printer
        //    .Print(keys.Print());
        //printer.Flush();
        //printer.Flush();
        var index = keys.GetIndexOfValue(0);
        var index1 = ((1000 + index) % keys.Count);
        var index2 = ((2000 + index) % keys.Count);
        var index3 = ((3000 + index) % keys.Count);
        var k1 = keys.GetElementAt(index1).Value;
        var k2 = keys.GetElementAt(index2).Value;
        var k3 = keys.GetElementAt(index3).Value;
        return k1 + k2 + k3;
    }

    public static long SolvePart2(string filename, IPrinter printer)
    {
        var keys = ParseInput(filename, true);
        return Solver(printer, keys, 10);
    }
}

public class DoublyLinkedList
{
    private Node? _head;
    private Node? _tail;

    public void AddLast(long value, int originalPosition)
    {
        var newNode = new Node(value, originalPosition) { Previous = _tail };

        if (_tail is null)
        {
            _head = newNode;
        }
        else
        {
            _tail.Next = newNode;
        }

        _tail = newNode;
        Count++;
    }

    public void Move(long fromIndex, long toIndex)
    {

        if (fromIndex < 0 || fromIndex >= Count)
        {
            throw new ArgumentOutOfRangeException($"From: {fromIndex}");
        }

        if (toIndex < 0 || toIndex >= Count)
        {
            throw new ArgumentOutOfRangeException($"To: {toIndex}");
        }

        // Find the node to move
        var current = _head;
        for (long i = 0; i < fromIndex; i++)
        {
            current = current!.Next;
        }

        // Remove the node from its current position
        if (current!.Previous is null)
        {
            _head = current.Next;
        }
        else
        {
            current.Previous.Next = current.Next;
        }

        if (current.Next is null)
        {
            _tail = current.Previous!;
        }
        else
        {
            current.Next.Previous = current.Previous!;
        }

        // Insert the node into its new position
        if (toIndex == 0)
        {
            current.Next = _head!;
            _head!.Previous = current;
            _head = current;
            current.Previous = null;
        }
        else if (toIndex == Count - 1) // Special case for moving to the last position
        {
            _tail!.Next = current;
            current.Previous = _tail;
            _tail = current;
            current.Next = null;
        }
        else
        {
            var target = _head;
            for (long i = 0; i < toIndex; i++)
            {
                target = target!.Next;
            }

            current.Next = target!;
            current.Previous = target!.Previous;
            target!.Previous!.Next = current;
            target.Previous = current;
        }
    }

    public int Count { get; set; }

    public Node GetOriginalElementAt(int index)
    {
        var current = _head;
        for (int i = 0; i < Count; i++)
        {
            if (current!.OriginalPosition == index)
            {
                return current;
            }
            current = current!.Next;
        }
        return current!;
        //throw new ArgumentOutOfRangeException($"Index {index} was out of range");
    }

    public Node GetElementAt(int index)
    {
        var current = _head;
        for (int i = 0; i < index; i++)
        {
            current = current!.Next;
        }
        return current!;
    }

    public int GetPosition(Node n)
    {
        var current = _head;
        for (int i = 0; i < Count; i++)
        {
            if (current!.Id == n.Id)
            {
                return i;
            }
            current = current.Next;
        }
        throw new ArgumentOutOfRangeException($"Could not find node {n.Id}");
    }

    public IEnumerable<Node> GetAll()
    {
        var current = _head;
        for (int i = 0; i < Count; i++)
        {
            yield return GetElementAt(i);
        }
    }

    public int GetIndexOfValue(long value)
    {
        var current = _head; ;
        for (int i = 0; i < Count; i++)
        {
            if (current!.Value == value)
            {
                return i;
            }
            current = current.Next;
        }
        throw new ArgumentException($"Value not found {value}");
    }

    public string Print() =>
        GetAll()
        .Select(_ => _.Value.ToString())
        .Aggregate((a, b) => $"{a}, {b}");
}

public class Node
{
    public Node(long value, int originalPosition)
    {
        Value = value;
        OriginalPosition = originalPosition;
        Id = Guid.NewGuid();
    }
    public Guid Id { get; }
    public Node? Next { get; set; }
    public Node? Previous { get; set; }
    public long Value { get; }
    public int OriginalPosition { get; }
}