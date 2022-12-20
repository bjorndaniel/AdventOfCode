namespace AoC2022;
public static class Day20
{
    public static DoublyLinkedList ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var list = new DoublyLinkedList();
        var counter = 0;
        foreach(var l in lines)
        {
            list.AddLast(int.Parse(l), counter);
            counter++;
        }
        return list;
    }

    public static long SolvePart1(string filename, IPrinter printer)
    {
        var keys = ParseInput(filename);
        printer.Print(keys.Print());
        printer.Flush();
        printer.Flush();
        for (int i = 0; i < keys.Count; i++)
        {
            var k = keys.GetOriginalElementAt(i);
            var current = keys.GetPosition(k);
            var value = k.Value + current;
            if(value < 0)
            {
                value = keys.Count - value - 1;
            }
            keys.Move(current, value);
            printer
                .Print(keys.Print());
            printer.Flush();
            printer.Flush();
            _ = "";
        }
        var (k1, k2, k3) = (0, 0, 0);
        var keyCounter = 0;
        for (int i = 0; i < 3002; i++)
        {
            if (i == 1001)
            {
                k1 = keys.GetElementAt(keyCounter).Value;
                Debug.Assert(k1 == 4);
            }
            if (i == 2001)
            {
                k2 = keys.GetElementAt(keyCounter).Value;
                Debug.Assert(k1 == -3);
            }
            if (i == 3001)
            {
                k3 = keys.GetElementAt(keyCounter).Value;
                Debug.Assert(k1 == 2);
            }
            keyCounter++;
            if (keyCounter >= keys.Count)
            {
                keyCounter = 0;
            }
        }
        return k1 + k2 + k3;
    }
}

public class DoublyLinkedList
{
    private Node? _head;
    private Node? _tail;

    public void AddLast(int value, int originalPosition)
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

    public void Move(int fromIndex, int toIndex)
    {
        toIndex %= Count;

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
        for (int i = 0; i < fromIndex; i++)
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
        }
        else if (toIndex == Count - 1) // Special case for moving to the last position
        {
            _tail!.Next = current;
            current.Previous = _tail;
            _tail = current;
        }
        else
        {
            var target = _head;
            for (int i = 0; i < toIndex; i++)
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
            if (current!.OriginalPosition == i)
            {
                return current;
            }
            current = current!.Next;
        }
        throw new ArgumentOutOfRangeException($"Index {index} was out of range");
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
        for(int i = 0; i < Count; i++)
        {
            if(current!.Id == n.Id)
            {
                return i;
            }
            current = current.Next;
        }
        throw new ArgumentOutOfRangeException($"Could not find node {n.Id}");
    }

    public IEnumerable<Node> GetAll()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return GetElementAt(i);
        }
    }

    public string Print() =>
        GetAll()
        .Select(_ => _.Value.ToString())
        .Aggregate((a, b) => $"{a}, {b}");
}

public class Node
{
    public Node(int value, int originalPosition)
    {
        Value = value;
        OriginalPosition = originalPosition;
        Id = Guid.NewGuid();
    }
    public Guid Id { get; }
    public Node? Next { get; set; }
    public Node? Previous { get; set; }
    public int Value { get; }
    public int OriginalPosition { get; }
}