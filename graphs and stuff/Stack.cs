using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace graphs_and_stuff
{
    public class Stack<T>
    {
        private T[] stack;
        private int top = -1;

        public Stack(int max)
        {
            stack = new T[max];
        }

        public void Push(T item)
        {
            if(!isFull())
            {
                top++;
                stack[top] = item;                
            }
        }

        public T Pop()
        {
            if(!isEmpty())
            {
                top--;
                return stack[top+1];
            }
            return default;
        }

        public T Peek()
        {
            if (!isEmpty())
            {
                return stack[top];
            }
            return default;
        }

        public bool isEmpty()
        {
            if(top < 0)
            {
                return true;
            }
            return false;
        }

        public bool isFull()
        {
            if (top == stack.Length-1)
            {
                return true;
            }
            return false;
        }
    }
}
