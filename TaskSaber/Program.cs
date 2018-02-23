using System;
using System.Collections.Generic;
using System.IO;

namespace TaskSaber
{
    internal class Program
    {
        static Random rand = new Random();

        private static ListNode SetNode(ListNode prev)
        {
            var res = new ListNode();
            res.Prev = prev;
            res.Data = rand.Next(0, 1000).ToString();
            prev.Next = res;
            return res;
        }

        private static ListNode SetRandomNode(ListNode node, int length)
        {
            var i = 0;
            var j = rand.Next(0, length);
            var res = node;

            while (i<j)
            {
                res = res.Next ?? res;
                i++;
            }

            return res;
        }

        public static void Main(string[] args)
        {
            var node = new ListNode();
            const int length = 10;

            node.Data = rand.Next(0, 1000).ToString();

            for (var i = 0; i < length; i++)
            {
                node = SetNode(node);
            }
            
            for (var i = 0; i < length; i++)
            {
                node.Rand = SetRandomNode(node, length);
                node = node.Next;
            }

            var listRandSerialized = new ListRand
            {
                Count = length,
                Head = node,
                Tail = node
            };

            var fout = new FileStream("taskSaber.task", FileMode.OpenOrCreate);
            listRandSerialized.Serialize(fout);
            
            var listRandDeserialized = new ListRand();
            
            var fin = new FileStream("taskSaber.task", FileMode.Open);
            listRandDeserialized.Deserialize(fin);

            var @equals = listRandSerialized.Equals(listRandDeserialized);
            Console.WriteLine(@equals);
            Console.ReadLine();
        }
    }
}