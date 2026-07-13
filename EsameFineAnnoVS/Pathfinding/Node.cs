using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsameFineAnnoVS
{
    class Node
    {
        public int X { get;  set; }
        public int Y { get;  set; }
        public int Cost { get; private set; }
        public List<Node> Neightbours { get; }

        public Node(int x, int y, int cost)
        {
            X = x;
            Y = y;
            Cost = cost;

            Neightbours = new List<Node>();
        }

        public void AddNeighbour(Node node)
        {
            Neightbours.Add(node);
        }

        public void RemoveNeighbour(Node node)
        {
            Neightbours.Remove(node);
        }

        public void SetCost(int cost)
        {
            Cost = cost;
        }
    }
}
