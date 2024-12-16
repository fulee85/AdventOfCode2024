using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Common;

public static class LinkedListExtensions
{
    public static LinkedListNode<T>? GetMin<T>(this LinkedList<T> list, Func<T,int> selector)
    {
        int minValue = int.MaxValue;
        var current = list.First;
        LinkedListNode<T>? minNode = current;
        while (current is not null)
        {
            if (selector(current.Value) <= minValue)
            {
                minNode = current;
                minValue = selector(current.Value);
            }
            current = current.Next;
        }

        return minNode;
    }

    public static LinkedListNode<T>? Find<T>(this LinkedList<T> list, Func<T, bool> predicate)
    {
        var current = list.First;
        while (current is not null)
        {
            if (predicate(current.Value))
            {
                return current;
            }
            current = current.Next;
        }

        return null;
    }
}
