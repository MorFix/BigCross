using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NearestPoint
{
    public class BinaryNode<T> where T : IComparable<T>
    {
        public T Node { get; set; }
        public BinaryNode<T> Parent { get; set; }
        public BinaryNode<T> LeftChild { get; set; }
        public BinaryNode<T> RightChild { get; set; }
        public BinaryNode(T node)
        {
            Node = node;
        }
    }
}
