using System;
using System.Threading;

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

            IsPlayingComputer();
            InitializeGame();
        }

		public void InitializeGame()
		{
            if (numOfPlayers == 1)
                PlayComputer(board.InitializeBoard(sizeOfBoard), 1);
            else if (numOfPlayers == 2)
                PlayGame(board.InitializeBoard(sizeOfBoard), 1);
            else Startup();
        }

        public void IsPlayingComputer()
        {
            Console.WriteLine("1 or 2 Players?");
            while(!int.TryParse(Console.ReadLine(), out numOfPlayers) || (numOfPlayers > 2))
            {
                Console.WriteLine("That was invalid. 1 or 2 Players?");
            }
        }

        public void GetSizeOfBoard()
        {
            Console.WriteLine("Enter a board size: ");
            while (!int.TryParse(Console.ReadLine(), out sizeOfBoard) || (sizeOfBoard < 3))
            {
                Console.WriteLine("That was invalid. Try an integer that is 3 or above.");
            }
        }

        public void PlayComputer(String[,] currBoard, int player)
        {
            String choice;

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
                    Console.WriteLine("Player 1 Turn");
                    board.DisplayBoard(currBoard);
                    choice = Console.ReadLine();
                    if (!board.MarkBoard(ReadMove(choice), player, currBoard))
                        PlayComputer(currBoard, player);
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

		public void PlayGame(String[,] currBoard, int player)
		{
            String choice;

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
                    Console.WriteLine("Player 1 Turn");
                }
                Console.WriteLine("\n");
                board.DisplayBoard(currBoard);
                choice = Console.ReadLine();

                if (!board.MarkBoard(ReadMove(choice), player, currBoard))
                    PlayGame(currBoard, player);
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
                Thread.Sleep(2000);
                System.Environment.Exit(1);
            }
        }

        public Tuple<int, int> ReadMove(String num)
        {
            int pos = 1;
            if (!int.TryParse(num, out int lookingFor))
                return Tuple.Create(-1, -1);
            for (int r = 0; r < sizeOfBoard; r++)
            {
                for (int c = 0; c < sizeOfBoard; c++)
                {
                    if (pos == lookingFor)
                        return Tuple.Create(r, c);
                    pos++;
                    
                }
            }
            return Tuple.Create(-1, -1);
        }
    }
}
