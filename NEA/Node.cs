using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA
{
    public class Node
    {
        public string cipher;
        public Node LeftChild;
        public Node RightChild;

        public Node(string c, Node l, Node r)
        {
            cipher = c;
            LeftChild = l;
            RightChild = r;
        }
    }
}
