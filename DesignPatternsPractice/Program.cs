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

    //Build out the Iterator from scratch
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

    //Build out the binary tree utlizing C#'s IEnumerables
    public class BinaryTree<T>
    {
        private Node<T> root;

        public BinaryTree(Node<T> root)
        {
            this.root = root;
        }

        public IEnumerable<Node<T>> InOrder
        {
            get
            {
                IEnumerable<Node<T>> Traverse(Node<T> current)
                {
                    //go to the left most element and yield return that element
                    if(current.Left != null)
                    {
                        foreach(var leftNode in Traverse(current.Left))
                        {
                            yield return leftNode;
                        }
                    }
                    yield return current;

                    if(current.Right != null)
                    {
                        foreach(var rightNode in Traverse(current.Right))
                        {
                            yield return rightNode;
                        }
                    }
                }

                foreach(var node in Traverse(root))
                {
                    yield return node;
                }    
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

            var tree = new BinaryTree<int>(root);
            Console.WriteLine(String.Join(",", tree.InOrder.Select(x => x.Value))); //much cleaner
        }
    }
}