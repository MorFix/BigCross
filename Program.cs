using System;

namespace BigCross
{
    class Program
    {
        private const int CONSTANT_MATRIX_SIZE = 15;
        private const int RANDOM_MATRIX_SIZE = 50;

        static void Main(string[] args)
        {
            var constantMatrix = new int[CONSTANT_MATRIX_SIZE, CONSTANT_MATRIX_SIZE]
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

            var randomMatrix = GetRandomMatrix();

            Console.WriteLine("Constant Matrix:");
            PrintMatrix(constantMatrix);
            
            var maxCrossConstant1 = GetMaxCrossN3(constantMatrix, out var constantBiggestCrossRow1, out var constantBiggestCrossCol1);
            Console.WriteLine($"Constant matrix - O(N^3): => Biggest cross size: {maxCrossConstant1} in [{constantBiggestCrossRow1},{constantBiggestCrossCol1}]");

            var maxCrossConstant2 = GetMaxCrossN2(constantMatrix, out var constantBiggestCrossRow2, out var constantBiggestCrossCol2);
            Console.WriteLine($"Constant matrix - O(N^2): => Biggest cross size: {maxCrossConstant2} in [{constantBiggestCrossRow2},{constantBiggestCrossCol2}]");

            Console.WriteLine("");

            Console.WriteLine("Random Matrix:");
            PrintMatrix(randomMatrix);

            var maxCrossRandom1 = GetMaxCrossN3(randomMatrix, out var randomBiggestCrossRow1, out var randomBiggestCrossCol1);
            Console.WriteLine($"Random matrix - O(N^3): => Biggest cross size: {maxCrossRandom1} in [{randomBiggestCrossRow1},{randomBiggestCrossCol1}]");

            var maxCrossRandom2 = GetMaxCrossN2(randomMatrix, out var randomBiggestCrossRow2, out var randomBiggestCrossCol2);
            Console.WriteLine($"Random matrix - O(N^2): => Biggest cross size: {maxCrossRandom2} in [{randomBiggestCrossRow2},{randomBiggestCrossCol2}]");
        }

        // Get the biggest cross using O(N^3) complexity
        private static int GetMaxCrossN3(int[,] matrix, out int biggestCrossRow, out int biggestCrossCol)
        {
            var biggestCrossSize = 0;
            
            biggestCrossRow = -1;
            biggestCrossCol = -1;

            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                for (var col = 0; col < matrix.GetLength(1); col++)
                {
                    // A cross might be starting here
                    if (matrix[row, col] == 1)
                    {
                        var crossSize = 0;
                        
                        while (IsCross(matrix, row, col, crossSize))
                        {
                            crossSize++;
                        }

                        if (crossSize > biggestCrossSize)
                        {
                            biggestCrossSize = crossSize;
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
            return row - size >= 0 && // Ensure left boundry
                col - size >= 0 && // Ensure top boundry
                row + size < matrix.GetLength(0) && // Ensure right boundry
                col + size < matrix.GetLength(1) && // Ensure bottom boundry
                matrix[row - size, col] == 1 && // Check if cross continues to the left
                matrix[row, col - size] == 1 && // Check if cross continues to the top
                matrix[row + size, col] == 1 && // Check if cross continues to the right
                matrix[row, col + size] == 1; // Check if cross continues to the bottom

        }

        // Get the biggest cross using O(N^2) complexity
        private static int GetMaxCrossN2(int[,] matrix, out int biggestCrossRow, out int biggestCrossCol)
        {
            var biggestCrossSize = 0;
            biggestCrossRow = -1;
            biggestCrossCol = -1;

            // TODO: DO THIS WITH O(N^2)

            return biggestCrossSize;
        }

        private static int[,] GetRandomMatrix()
        {
            var matrix = new int[RANDOM_MATRIX_SIZE, RANDOM_MATRIX_SIZE];
            var random = new Random();

            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = random.Next(0, 2);
                }
            }

            return matrix;
        }

        private static void PrintMatrix(int[,] matrix)
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }

                Console.WriteLine("");
            }
        }
    }
}
