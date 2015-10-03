using System;
using System.Threading;
namespace TIC_TAC_TOE
{
    class Program
    {
        static char[] arr = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static int player = 1; 
        static int choice; 
        static int flag = 0;
        static ConsoleColor player1Color = ConsoleColor.Red;
        static ConsoleColor player2Color = ConsoleColor.Blue;
        static int currentColumn = 1, currentRow = 1;
      
        static void Main()
        {
            Console.WriteLine("Enter first name: ");
            string playerOneName = Console.ReadLine();
            Console.WriteLine("Enter second name: ");
            string playerTwoName = Console.ReadLine();
            do
            {
               
                Console.Clear();// whenever loop will be again start then screen will be clear  
                Board(true,playerOneName,playerTwoName);// calling the board Function  
                choice = ReadInput(playerOneName, playerTwoName); // int.Parse(Console.ReadLine());//Taking users choice   
                // checking that position where user want to run is marked (with X or O) or not  
                if (arr[choice] != 'X' && arr[choice] != 'O')
                {
                    if (player % 2 == 0) //if chance is of player 2 then mark O else mark X  
                    {
                        arr[choice] = 'O';
                        player++;
                    }
                    else
                    {

                        arr[choice] = 'X';

                        player++;
                    }

                }

                else //If there is any possition where user want to run and that is already marked then show message and load board again  

                {
                    Console.SetCursorPosition(0, 15);
                    Console.WriteLine("Sorry the cell is already marked with {0}", arr[choice]);

                    Console.WriteLine("\n");

                    Console.WriteLine("Please wait 2 second board is loading again.....");

                    Thread.Sleep(2000);
                }
                flag = CheckWin();// calling of check win  

            } while (flag != 1 && flag != -1);// This loop will be run until all cells of the grid are not marked with X and O or some player didn't win  
            Console.Clear();// clearing the console  
            Board(false, playerOneName, playerTwoName);// show the board once again, but without the current active player
            if (flag == 1)// if flag value is 1 then some one has win or means who played marked last time which has win  

            {

                Console.WriteLine("Player {0} has won", (player % 2) + 1);

            }

            else// if flag value is -1 the match will be draw and no one is winner  

            {

                Console.WriteLine("No one has won");

            }

            Console.ReadLine();

        }

        /// <summary>
        /// Reads a key from the keyboard and highlights the corresponding cell in the board.
        /// If the pressed key is Enter, the highlighted cell is marked by the current player.
        /// </summary>
        /// <returns>The index of the marked cell: 1-9</returns>
        public static int ReadInput(string playerOneName, string playerTwoName)
        {
            int choice = 1;
            DrawCurrentCell(currentColumn, currentRow);

            // Read a key from the keyboard
            ConsoleKeyInfo keyInfo;
            while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (currentRow > 1)
                            currentRow--;
                        break;

                    case ConsoleKey.RightArrow:
                        if (currentColumn < 3)
                            currentColumn++;
                        break;

                    case ConsoleKey.DownArrow:
                        if (currentRow < 3)
                            currentRow++;
                        break;

                    case ConsoleKey.LeftArrow:
                        if (currentColumn > 1)
                            currentColumn--;
                        break;
                }

                // draw the board
                Board(true, playerOneName,playerTwoName);

                // draw the highlighted cell
                DrawCurrentCell(currentColumn, currentRow);
            }

            // turn the current currentColumn and currentRow coordinates into an index from the array
            choice = (currentRow - 1) * 3 + currentColumn;

            return choice;
        }
        // Draws the currently highlighted cell
        public static void DrawCurrentCell(int x, int y)
        {
            // Console.WriteLine(x + " " + y); // turn on for debug

            // turn the index of row and column into a coordiante in the console window
            int consoleX = 6 * (x - 1);
            int consoleY = 3 * (y - 1);

            // Determine the color of the current player
            if (player % 2 == 0)
                Console.BackgroundColor = player2Color;
            else
                Console.BackgroundColor = player1Color;

            // draw the highlight
            Console.SetCursorPosition(consoleX, consoleY);
            string s = new string(' ', 5);
            Console.Write(s);

            Console.SetCursorPosition(consoleX, consoleY + 1);
            Console.Write(s);

            // Draw the underlaying character - X or O
            // turn the current currentColumn and currentRow coordinates into an index from the array
            int choiceIndex = (y - 1) * 3 + x;
            Console.SetCursorPosition(consoleX + 2, consoleY + 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(arr[choiceIndex]);

            Console.SetCursorPosition(consoleX, consoleY + 2);
            Console.Write(s);

            // reset the background color
            Console.ResetColor();
        }
        // Board method which creats board  
        private static void Board(bool showCurrentPlayer , string playerOneName, string playerTwoName)
        {
            Console.Clear();
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", arr[1], arr[2], arr[3]);

            Console.WriteLine("_____|_____|_____ ");

            Console.WriteLine("     |     |      ");

            Console.WriteLine("  {0}  |  {1}  |  {2}", arr[4], arr[5], arr[6]);

            Console.WriteLine("_____|_____|_____ ");

            Console.WriteLine("     |     |      ");

            Console.WriteLine("  {0}  |  {1}  |  {2}", arr[7], arr[8], arr[9]);

            Console.WriteLine("     |     |      ");

            Console.ForegroundColor = player1Color;
            Console.Write(playerOneName);
            Console.ResetColor();
            Console.Write(" and ");
            Console.ForegroundColor = player2Color;
            Console.Write(playerTwoName);
            Console.WriteLine("\n");
            if (showCurrentPlayer && flag != 0)
            {
                if (player % 2 == 0)//checking the chance of the player  
                {
                    Console.ForegroundColor = player2Color;
                    Console.WriteLine("Player 2 Chance");
                }
                else
                {
                    Console.ForegroundColor = player1Color;
                    Console.WriteLine("Player 1 Chance");
                }
            }

            Console.ResetColor();
            Console.WriteLine("\n");
        }
        //Checking that any player has won or not  
        private static int CheckWin()
        {

            #region Horzontal Winning Condtion
            //Winning Condition For First Row   
            if (arr[1] == arr[2] && arr[2] == arr[3])

            {

                return 1;

            }

            //Winning Condition For Second Row   

            else if (arr[4] == arr[5] && arr[5] == arr[6])
            {
                return 1;
            }
            //Winning Condition For Third Row   

            else if (arr[7] == arr[8] && arr[7] == arr[9])
            {

                return 1;

            }
            #endregion
            #region vertical Winning Condtion
            //Winning Condition For First Column       
            else if (arr[1] == arr[4] && arr[4] == arr[7])
            {
                return 1;
            }
            //Winning Condition For Second Column  
            else if (arr[2] == arr[5] && arr[5] == arr[8])
            {
                return 1;
            }
            //Winning Condition For Third Column  
            else if (arr[3] == arr[6] && arr[6] == arr[9])

            {
                return 1;
            }
            #endregion
            #region Diagonal Winning Condition

            else if (arr[1] == arr[5] && arr[5] == arr[9])
            {
                return 1;
            }
            else if (arr[3] == arr[5] && arr[5] == arr[7])
            {
                return 1;
            }
            #endregion
            #region Checking For Draw
            // If all the cells or values filled with X or O then any player has won the match  
            else if (arr[1] != '1' && arr[2] != '2' && arr[3] != '3' && arr[4] != '4' && arr[5] != '5' && arr[6] != '6' && arr[7] != '7' && arr[8] != '8' && arr[9] != '9')

            {
               return -1;
            }
            #endregion
            else
            {
                return 0;
            }

        }

    }

}
