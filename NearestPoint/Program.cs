// Tal Gavriel, ID: 209209808
// Mor Cohen, ID: 315825356

using System;
using System.Collections.Generic;

namespace NearestPoint
{
    class Program
    {
        private static readonly BinarySearchTree<Point> BinarySearchTree = new BinarySearchTree<Point>();
        private static readonly Random Random = new Random();

        public static void Main(string[] args)
        {
            BuildTree();

            PrintTree(BinarySearchTree.Root);

            Console.Write("X = ");
            var x = double.Parse(Console.ReadLine());
            Console.WriteLine();


            var nearestNode = NearestRightPoint(BinarySearchTree.Root, x) ?? new BinaryNode<Point>(new Point(0, 0));
            Console.WriteLine($"Nearest Point: {nearestNode.Value}");
            Console.ReadKey();
        }

        private static void BuildTree()
        {
            for (int i = 0; i < 10; i++)
            {
                Point newPoint = GetRandomPoint();

                // The insert methods preserves the Binary Search Tree order, so the search would be O(logN)
                BinarySearchTree.Insert(newPoint);
            }
        }

        private static void PrintTree(BinaryNode<Point> node)
        {
            if (node == null)
            {
                return;
            }

            Console.WriteLine(node.Value);

            PrintTree(node.RightChild);
            PrintTree(node.LeftChild);
        }

        private static Point GetRandomPoint()
        {
            var x = Random.NextDouble() * 100;
            var y = Random.NextDouble() * 100;

            return new Point(x, y);
        }

        private static BinaryNode<Point> NearestRightPoint(BinaryNode<Point> node, double x, BinaryNode<Point> currentClosestNode = null)
        {
            if (node == null)
            {
                return currentClosestNode;
            }

            var compare = node.Value.CompareTo(new Point(x, 0));

            // Checking if we got a new minimal difference
            if (compare > 0 && (currentClosestNode == null || node.Value.X < currentClosestNode.Value.X))
            {
                currentClosestNode = node;
            }

            // If x is less (or equal, since we ignore this case) than current X move right, else - left  
            return compare <= 0
                ? NearestRightPoint(node.RightChild, x, currentClosestNode)
                : NearestRightPoint(node.LeftChild, x, currentClosestNode);
        }
    }
}
