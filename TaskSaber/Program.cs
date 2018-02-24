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
            try
            {
                res.Prev = prev;
                res.Data = rand.Next(0, 1000).ToString();
                prev.Next = res;
                return res;
            }
            catch (ListException)
            {
                throw;
            }
        }

        private static ListNode SetRandomNode(ListNode node, int length)
        {
            var i = 0;
            var j = rand.Next(0, length);
            var res = node;

            try
            {
                while (i < j)
                {
                    res = res.Prev ?? res.Next;
                    i++;
                }

                return res;
            }
            catch (ListException)
            {
                throw;
            }
        }

        public static void Main(string[] args)
        {
            try
            {
                var head = new ListNode();
                var tail = new ListNode();
                const int length = 10;

                head.Data = rand.Next(0, 1000).ToString();

                for (var i = 1; i < length; i++)
                {
                    head = SetNode(head);
                }

                tail = head;

                for (var i = 0; i < length; i++)
                {
                    head.Rand = SetRandomNode(head, length);
                    head = head.Prev ?? head;
                }

                var listRandSerialized = new ListRand
                {
                    Count = length,
                    Head = head,
                    Tail = tail
                };

                var fout = new FileStream("taskSaber.task", FileMode.Create);
                listRandSerialized.Serialize(fout);

                var listRandDeserialized = new ListRand();

                var fin = new FileStream("taskSaber.task", FileMode.Open);
                listRandDeserialized.Deserialize(fin);

                var @equals = listRandSerialized.Tail.Data.Equals(listRandDeserialized.Tail.Data);
                Console.WriteLine(@equals);
                Console.WriteLine($"{listRandSerialized.Tail.Data} {listRandDeserialized.Tail.Data}");
                Console.ReadLine();
            }
            catch (ListException ex)
            {
                Console.WriteLine($"{ex.Message} {ex.Source} {ex.StackTrace}");
                Console.ReadLine();
            }
        }
    }
}