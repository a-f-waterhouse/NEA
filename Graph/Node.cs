using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    public class Node
    {
        public readonly string cipher;
        public readonly Node LeftChild;
        public readonly Node RightChild;

        public Node(string c, Node l, Node r)
        {
            cipher = c;
            LeftChild = l;
            RightChild = r;
        }
    }
}
