using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Menu
{
    internal class Program
    { 
        public void Traversal(Node[] tree, int startnode)
        {
            if(tree[startnode].leftPointer != -1)
            {
                Traversal(tree, tree[startnode].leftPointer);
            }
            Console.WriteLine(tree[startnode].cipher);
            if (tree[startnode].rightPointer != -1)
            {
                Traversal(tree, tree[startnode].rightPointer);
            }
        }


        static void Main(string[] args)
        {
            Node baconian = new Node()

            Node[] tree = { new Node("baconian" ),  };

        }
    }
}
