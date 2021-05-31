using System;
using System.Linq;
using System.Threading;

namespace ConsoleApp1
{
	class Board
	{
        public String[,] InitializeBoard(int size)
        {
            /**
             * Creates a game board 
             * 
             * @Param: 
             *  - int size : the size of the board, will always be a square
             *  
             *  @Return:
             *  - String[,] : The created board.
             */
            String[,] futureBoard = new string[size, size];
            int pos = 1;
            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    futureBoard[r, c] = pos.ToString();
                    pos++;
                }
            }
            return futureBoard;
        }

        public void DisplayBoard(String[,] currBoard)
        {
            /**
             * Displays the current board
             * 
             * @param: 
             *  - String[,] currBoard : The current board
             */
            int count = 0;
            DisplayStartEnd(currBoard.GetLength(1));
               for (int r = 0; r < currBoard.GetLength(0); r++)
               {
                   for (int c = 0; c < currBoard.GetLength(1); c++)
                   {
                    count = DisplayMoves(r, c, count, currBoard);
                   }
                count = 0;
                if (r != currBoard.GetLength(0) - 1)
                {
                    DisplayLines(currBoard.GetLength(1), count);
                }
                
            }
            DisplayStartEnd(currBoard.GetLength(1));
        }

        public int DisplayMoves(int r, int c, int count, String[,] currBoard)
        {
            /**
             * Helper method for DisplayBoard. This is used to print all of the current moves that have been done. 
             * 
             * @Param: 
             *  int r : current row number
             *  int c : current column number
             *  int count : the count of middle columns
             *  String[,] currBoard : the current board
             *  
             * @Return:
             *  int : number of middle columns  
             */
            if (c == 0)
            {
                if (currBoard[r, c].Length < 2)
                    Console.Write("  {0}  ", currBoard[r, c]);
                else Console.Write("  {0} ", currBoard[r, c]);
            }
            else if (c == currBoard.GetLength(1) - 1)
            {
                Console.WriteLine("  {0}", currBoard[r, c]);
            }
            else
            {
                if (count == 0)
                {
                    if (currBoard[r, c].Length < 2)
                        Console.Write("|  {0}  |", currBoard[r, c]);
                    else Console.Write("|  {0} |", currBoard[r, c]);
                }
                else
                {
                    if (currBoard[r, c].Length < 2)
                        Console.Write("  {0}  |", currBoard[r, c]);
                    else Console.Write("  {0} |", currBoard[r, c]);
                }
                count++;
            }
            return count;
        }

        public void DisplayLines(int numOfColumns, int count)
        {
            /**
             * Helper for DisplayBoard. Prints out the divisions between coordinates. 
             * 
             * @Param: 
             *  int numOfColumns : the number of columns
             *  int count : the count of middle columns
             */
            String line1 = "",  line2 = "";
            for (int i = 0; i < numOfColumns; i++)
            {
                if (i == 0)
                {
                    line1 += "_____";
                    line2 += "     ";
                }
                else if (i == numOfColumns-1)
                {
                    line1 += "_____";
                    line2 += "     ";
                }
                else
                {
                    if (count == 0)
                    {
                        line1 += "|_____|";
                        line2 += "|     |";
                    }
                    else
                    {
                        line1 += "_____|";
                        line2 += "     |";
                    }
                    count++;
                }
            }
            Console.WriteLine(line1);
            Console.WriteLine(line2);
        }

        public void DisplayStartEnd(int numOfColumns)
        {
            /*
             * Helper for DisplayBoard. Displays the first and last row which is just | lines. 
             * 
             * @Param: 
             *  int numOfColumns : the number of columns
             */
            String lines = "";
            for (int i = 0; i < numOfColumns; i++)
            {
                if (i != numOfColumns - 1)
                    lines += "     |";
            }
            Console.WriteLine(lines);
        }


        public bool MarkBoard(Tuple<int, int> move, int player, String[,] currBoard)
        {
            /**
            * Marks the board based on the given input and player. 
            * 
            * @Param: 
            *  - Tuple<int, int> move : the location on the 2D array (x,y)
            *  - int player : the int of the player number to decide which marking
            *  - String[,] currBoard : The current board.
            */
            if (move.Item1 == -1)
            {
                Console.WriteLine("That is an invalid move. Try again");
                Console.WriteLine("\n");
                Console.WriteLine("Please wait 2 second(s), board is loading again.....");
                Thread.Sleep(2000);
                return false;
            }
                
            string XorO;
            if (player % 2 == 0)
                XorO = "X";
            else
                XorO = "O";

            int x = move.Item1;
            int y = move.Item2;
            
            if (currBoard[x, y] != "X" && currBoard[x, y] != "O")
            {
                currBoard[x, y] = XorO;
                return true;
            }
            else
            {
                Console.WriteLine("That place is already taken. Try again");
                Console.WriteLine("\n");
                Console.WriteLine("Please wait 2 second(s), board is loading again.....");
                Thread.Sleep(2000);
                return false;
            }
        }


        public int IsGameOver(String[,] currBoard)
        {
            /**
             * Checks if a game is over
             * 
             * @Param: 
             *  - String[,] currBoard : The current board
             *  
             * @Return:
             *  - int : 1 is returned if game is over, -1 if it is a draw, 0 otherwise
             */
            String[] array = new string[currBoard.GetLength(0)];
            #region Horizontal Win condition
            for (int r = 0; r < currBoard.GetLength(0); r++)
            {
                array = GetRow(currBoard, r);
                if (isFull(array))
                    return 1;
            }
            #endregion

            #region Vertical Win condition
            for (int c = 0; c < currBoard.GetLength(1); c++)
            {
                array = GetColumn(currBoard, c);
                if (isFull(array))
                    return 1;
            }
            #endregion

            int count = 0;
            #region Principle Diagonal Win Condition
            for (int r = 0; r < currBoard.GetLength(0); r++)
            {
                for (int c = 0; c < currBoard.GetLength(1); c++)
                {
                    if (r == c)
                    {
                        array[count] = currBoard[r, c];
                        count++;
                    }
                }
            }
            if (isFull(array))
                return 1;
            #endregion

            
            count = 0;
            #region Secondary Diagonal Win Condition
            for (int r = 0; r < currBoard.GetLength(0); r++)
            {
                for (int c = 0; c < currBoard.GetLength(1); c++)
                {
                    if ((r + c) == (currBoard.GetLength(0) - 1))
                    {
                        array[count] = currBoard[r, c];
                        count++;
                    }
                }
            }
            if (isFull(array))
                return 1;
			#endregion

			#region Draw
			int moves = 0;
            for (int r = 0; r < currBoard.GetLength(0); r++)
            {
                for (int c = 0; c < currBoard.GetLength(0); c++)
                {
                    if (currBoard[r, c] == "X" || currBoard[r, c] == "O")
                        moves++;
                }
            }
            if (moves == Math.Pow(currBoard.GetLength(0), 2))
                return -1;
            #endregion
            return 0;
        }


        public bool isFull(String[] array)
        {
        /**
         * Checks if an array is full. Used for checking if the game is over
         * 
         * @Param: 
         *  - String[] array : The array to check
         *  
         * @Return: 
         *  - True if the column is full, false otherwise
         */
            var groups = array.GroupBy(v => v);
            foreach (var group in groups)
            {
                if (group.Count() == array.Length)
                    return true;
            }
            return false;
                
        }

   
        public String[] GetColumn(String[,] array, int colNumber)
        {
        /**
		 * Gets the Column from a given String[,]
		 * 
		 * @Param: 
		 *  - String[,] array : 2D array
		 *  - int colNumber : The column number that is desired
		 *  
		 *  @Return:
		 *      - String[] or the specific column
		 */
            return Enumerable.Range(0, array.GetLength(0)).Select(x => array[x, colNumber]).ToArray();
        }


        public String[] GetRow(String[,] array, int rowNumber)
        {
         /**
		 * Gets the row from a given String[,]
		 * 
		 * @Param: 
		 *  - String[,] array : 2D array
		 *  - int rowNumber : The row number that is desired
		 *  
		 *  @Return:
		 *      - String[] or the specific row
		 */
            return Enumerable.Range(0, array.GetLength(1)).Select(x => array[rowNumber, x]).ToArray();
        }
    }
}
