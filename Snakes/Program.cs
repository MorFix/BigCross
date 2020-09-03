using System;

namespace Snakes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var randomList = CreateRandomList();
            Print(randomList);
        }

        private static LinkedList<int> CreateRandomList()
        {
            var random = new Random();
            if (IsChanceMet(0.5, random))
            {
                return CreateRandomSnake();
            }

            return CreateRandomCycle();
        }

        private static LinkedList<int> CreateRandomSnake()
        {
            var random = new Random();
            var list = new LinkedList<int>();

            while (!IsChanceMet(0.01, random))
            {
                list.AddLast(random.Next());        
            }

            return list;
        }

        private static LinkedList<int> CreateRandomCycle()
        {
            var random = new Random();
            var list = new LinkedList<int>();
            var cycleStartNode = (LinkedListNode<int>) null;
            var lastNode = (LinkedListNode<int>) null;

            while (!IsChanceMet(0.02, random) || cycleStartNode == null)
            {
                lastNode = list.AddLast(random.Next());

                if (cycleStartNode == null && IsChanceMet(0.015, random))
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
        /// <param name="random">The random object to work with</param>
        /// <returns>boolean</returns>
        private static bool IsChanceMet(double chance, Random random)
        {
            var digitsAfterPoint = chance.ToString().Split('.')[1]?.Length ?? 0;
            var maxValue = Math.Pow(10, digitsAfterPoint);
            var rand = random.Next((int) maxValue);

            return rand < (chance * maxValue);
        }

        private static void Print(LinkedList<int> list)
        {
            // TODO: Print
        }

        private static LinkedListNode<int> SnackorSnail(LinkedList<int> list)
        {
            // Moving two pointers, "slow" with 1 step on each iteration, "fast" with 2 steps.
            // "Fast" will get 1 more step away on each iteration.
            // If they meet, it means that "fast" got N steps away from slow, but it didn't reach NULL.
            // This would mean that there is a cycle.
            var slowNode = list.Head;
            var fastNode = list.Head;

            do
            {
                fastNode = fastNode?.Next?.Next;
                slowNode = slowNode.Next;
            } while (fastNode != slowNode && fastNode != null);

            // It's a snake, since they didn't meet
            if (fastNode == null)
            {
                return null;
            }

            // Finding the node that have 2 "Next"s set to
            slowNode = list.Head;
            while(slowNode.Next != fastNode.Next)
            {
                slowNode = slowNode.Next;
                fastNode = fastNode.Next;
            }

            // Returning the start of the loop
            return slowNode.Next;
        }
    }
}
