using System;

namespace NearestPoint
{
    public class BinaryNode<T> where T : IComparable<T>
    {
        public T Value { get; set; }
        public BinaryNode<T> LeftChild { get; set; }
        public BinaryNode<T> RightChild { get; set; }
        public BinaryNode(T node)
        {
            Value = node;
        }
    }
}
