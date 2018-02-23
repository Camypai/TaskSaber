using System;
using System.Collections.Generic;
using System.IO;

namespace TaskSaber
{
    public class ListRand
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(FileStream stream)
        {
            var listNodes = new List<ListNode>();
            var listNode = new ListNode();
            listNode = Head;

            while (listNode != null)
            {
                listNodes.Add(listNode);
                listNode = listNode.Next;
            }

            using (var sw = new StreamWriter(stream))
            {
                foreach (var node in listNodes)
                {
                    sw.WriteLine($"{node.Data}:{listNodes.IndexOf(node.Rand)}");
                }
            }
        }

        public void Deserialize(FileStream stream)
        {
            var listNodes = new List<ListNode>();
            var listNode = new ListNode();
            Count = 0;

            try
            {
                using (var sr = new StreamReader(stream))
                {
                    while (!sr.EndOfStream)
                    {

                        listNode.Data = sr.ReadLine();
                        var next = new ListNode();
                        listNode.Next = next;
                        listNodes.Add(listNode);
                        next.Prev = listNode;
                        listNode = next;
                        Count++;
                    }
                }

                Tail = listNode.Prev;
                Tail.Next = null;

                foreach (var node in listNodes)
                {
                    node.Rand = listNodes[Convert.ToInt32(node.Data.Split(':')[1])];
                    node.Data = node.Data.Split(':')[0];
                }
            }
            catch (ListException ex)
            {
                Console.WriteLine($"{ex.Message} {ex.Source} {ex.StackTrace}");
            }
        }
    }
}