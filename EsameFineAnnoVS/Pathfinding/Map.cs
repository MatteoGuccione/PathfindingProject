using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OpenTK;

namespace EsameFineAnnoVS
{
    class Map
    {
        // Pathfinding
        Dictionary<Node, Node> cameFrom;    // parents
        Dictionary<Node, int> costSoFar;    // distances
        PriorityQueue frontier;             // toVisit

        // Map
        int width, height; //width and height of the map
        int[] cells; //Number of cells present in the map

        public Node[] Nodes { get; }        // Map

        public Map(int width, int height, int[] cells)
        {
            this.width = width;
            this.height = height;
            this.cells = cells;

            Nodes = new Node[cells.Length];

            for (int i = 0; i < cells.Length; i++)
            {
                int x = i % width;
                int y = i / width;
                Nodes[i] = new Node(x, y, cells[i]);
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * width + x;

                    AddNeighbours(Nodes[index], x, y);
                }
            }
        }

        private void AddNeighbours(Node node, int x, int y)
        {
            // Check neighbpurs in each direction

            // TOP
            CheckNeighbours(node, x, y - 1);
            // BOTTOM
            CheckNeighbours(node, x, y + 1);
            // LEFT
            CheckNeighbours(node, x - 1, y);
            // RIGHT
            CheckNeighbours(node, x + 1, y);
        }

        private void CheckNeighbours(Node node, int cellX, int cellY)
        {
            if (cellX < 0 || cellX >= width)
            {
                return;
            }
            if (cellY < 0 || cellY >= height)
            {
                return;
            }

            int index = cellY * width + cellX;

            Node neighbour = Nodes[index];

            if (neighbour.Cost != 9000) //Check to see if the neighbour is a cells the owner can go to
            {
                node.AddNeighbour(neighbour);
            }
        }

        public List<Node> GetPath(int startX, int startY, int endX, int endY) //Given a start node and end node this methods returns a new Path using AStar
        {
            List<Node> path = new List<Node>();

            Node start = GetNode(startX, startY);
            Node end = GetNode(endX, endY);

            if (start.Cost == int.MaxValue || end.Cost == int.MaxValue)
            {
                return path;
            }

            AStar(start, end);

            if (!cameFrom.ContainsKey(end))
            {
                return path;
            }

            Node currNode = end;

            while (currNode != cameFrom[currNode])
            {
                path.Add(currNode);
                currNode = cameFrom[currNode];
            }

            path.Reverse();

            return path;
        }
        public Node GetRandomNode(int lX, int rX, int uY, int dY) //Gets a random node given a min Node and a max Node
        {
            int randomX;
            int randomY;
            Node randomNode;
            do
            {
                randomX = RandomGenerator.GetRandomInt(lX + 1, rX);
                randomY = RandomGenerator.GetRandomInt(uY + 1, dY);
                randomNode = GetNode(randomX, randomY);
                if (randomNode.X > 16 && randomNode.Y < 16)
                {
                    randomNode.X *= 16;
                    randomNode.Y *= 16;
                }
                if (GetNode(randomX, randomY).Cost == 9000)
                {
                    Console.WriteLine();
                }
            }
            while (GetNode(randomX, randomY).Cost == 9000);
            
            return randomNode;
        }


        public Node GetNode(int x, int y) //get the node with the given Position
        {
            if (x < 0 || x >= width)
            {
                return null;
            }

            if (y < 0 || y >= height)
            {
                return null;
            }
            return Nodes[y * width + x];
        }


        private void AStar(Node start, Node end) //Given a start and end Node, AStar return the most optimal path to reach the destination
        {
            cameFrom = new Dictionary<Node, Node>();
            costSoFar = new Dictionary<Node, int>();
            frontier = new PriorityQueue();

            cameFrom[start] = start;
            costSoFar[start] = 0;
            frontier.Enqueue(start, Heuristic(start, end));

            while (!frontier.IsEmpty)
            {
                Node currNode = frontier.Dequeue();

                if (currNode == end)
                {
                    return;
                }

                foreach (Node nextNode in currNode.Neightbours)
                {
                    int newCost = costSoFar[currNode] + nextNode.Cost;

                    if (!costSoFar.ContainsKey(nextNode) || costSoFar[nextNode] > newCost)
                    {
                        cameFrom[nextNode] = currNode;
                        costSoFar[nextNode] = newCost;
                        Node newNode = nextNode;
                        if (nextNode.X > 40 || nextNode.Y > 40)
                        {
                            nextNode.X /= 16;
                            nextNode.Y /= 16;
                        }
                        int priority = newCost + Heuristic(nextNode, end);
                        frontier.Enqueue(nextNode, priority);
                    }
                }
            }
        }

        // Manhattan Distance
        private int Heuristic(Node start, Node end) //Return the manamtam distance
        {
            int endingX = end.X / 16;
            int endingY = end.Y / 16;
            return Math.Abs(start.X - endingX) + Math.Abs(start.Y - endingY);
        }
    }
}
