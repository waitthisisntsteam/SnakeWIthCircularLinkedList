using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Net.Http.Headers;
using System;
using System.Text.RegularExpressions;

namespace linkedLists
{
    public class CircularLinkedList<T>
    {
        public int Count { get; private set; }
        public CircularLinkedListNode<T> Head { get; private set; }
        public CircularLinkedListNode<T> Tail { get; private set; }

        public CircularLinkedList()
        {
            Clear();
        }

        public void AddNodeToFront(T value)
        {
            if (Head == null)
            {
                Head = new CircularLinkedListNode<T>(value);
                Tail = Head;
                Head.prev = Tail;
                Tail.next = Head;
            }
            else
            {
                CircularLinkedListNode<T> current = new CircularLinkedListNode<T>(value);
                Head.prev = null;
                Head.prev = current;
                current.next = Head;
                Head = current;
                Head.prev = Tail;
                Tail.next = Head;
            }
            Count++;
        }
        public void AddNodeToLast(T value)
        {
            if (Head == null)
            {
                AddNodeToFront(value);
            }
            else
            {
                CircularLinkedListNode<T> current = new CircularLinkedListNode<T>(value);
                Tail.next = null;
                Tail.next = current;
                current.prev = Tail;
                Tail = current;
                Tail.next = Head;
                Head.prev = Tail;
                Count++;
            }
        }

        public void AddBefore(CircularLinkedListNode<T> nodeAhead, T value)
        {
                      
            if (nodeAhead == Head)
            {
                AddNodeToFront(value);
                return;
            }

            CircularLinkedListNode<T> runner = Head;
            CircularLinkedListNode<T> current = new CircularLinkedListNode<T>(value);


            for (int i = 0; i < Count; i++)
            {
                if (runner.next == null)
                {
                    return;
                }
                if (runner.next == nodeAhead)
                {
                    runner.next = current;
                    current.next = nodeAhead;
                }
                runner = runner.next;
            }
            if (nodeAhead.next == null)
            {
                Tail.next = null;                
                Tail = nodeAhead;
                Tail.next = Head;
            }
            Count++;
        }

        public void AddAfter(CircularLinkedListNode<T> nodeBehind, T value)
        {
            CircularLinkedListNode<T> current = new CircularLinkedListNode<T>(value);
            if (nodeBehind == null)
            {
                return;
            }
            else
            {
                if (nodeBehind == Tail)
                {
                    AddNodeToLast(value);
                    return;
                }
                current.next = nodeBehind.next;
                nodeBehind.next = current;
                current.prev = nodeBehind;
                if (current.next == null)
                {                   
                    Tail = current;
                    Tail.next = null;
                    Tail.next = Head;
                }
            }
            Count++;
        }

        public CircularLinkedListNode<T> FindNode(T val)
        {
            var current = Head;

            while (current != null)
            {
                if (current.data.Equals(val) && !val.Equals(Head.prev) && !val.Equals(Tail.next))
                {
                    break;
                }

                current = current.next;
            }

            return current;
        }
        public bool RemoveFirst()
        {
            if (Head == null)
            {
                return false;
            }
            else
            {
                if (Head == Tail)
                {
                    Clear();
                    return true;
                }
                else
                {
                    Head.prev = null;
                    Head = Head.next;
                    Head.prev = null;
                    Head.prev = Tail;
                    Count--;
                    return true;
                }
            }
        }
        public bool RemoveLast()
        {
            if (Head == null)
            {
                return false;
            }
            else
            {
                if (Head == Tail)
                {
                    Clear();
                    return true;
                }
                else
                {
                    Tail.next = null;
                    Tail = Tail.prev;
                    Tail.next = null;
                    Tail.next = Head;
                }
                Count--;
                return true;
            }
        }
        public bool Remove(T value)
        {
            var node = FindNode(value);

            if (node == null)
            {
                return false;
            }

            if (node == Head)
            {
                RemoveFirst();
                return true;
            }

            if (node == Tail)
            {
                RemoveLast();
                return true;
            }

            var nodeafter = node.next;
            var nodebefore = node.prev;
            nodebefore.next = nodeafter;
            Count--;
            return true;
        }

        public void Clear()
        {       
            Head = null;
            Tail = null;           
            Count = 0;
        }

        public void PrintList()
        {
            int i = 0;
            Console.WriteLine("-------------------------");
            CircularLinkedListNode<T> runner = Head;
            while (i < Count)
            {
                Console.WriteLine(runner.data);
                runner = runner.next;
                i++;
            }
            Console.WriteLine("-------------------------");

        }

        public void PrintListBackwards()
        {

        }

        public override string ToString()
        {
            return $"Count: {Count}, Head: {Head?.ToString() ?? "Null"}, Tail: {Tail?.ToString() ?? "Null"}";
        }
    }
}
