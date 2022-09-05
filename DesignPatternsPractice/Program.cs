using System;

namespace DesignPatternsPractice 
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
        private bool yieldStart;

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
            if(!yieldStart)
            {
                yieldStart = true;
                return true;
            }

            if(Current.Right != null)
            {
                Current = Current.Right;
                while(Current.Left != null)
                {
                    Current = Current.Left;                 
                }
                return true;
            }
            else
            {
                var parent = Current.Parent;
                while(parent != null && Current == parent.Right)
                {
                    Current = parent;
                    parent = parent.Parent;
                }
                Current = parent;
                return Current != null;
            }

        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            //       1
            //      / \ 
            //     2   3  

            //in-order: 213
            var root = new Node<int>(1, new Node<int>(2), new Node<int>(3));

            var iterator = new InOrderIterator<int>(root);
            while(iterator.MoveNext())
            {
                Console.Write(iterator.Current.Value + ", "); //hmm that last comma kind of ugly.
            }
        }
    }
}