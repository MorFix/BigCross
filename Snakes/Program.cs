using System;

namespace Snakes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LinkedList<int> randomList = CreateRandomList();
            Print(randomList);
        }

        private static LinkedList<int> CreateRandomList()
        {
            Random random = new Random();
            if (IsChanceMet(0.5, random))
            {
                return CreateRandomSnake();
            }

            return CreateRandomCycle();
        }

        private static LinkedList<int> CreateRandomSnake()
        {
            Random random = new Random();
            LinkedList<int> list = new LinkedList<int>();

            while (!IsChanceMet(0.01, random))
            {
                list.AddLast(random.Next());
            }

            return list;
        }

        private static LinkedList<int> CreateRandomCycle()
        {
            Random random = new Random();
            LinkedList<int> list = new LinkedList<int>();
            LinkedListNode<int> cycleStartNode = (LinkedListNode<int>)null;
            LinkedListNode<int> lastNode = (LinkedListNode<int>)null;

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
            int digitsAfterPoint = chance.ToString().Split('.')[1]?.Length ?? 0;
            double maxValue = Math.Pow(10, digitsAfterPoint);
            int rand = random.Next((int)maxValue);

            return rand < (chance * maxValue);
        }

        private static void Print(LinkedList<int> list)
        {
            LinkedListNode<int> startOfSnailLoop = SnackorSnail(list);
            if (startOfSnailLoop == null)
            {
                LinkedListNode<int> node = list.Head;
                int snakeLength = 0;

                Console.Write("snake: ");

                while (node != null)
                {
                    snakeLength++;
                    Console.Write(node.Data + "→");
                    node = node.Next;
                }

                Console.WriteLine("null");

                Console.WriteLine("The length of the snake is: " + snakeLength);
            }
            else
            {
                int meetCounter = 0;
                LinkedListNode<int> node = list.Head;
                int snailLength = 0;
                int loopLength = 0;

                Console.Write("snail: ");

                while (meetCounter != 2)
                {
                    if (node == startOfSnailLoop)
                    {
                        meetCounter++;
                        if (meetCounter == 2)
                        {
                            continue;
                        }
                        Console.Write(" ↱ " + node.Data + " → ");
                    }

                    else if ((node.Next == startOfSnailLoop) && (meetCounter > 0))
                    {
                        Console.WriteLine(node.Data + " ↲");
                    }
                    else
                    {
                        if (node.Next == startOfSnailLoop)
                        {
                            Console.Write(node.Data);
                        }
                        else
                        {
                            Console.Write(node.Data + " → ");
                        }
                    }


                    if (meetCounter > 0)
                    {
                        loopLength++;
                    }

                    snailLength++;

                    node = node.Next;
                }

                Console.WriteLine("The length of the snail is: " + snailLength + " and the length of the loop is: " + loopLength);
            }
        }

        

        private static LinkedListNode<int> SnackorSnail(LinkedList<int> list)
        {
            // Moving two pointers, "slow" with 1 step on each iteration, "fast" with 2 steps.
            // "Fast" will get 1 more step away on each iteration.
            // If they meet, it means that "fast" got N steps away from slow, but it didn't reach NULL.
            // This would mean that there is a cycle.
            LinkedListNode<int> slowNode = list.Head;
            LinkedListNode<int> fastNode = list.Head;

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
