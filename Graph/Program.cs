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

        static void Traversal(Node startNode)
        {
            Console.WriteLine(startNode.cipher);
            if (startNode.LeftChild != null)
            {
                Traversal(startNode.LeftChild);
            }
            
            if (startNode.RightChild != null)
            {
                Traversal(startNode.RightChild);
            }
        }

        static void Main(string[] args)
        {
            Node hill = new Node("hill", null, null);
            Node nihilist = new Node("nihilist", null, null);
            Node railfence = new Node("railfence", null, null);
            Node polybius = new Node("polybius", railfence, nihilist);
            Node vigenere = new Node("vigenere", null, hill);
            Node substitution = new Node("substitution", vigenere, polybius);
            Node affine = new Node("affine", null, null);
            Node caesar = new Node("caesar", affine, substitution);
            Node baconian = new Node("baconian", caesar, null);
            
            Traversal(baconian);

            Console.ReadKey();

        }
    }
}
