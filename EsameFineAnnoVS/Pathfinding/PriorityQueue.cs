using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsameFineAnnoVS
{
    class PriorityQueue
    {
        Dictionary<Node, int> items;
        public bool IsEmpty { get { return items.Count == 0; } }

        public PriorityQueue()
        {
            items = new Dictionary<Node, int>();
        }

        public void Enqueue(Node node, int priority)
        {
            if(!items.ContainsKey(node))
            {
                items.Add(node, priority);
            }
        }

        public Node Dequeue() //Return and remove form the list the lowest cost neighbour given a single Node
        {
            Node node = null;
            int lowestPriority = int.MaxValue;

            foreach(Node item in items.Keys)
            {
                int currentPriority = items[item];

                if(currentPriority < lowestPriority)
                {
                    lowestPriority = currentPriority;
                    node = item;
                }
            }

            items.Remove(node);

            return node;
        }
    }
}
