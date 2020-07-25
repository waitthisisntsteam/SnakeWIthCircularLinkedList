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

    public class CircularLinkedListNode<T>
    {
        public T data;
        public CircularLinkedListNode<T> next;
        public CircularLinkedListNode<T> prev;

        public CircularLinkedListNode(T current)
        {
            data = current;
            next = null;
            prev = null;
        }

        public override string ToString()
        {
            string nextVal = next?.data.ToString() ?? "null";
            string prevVal = prev?.data.ToString() ?? "null";

            string output = $"Value: {data.ToString()}, Next: {nextVal}, Prev: {prevVal}";
            return output;
        }
    }

}