using System;
using System.Collections.Generic;

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
                compare = current.Node.CompareTo(item);
                current = compare < 0 ? current.RightChild : current.LeftChild;
            }
            BinaryNode<T> newNode = new BinaryNode<T>(item);
            if (parent != null)
                if (compare < 0)
                    parent.RightChild = newNode;
                else
                    parent.LeftChild = newNode;
            else
                Root = newNode;
            newNode.Parent = parent;
        }

    
        protected BinaryNode<T> FindNode(T item)
        {
            BinaryNode<T> current = Root;
            while (current != null)
            {
                int compare = current.Node.CompareTo(item);
                if (compare == 0)
                    return current;
                if (compare < 0)
                    current = current.RightChild;
                else
                    current = current.LeftChild;
            }

            return null;
        }

        public List<T> GetNodesInOrder(BinaryNode<T> node, List<T> list)
        {
            List<T> nodesList = new List<T>();

            if (node == null)
            {
                return nodesList;
            }

            GetNodesInOrder(node.LeftChild, list);
            list.Add(node.Node);
            GetNodesInOrder(node.RightChild, list);
            return list;
        }
    }

}
