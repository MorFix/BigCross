namespace Snakes
{
    public class LinkedList<T>
    {
        public LinkedListNode<T> Head { get; set; }

        public LinkedListNode<T> AddLast(T data)
        {
            var newNode = new LinkedListNode<T>(data);

            if (Head == null)
            {
                Head = newNode;

                return newNode;
            }

            var current = Head;

            while(current.Next != null)
            {
                current = current.Next;
            }

            current.Next = newNode;

            return newNode;
        }
    }

    public class LinkedListNode<T>
    {
        public T Data { get; set; }
        public LinkedListNode<T> Next { get; set; }
    
        public LinkedListNode(T data)
        {
            Data = data;
        }
    }
}
