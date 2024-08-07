using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphs_and_stuff
{
    public class AdjacencyMatrix
    {
        private int numOfNodes = 9;
        private List<Node> nodes;
        private int[,] adjacencyMatrix;

        public AdjacencyMatrix()
        {
            nodes = new List<Node>();
            adjacencyMatrix = new int[numOfNodes, numOfNodes];
        }

        public List<Node> Neighbors(Node n)
        {
            List<Node> neighbours = new List<Node>();
            int index = nodes.IndexOf(n);
            for (int i = 0; i < nodes.Count; i++)
            {
                if (adjacencyMatrix[index,i] != 0)
                {
                    neighbours.Add(nodes[i]);
                }
            }
            return neighbours;
        }

    }
}
