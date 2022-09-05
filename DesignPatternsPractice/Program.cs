using System;

namespace DesignPatternsPractice // Note: actual namespace depends on the project name.
{

    //Build out the binary tree
    public class Node<T>
    {
        public T Value;
        public Node<T> Left, Right;
        public Node<T> Parent;

        public Node(T value)
        {
            Value = value;
        }

        public Node(T value, Node<T> left, Node<T> right)
        {
            Value = value;
            Left = left;
            Right = right;

            left.Parent = right.Parent = this;
        }
    }

    //Build out the Iterator 
    public class InOrderIterator<T>
    {
        //important elements: root, Current, MoveNext()

        private readonly Node<T> root;
        public Node<T> Current;

        public InOrderIterator(Node<T> root)
        {
            this.root = root;
            Current = root;
            while(Current.Left != null)
            {
                Current = Current.Left;
            }
        }

        public bool MoveNext()
        {
            return Current.Right != null;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}