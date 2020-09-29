// Tal Gavriel, ID: 209209808
// Mor Cohen, ID: 315825356

using System;
using System.Collections.Generic;

namespace NearestPoint
{
    class Program
    {
        private static readonly BinarySearchTree<Point> BinarySearchTree = new BinarySearchTree<Point>();
        private static List<Point> _listOfPoints = new List<Point>();
        private static readonly Random Random = new Random();
        private static readonly object SyncLock = new object();
        public static void Main(string[] args)
        {
            BuildTree();
            _listOfPoints = BinarySearchTree.GetNodesInOrder(BinarySearchTree.Root, new List<Point>());
            printPoints();
            Console.ReadLine();
        }

        public static void BuildTree()
        {
            for (int i = 0; i < 1000; i++)
            {
                Point newPoint = GetRandomPoint();
                BinarySearchTree.Insert(newPoint);
            }
        }

        public static void printPoints()
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    Point newPoint = new Point(i,j);

                    if (_listOfPoints.Contains(newPoint))
                    {
                        Console.Write("O");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }
        }

       

        public static Point GetRandomPoint()
        {
            lock (SyncLock)
            {
                 int x = Random.Next(0, 100);
                 int y = Random.Next(0, 100);
                 return new Point(x, y);

            }
        }
    }
}
