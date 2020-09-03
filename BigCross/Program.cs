//Tal Gavriel, ID:209209808
//Mor Cohen, ID:315825356



using System;

namespace BigCross
{
    class Program
    {
        private const int ConstantMatrixSize = 15;
        private const int RandomMatrixSize = 50;

        static void Main(string[] args)
        {
            int[,] constantMatrix = new int[ConstantMatrixSize, ConstantMatrixSize]
            {
                { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0 },
                { 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0 },
                { 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0 },
                { 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0 },
                { 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0 },
                { 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1 },
                { 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0 },
                { 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0 },
                { 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0 },
                { 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0 },
                { 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0 },
                { 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0 },
            };

            int[,] randomMatrix = GetRandomMatrix();

            Console.WriteLine("Constant Matrix:");
            PrintMatrix(constantMatrix);
            
            int maxCrossConstant1 = BigCross1(constantMatrix, out int constantBiggestCrossRow1, out int constantBiggestCrossCol1);
            Console.WriteLine($"Constant matrix - O(N^3): => Biggest cross size: {maxCrossConstant1} in [{constantBiggestCrossRow1},{constantBiggestCrossCol1}]");

            int maxCrossConstant2 = BigCross2(constantMatrix, out int constantBiggestCrossRow2, out int constantBiggestCrossCol2);
            Console.WriteLine($"Constant matrix - O(N^2): => Biggest cross size: {maxCrossConstant2} in [{constantBiggestCrossRow2},{constantBiggestCrossCol2}]");

            Console.WriteLine("");

            Console.WriteLine("Random Matrix:");
            PrintMatrix(randomMatrix);

            int maxCrossRandom1 = BigCross1(randomMatrix, out int randomBiggestCrossRow1, out int randomBiggestCrossCol1);
            Console.WriteLine($"Random matrix - O(N^3): => Biggest cross size: {maxCrossRandom1} in [{randomBiggestCrossRow1},{randomBiggestCrossCol1}]");

            int maxCrossRandom2 = BigCross2(randomMatrix, out int randomBiggestCrossRow2, out int randomBiggestCrossCol2);
            Console.WriteLine($"Random matrix - O(N^2): => Biggest cross size: {maxCrossRandom2} in [{randomBiggestCrossRow2},{randomBiggestCrossCol2}]");
        }

        // Get the biggest cross using O(N^3) complexity
        private static int BigCross1(int[,] matrix, out int biggestCrossRow, out int biggestCrossCol)
        {
            int biggestCrossSize = 0;
            
            biggestCrossRow = -1;
            biggestCrossCol = -1;

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    // A cross might be starting here
                    if (matrix[row, col] == 1)
                    {
                        int crossSize = 0;
                        
                        while (IsCross(matrix, row, col, crossSize))
                        {
                            crossSize++;
                        }

                        if (crossSize-1 > biggestCrossSize)
                        {
                            biggestCrossSize = crossSize-1;
                            biggestCrossRow = row;
                            biggestCrossCol = col;
                        }
                    }
                }
            }

            return biggestCrossSize;
        }

        private static bool IsCross(int[,] matrix, int row, int col, int size)
        {
            return row - size >= 0 && // Ensure left boundary
                col - size >= 0 && // Ensure top boundary
                row + size < matrix.GetLength(0) && // Ensure right boundary
                col + size < matrix.GetLength(1) && // Ensure bottom boundary
                matrix[row - size, col] == 1 && // Check if cross continues to the left
                matrix[row, col - size] == 1 && // Check if cross continues to the top
                matrix[row + size, col] == 1 && // Check if cross continues to the right
                matrix[row, col + size] == 1; // Check if cross continues to the bottom

        }

        // Get the biggest cross using O(N^2) complexity
        private static int BigCross2(int[,] matrix, out int biggestCrossRow, out int biggestCrossCol)
        {
            int[,] leftCounters = new int[matrix.GetLength(0), matrix.GetLength(0)];
            int[,] rightCounters = new int[matrix.GetLength(0), matrix.GetLength(0)];
            int[,] topCounters = new int[matrix.GetLength(0), matrix.GetLength(0)];
            int[,] bottomCounters = new int[matrix.GetLength(0), matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                topCounters[0,i] = matrix[0,i];
                bottomCounters[matrix.GetLength(0) - 1,i] = matrix[matrix.GetLength(0) - 1,i];
                leftCounters[i,0] = matrix[i,0];
                rightCounters[i,matrix.GetLength(0) - 1] = matrix[i,matrix.GetLength(0) - 1];
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 1; j < matrix.GetLength(0); j++)
                {
                    // Calculate the left counter, left to right 
                    if (matrix[i,j]==1)
                    {
                        leftCounters[i,j] = leftCounters[i,j - 1] + 1;
                    }
                    else
                    {
                        leftCounters[i,j] = 0;
                    }

                    // Calculate the top counters, top to bottom 
                    if (matrix[j,i]==1)
                    {
                        topCounters[j,i] = topCounters[j - 1,i] + 1;
                    }
                    else
                    {
                        topCounters[j,i] = 0;
                    }

                    // Calculate the bottom counters, bottom to top 
                    if (matrix[matrix.GetLength(0) - 1 - j,i]==1)
                    {
                        bottomCounters[matrix.GetLength(0) - 1 - j,i] = bottomCounters[matrix.GetLength(0) - j,i] + 1;
                    }
                    else
                    {
                        bottomCounters[matrix.GetLength(0) - 1 - j,i] = 0;
                    }

                    // Calculate the right counters, right to left
                    if (matrix[i,matrix.GetLength(0) - 1 - j]==1)
                    {
                        rightCounters[i,matrix.GetLength(0) - 1 - j] = rightCounters[i,matrix.GetLength(0) - j] + 1;
                    }
                    else
                    {
                        rightCounters[i,matrix.GetLength(0) - 1 - j] = 0;
                    }
                }
            }

            int biggestCrossSize = 0;
            biggestCrossRow = -1;
            biggestCrossCol = -1;

            // Searching for the max cross length 
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    // Finding the current symetric cross length
                    int crossLength = Math.Min(Math.Min(topCounters[i,j], bottomCounters[i,j]), Math.Min(leftCounters[i,j], rightCounters[i,j]));

                    if (crossLength - 1 > biggestCrossSize)
                    {
                        biggestCrossSize = crossLength - 1;
                        biggestCrossRow = i;
                        biggestCrossCol = j;
                    }
                }
            }

            return biggestCrossSize;
        }

        private static int[,] GetRandomMatrix()
        {
            int[,] matrix = new int[RandomMatrixSize, RandomMatrixSize];
            Random random = new Random();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = random.Next(0, 2);
                }
            }

            return matrix;
        }

        private static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }

                Console.WriteLine("");
            }
        }
    }
}
