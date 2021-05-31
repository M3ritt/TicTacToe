using System;

namespace ConsoleApp1
{
    class Game
    {
		readonly Board board;
        int playerOneWins, playerTwoWins, sizeOfBoard = -1, numOfPlayers = 0;
        readonly Computer HENRY;

        public Game(){
            board = new Board();
            HENRY = new Computer();
            playerOneWins = 0;
            playerTwoWins = 0;
        }

        public void Startup()
        {
            if (sizeOfBoard < 3)
                GetSizeOfBoard();

            if (PlayComputer())
                PlayComputer(board.InitializeBoard(sizeOfBoard));
            else
                PlayGame(board.InitializeBoard(sizeOfBoard));
        }

		public void InitializeGame()
		{
            if (numOfPlayers == 1)
                PlayComputer(board.InitializeBoard(sizeOfBoard));
            else if (numOfPlayers == 2)
                PlayGame(board.InitializeBoard(sizeOfBoard));
            else Startup();
        }

        public Boolean PlayComputer()
        {
            Console.WriteLine("1 or 2 Players?");
            while(!int.TryParse(Console.ReadLine(), out numOfPlayers) || (numOfPlayers > 2))
            {
                Console.WriteLine("That was invalid. 1 or 2 Players?");
            }
            if (numOfPlayers == 1)
                return true;
            else return false;
        }

        public void GetSizeOfBoard()
        {
            Console.WriteLine("Enter a board size: ");
            while (!int.TryParse(Console.ReadLine(), out sizeOfBoard) || (sizeOfBoard < 3))
            {
                Console.WriteLine("That was invalid. Try an integer that is 3 or above.");
            }
        }

        public void PlayComputer(String[,] currBoard)
        {
            String choice;
            int player = 1;

            int flag;
            do
            {
                Console.Clear();
                Console.WriteLine("Player1:X and Player2:O");
                Console.WriteLine("Player1 wins: {0} and Henry's wins: {1}", playerOneWins, playerTwoWins);
                Console.WriteLine("\n");
                if (player % 2 == 0)
                {
                    Console.WriteLine("Henry's Turn");
                    Console.WriteLine("\n");
                    Tuple<int, int> move = HENRY.Move(currBoard);
                    board.MarkBoard(move, player, currBoard);
                }
                else
                {
                    Console.WriteLine("Player 1 Chance");
                    board.DisplayBoard(currBoard);
                    choice = Console.ReadLine();
                    board.MarkBoard(ReadMove(choice), player, currBoard);
                }
                Console.WriteLine("\n");
                flag = board.IsGameOver(currBoard);
                player++;
            } while (flag != 1 && flag != -1);
            Console.Clear();
            board.DisplayBoard(currBoard);
            if (flag == 1)
            {
                if((player % 2) != 0)
                    Console.WriteLine("Henry has won");
                else 
                    Console.WriteLine("Player 1 has won");
                if ((player % 2) + 1 == 1)
                    playerOneWins++;
                else
                    playerTwoWins++;
            }
            else
            {
                Console.WriteLine("Draw");
            }
            PlayAgain();
        }

		public void PlayGame(String[,] currBoard)
		{
            String choice;
            int player = 1;

            int flag;
            do
            {
                Console.Clear();
                Console.WriteLine("Player1:X and Player2:O");
                Console.WriteLine("Player1 wins: {0} and Player2 wins: {1}", playerOneWins, playerTwoWins);
                Console.WriteLine("\n");
                if (player % 2 == 0)
                {
                    Console.WriteLine("Player 2 Turn");
                }
                else
                {
                    Console.WriteLine("Player 1 Chance");
                }
                Console.WriteLine("\n");
                board.DisplayBoard(currBoard);
                choice = Console.ReadLine();
                board.MarkBoard(ReadMove(choice), player, currBoard);
                flag = board.IsGameOver(currBoard);
                player++;
            } while (flag != 1 && flag != -1);
            Console.Clear();
            board.DisplayBoard(currBoard);
            if (flag == 1)
            {
                Console.WriteLine("Player {0} has won", (player % 2) + 1);
                if ((player % 2) + 1 == 1)
                    playerOneWins++;
                else
                    playerTwoWins++;
            }
            else
            {
                Console.WriteLine("Draw");
            }
            PlayAgain();
        }

        public void PlayAgain()
        {
            Console.WriteLine("\n \n Would you like to play again? (Yes or No)");
            String isAgain = Console.ReadLine();
            if (isAgain.Equals("Yes"))
                InitializeGame();
            else
            {
                Console.WriteLine("Player1 wins: {0} and Player2 wins: {1}", playerOneWins, playerTwoWins);
                Console.WriteLine("Thank you for playing");
                Console.ReadLine();
            }
        }

        public Tuple<int, int> ReadMove(String num)
        {
            /*
            int pos = 1, lookingFor;
            if (!int.TryParse(num, out lookingFor))
                return Tuple.Create(-1, -1);
            for (int r = 0; r < sizeOfBoard; r++)
            {
                for (int c = 0; c < sizeOfBoard; c++)
                {
                    if (pos == lookingFor)
                        return Tuple.Create(r, c);
                    
                }
            }
            return Tuple.Create(-1, -1);
            */
            if (sizeOfBoard == 3)
            {
                if (num == "1")
                    return Tuple.Create(0, 0);
                else if (num == "2")
                    return Tuple.Create(0, 1);
                else if (num == "3")
                    return Tuple.Create(0, 2);
                else if (num == "4")
                    return Tuple.Create(1, 0);
                else if (num == "5")
                    return Tuple.Create(1, 1);
                else if (num == "6")
                    return Tuple.Create(1, 2);
                else if (num == "7")
                    return Tuple.Create(2, 0);
                else if (num == "8")
                    return Tuple.Create(2, 1);
                else
                    return Tuple.Create(2, 2);
            }
            else
            {
                if (num == "1")
                    return Tuple.Create(0, 0);
                else if (num == "2")
                    return Tuple.Create(0, 1);
                else if (num == "3")
                    return Tuple.Create(0, 2);
                else if (num == "4")
                    return Tuple.Create(0, 3);
                else if (num == "5")
                    return Tuple.Create(1, 0);
                else if (num == "6")
                    return Tuple.Create(1, 1);
                else if (num == "7")
                    return Tuple.Create(1, 2);
                else if (num == "8")
                    return Tuple.Create(1, 3);
                else if (num == "9")
                    return Tuple.Create(2, 0);
                else if (num == "10")
                    return Tuple.Create(2, 1);
                else if (num == "11")
                    return Tuple.Create(2, 2);
                else if (num == "12")
                    return Tuple.Create(2, 3);
                else if (num == "13")
                    return Tuple.Create(3, 0);
                else if (num == "14")
                    return Tuple.Create(3, 1);
                else if (num == "15")
                    return Tuple.Create(3, 2);
                else return Tuple.Create(3, 3);
            }
            
        }
    }
}
