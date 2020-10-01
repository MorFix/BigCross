using System;

namespace NearestPoint
{

    public class BinarySearchTree<T> where T : IComparable<T>
    {
        public BinaryNode<T> Root { get; set; }

        public BinarySearchTree()
        {
            Root = null;
        }

        public void Insert(T item)
        {
            BinaryNode<T> parent = null;
            BinaryNode<T> current = Root;

            int compare = 0;
            
            while (current != null)
            {
                parent = current;
                compare = current.Value.CompareTo(item);
                current = compare < 0 ? current.RightChild : current.LeftChild;
            }

            BinaryNode<T> newNode = new BinaryNode<T>(item);

            if (parent != null)
            {
                if (compare < 0)
                {
                    parent.RightChild = newNode;
                }
                else
                {
                    parent.LeftChild = newNode;
                }
            } else {
                Root = newNode;
            }
        }
    }
}
