// Tal Gavriel, ID: 209209808
// Mor Cohen, ID: 315825356

using System;

namespace Snakes
{
    public class Program
    {
        private static Random _random = new Random();

        public static void Main(string[] args)
        {
            var randomList = IsChanceMet(0.5) ? CreateRandomSnake() : CreateRandomSnail();
            Print(randomList);

            Console.ReadKey();
        }

        private static LinkedList<int> CreateRandomSnake()
        {
            var list = new LinkedList<int>();

            while (!IsChanceMet(0.01))
            {
                list.AddLast(_random.Next());
            }

            return list;
        }

        private static LinkedList<int> CreateRandomSnail()
        {
            LinkedListNode<int> cycleStartNode = null;
            LinkedListNode<int> lastNode = null;

            var list = new LinkedList<int>();

            while (!IsChanceMet(0.02) || cycleStartNode == null)
            {
                lastNode = list.AddLast(_random.Next());

                if (cycleStartNode == null && IsChanceMet(0.015))
                {
                    cycleStartNode = lastNode;
                }
            }

            lastNode.Next = cycleStartNode;

            return list;
        }

        /// <summary>
        /// Return true if the random matches the specified chance
        /// </summary>
        /// <param name="chance">The chance - between 0 and 1</param>
        /// <returns>boolean</returns>
        private static bool IsChanceMet(double chance)
        {
            var digitsAfterPoint = chance.ToString().Split('.')[1]?.Length ?? 0;
            var maxValue = (int) Math.Pow(10, digitsAfterPoint);

            return _random.Next(maxValue) < (chance * maxValue);
        }

        private static void Print(LinkedList<int> list)
        {
            var snailStart = SnakeOrSnale(list);

            // Printing the part until snailStart, which is always a snake
            var bodyLength = PrintSnake(list.Head, snailStart);
            
            // Terminate the print when it's a snake
            if (snailStart == null)
            {
                Console.WriteLine(" → null");
                Console.WriteLine($"Snake Length: {bodyLength}");
                
                return;
            }

            // Printing the cyclic part of the snail
            var cycleLength = PrintSnale(snailStart);

            Console.WriteLine($"Snail cycle length: {cycleLength}");
            Console.WriteLine($"Snail total length: {bodyLength + cycleLength}");
        }

        private static int PrintSnake(LinkedListNode<int> startNode, LinkedListNode<int> endNode)
        {
            var currentNode = startNode;
            var bodyLength = 0;

            while (currentNode != endNode)
            {
                PrintNode(currentNode, endNode);

                bodyLength++;
                currentNode = currentNode.Next;
            }

            return bodyLength;
        }

        private static int PrintSnale(LinkedListNode<int> snailStart)
        {
            var currentNode = snailStart;
            var cycleLength = 0;

            Console.Write(" ↱ ");
            
            do
            {
                PrintNode(currentNode, snailStart);
                
                cycleLength++;
                currentNode = currentNode.Next;
            } while (currentNode != snailStart);
            
            Console.WriteLine(" ↲ ");

            return cycleLength;
        }

        private static void PrintNode(LinkedListNode<int> node, LinkedListNode<int> lastNode)
        {
            Console.Write(node.Data);
            if (node.Next != lastNode)
            {
                Console.Write(" → ");
            }
        }

        private static LinkedListNode<int> SnakeOrSnale(LinkedList<int> list)
        {
            // Moving two pointers, "slow" with 1 step on each iteration, "fast" with 2 steps.
            // "Fast" will get 1 more step away on each iteration.
            // If they meet, it means that "fast" returned back without reaching NULL.
            // This would mean that there is a cycle.
            var slowNode = list.Head.Next;
            var fastNode = list.Head.Next?.Next;

            do
            {
                fastNode = fastNode?.Next?.Next;
                slowNode = slowNode.Next;
            } while (fastNode != slowNode && fastNode != null);

            // A snake, or a meeting at head which means the whole list is a snail
            if (fastNode == null || fastNode == list.Head)
            {
                return fastNode;
            }

            // Mathematically, the snail end point is exactly X steps away from both head point & meeting point,
            // So leaving "fast" at the meeting point, placing "slow" at head, and moving them both towards the start point
            slowNode = list.Head;
            while (slowNode.Next != fastNode.Next)
            {
                slowNode = slowNode.Next;
                fastNode = fastNode.Next;
            }

            // Returning the start of the loop
            return slowNode.Next;
        }
    }
}