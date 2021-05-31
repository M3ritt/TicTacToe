using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
	class Computer
	{
		Board board;
		String[,] currBoard;

		
		public Tuple<int, int> GoingToLose(String[,] curr)
		{
		/**
		 * Checks if the computer player is going to lose and blocks the potential loss.
		 * 
		 * @Return: 
		 *	- The coordinates of the 2D array of where to move
		 */
			int tempNum;
			#region Row loss
			for (int r = 0; r < curr.GetLength(0); r++)
			{
				tempNum = GoingToLoseHelper(board.GetRow(curr, r));
				if (tempNum != -1)
					return Tuple.Create(r, tempNum);
			}
			#endregion

			tempNum = -1;
			#region Column loss
			for (int c = 0; c < curr.GetLength(1); c++)
			{
				tempNum = GoingToLoseHelper(board.GetColumn(curr, c));
				if (tempNum != -1)
					return Tuple.Create(tempNum, c);
			}
			#endregion

			tempNum = -1;
			String[] temp = new string[curr.GetLength(0)];
			int count = 0;
			#region Principle Diagonal loss
			for (int r = 0; r < curr.GetLength(0); r++)
			{
				for (int c = 0; c < curr.GetLength(1); c++)
				{
					if (r == c)
					{
						temp[count] = curr[r, c];
						count++;
					}
				}
			}
			tempNum = GoingToLoseHelper(temp);
			if (tempNum != -1)
				return Tuple.Create(tempNum, tempNum);
			#endregion

			
			tempNum = -1;
			count = 0;
			#region Secondary Diagonal loss
			for (int r = 0; r < curr.GetLength(0); r++)
			{
				for (int c = 0; c < curr.GetLength(1); c++)
				{
					if ((r + c) == (curr.GetLength(0) - 1))
					{
						temp[count] = curr[r, c];
						count++;
					}
				}
			}
			tempNum = GoingToLoseHelper(temp);
			if (tempNum != -1)
				return Tuple.Create(tempNum, tempNum);
			#endregion

			return Tuple.Create(-1, -1);
		}


		public int GoingToLoseHelper(String[] array)
		{
		/**
		* Helper method for GoingToLose. This method checks to see if an opponent is one away from winning
		* 
		* @Param: 
		*	- String[] array : A list of the coordinates to look at
		*	
		* @Return:
		*	- int : The spot in the given array where Henry should move, or -1 if not going to lose. 
		*/
			int[] positions = new int[array.Length];
			int pos = 0;
			for (int i = 0; i < array.Length-2; i++)
			{
				if (array[i] == array[i + 1])
				{
					positions[i] = i;
					positions[i + 1] = i + 1;
					pos++;
					pos++;
				}
				else if (array[i] == array[i + 2])
				{
					positions[i] = i;
					positions[i + 2] = i + 2;
					pos++;
					pos++;
				}
			}

			if (pos != array.Length - 1)
				return -1;
			else
			{
				int count = 0;
				for (int i = 0; i < positions.Length; i++)
				{
					if (positions[i] != count && array[i] != "X")
						return i;
					count++;
				}
				return -1;
			}
		}


		public Tuple<int, int> LookAhead(String[,] array)
		{
		/**
		 * Looks ahead to the next turn  
		 * 
		 * @Param: 
		 *	- String[,] array : array to look at
		 *	
		 *	@Return: 
		 *	- Tuple<int, int> : Possible move to block future attack, -1 otherwise. 
		 */
			int x = -1, y = -1;
			String[,] saveState = CreateCopy();
			
			for (int r = 0; r < saveState.GetLength(0); r++) 
			{
				for (int c = 0; c < saveState.GetLength(1); c++)
				{
					if (saveState[r, c] != "X" && saveState[r, c] != "O")
					{
						saveState[r, c] = "O";
						Tuple<int, int> potentialBlock = GoingToLose(saveState);
						if (potentialBlock.Item1 != -1)
						{
							saveState = currBoard;
							return GoingToLose(saveState);
						}
						saveState = array;
					}
				}
			}
			return Tuple.Create(x, y);
		}

		public String[,] CreateCopy()
		{
			/**
			 * Creates a copy of the current board
			 * 
			 * @Return:
			 *	- String[,] : 2D copy of the current board
			 */
			String[,] copy = new string[currBoard.GetLength(0), currBoard.GetLength(1)];
			for (int r = 0; r < currBoard.GetLength(0); r++)
			{
				for (int c = 0; c < currBoard.GetLength(1); c++)
				{
					copy[r, c] = currBoard[r, c];
				}
			}
			return copy;
		}


		public Tuple<int, int> OnTheAttack()
		{
			/**
			 * Figuring out the best offensive move. 
			 *		Stratgey:
			 *			- Grabs the corners as fast and safe as possible, then go for center to auto win. 
			 * 
			 * @Return:
			 *	Tuple<int, int> : The coordinates of where to move. 
			 */
			if (FindNumBenficialCorners() >= 1)
			{
				return FindBestCornerMove();
			}
		
			if (currBoard[1, 1] == "5")
				return Tuple.Create(1,1);
			else if (currBoard[0, 0] == "1")
				return Tuple.Create(0, 0);
			else if (currBoard[0, 2] == "3")
				return Tuple.Create(0, 2);
			else if (currBoard[2, 0] == "7")
				return Tuple.Create(2, 0);
			else if (currBoard[2, 2] == "9")
				return Tuple.Create(2, 2);
			else if (currBoard[0, 1] == "2")
				return Tuple.Create(0, 1);
			else if (currBoard[1, 0] == "4")
				return Tuple.Create(1, 0);
			else if (currBoard[1, 2] == "6")
				return Tuple.Create(1, 2);
			else
				return Tuple.Create(2, 1);
		}

		public int FindNumBenficialCorners()
		{
			int openCorners = 0;
			if (currBoard[0, 0] != "O")
				openCorners++;
			if (currBoard[0, currBoard.GetLength(0)-1] != "O")
				openCorners++;
			if (currBoard[currBoard.GetLength(0)-1, 0] != "O")
				openCorners++;
			if (currBoard[currBoard.GetLength(0) - 1, currBoard.GetLength(0) - 1] != "O")
				openCorners++;

			return openCorners;
		}

		public Tuple<int, int> FindBestCornerMove()
		{
			//Returns top right corner if [0,0] is Henry's and top right is available. 
			if (currBoard[0, 0] == "X" && (currBoard[0, currBoard.GetLength(0) - 1] != "O" && currBoard[0, currBoard.GetLength(0) - 1] != "X"))
				return Tuple.Create(0, currBoard.GetLength(0));
			//Returns top left corner if top right is Henry's and top left is available. 
			else if (currBoard[0, currBoard.GetLength(0) - 1] == "X" && (currBoard[0, 0] != "O" && currBoard[0, 0] != "X"))
				return Tuple.Create(0, 0);
			
			//Returns bottom right corner if bottom left is Henry's and bottom right is open.
			else if (currBoard[currBoard.GetLength(0) - 1, 0] == "X" && (currBoard[currBoard.GetLength(0) - 1, currBoard.GetLength(0) - 1] != "O"
				&& currBoard[currBoard.GetLength(0) - 1, currBoard.GetLength(0) - 1] != "X"))
				return Tuple.Create(currBoard.GetLength(0) - 1, currBoard.GetLength(0) - 1);
			//Returns bottom left corner if bottom right is Henry's and bottom left is open. 
			else return Tuple.Create(0, currBoard.GetLength(0) - 1);
		}

		

		public Tuple<int, int> PointSystem()
		{
			/**
			 * The point system hieracrhy that Henry uses to make a move. 
			 * 
			 * @Return:
			 *	Tuple<int, int> : The coordinates of where to move
			 */
			String[,] copiedBoard = CreateCopy();
			Tuple<int, int> potentialMove1 = GoingToLose(copiedBoard);
			Tuple<int, int> potentialMove2 = LookAhead(copiedBoard);
			if (potentialMove1.Item1 != -1)
				return potentialMove1;
			else if (potentialMove2.Item1 != -1)
				return potentialMove2;
			else return OnTheAttack();
		}

		public Tuple<int, int> Move(String[,] currBoard)
		{
		/**
		 * Method used for calling Henry to make a move. 
		 * 
		 * @Param:
		 *	- String[,] currBoard : The current board. 
		 *	
		 *	@Return: 
		 *	- Tuple<int, int> : The Coordinates of what Henry deemed best. 
		 */
			board = new Board();
			this.currBoard = currBoard;
			return PointSystem();
			
		}
	}
}
