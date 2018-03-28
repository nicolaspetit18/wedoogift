using ConsoleMastermindGame.Classes;
using System;

namespace ConsoleMastermindGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //Loop necessary to play multiple games
            while (true)
            {
                //Intro Sequence & Game Setup
                Console.WriteLine("MasterMind Console Game");
                System.Threading.Thread.Sleep(2000);

                int intGuesses = GetNumberOfGuesses();
                var game = new Game(intGuesses, 4);
                game.GenerateSecretCode();
                Console.Clear();

                //Guesses Loop
                while (game.GetLeftRoundsCount() > 0)
                {
                    Console.WriteLine("Guesses Remaining: " + game.GetLeftRoundsCount().ToString());

                    Console.WriteLine("\nMake your guess ( R, J, B, O, V, N ) :\n");
                    string strUserGuess = Console.ReadLine();


                    if (game.IsGuessCorrectFormat(strUserGuess))
                    {
                        game.AddNewMove(strUserGuess);

                        if (game.UserHasWon()) //Game has been won.
                        {
                            break;
                        }

                        int inPlaceCount = game.GetInPlaceCount(strUserGuess);
                        int outOfPlaceCount = game.GetOutOfPlaceCount(strUserGuess);

                        Console.Clear();
                        Console.WriteLine(game.ToDisplay());
                    }
                    else
                        Console.WriteLine("Make sure your input has to be 4 characters between R, J, B, O, V, N");
                }
                if (game.UserHasWon())
                {
                    Console.WriteLine("--------------------\n");
                    Console.WriteLine("\nYou solved it!");
                }
                else
                {
                    Console.WriteLine("\nYou lose. :(\n");
                    Console.WriteLine("The code was " + game.GetSecretCode());
                }
                if (EndGameDisplay())
                {
                    Console.Clear();
                    continue;
                }
                break;
            }
        }
        #region Functions

        /// <summary>
        /// Displays the End Game screen.
        /// </summary>
        /// <returns>True if the user wishes to play again, false otherwise</returns>
        private static bool EndGameDisplay()
        {
            Console.WriteLine("\nWould you like to play again? (Y/N)\n");
            while (true)
            {
                string strPlayAgain = Console.ReadLine();
                if (strPlayAgain == "N" || strPlayAgain == "n" || strPlayAgain == "No" || strPlayAgain == "no")
                {
                    return false;
                }
                else if (strPlayAgain == "Y" || strPlayAgain == "y" || strPlayAgain == "Yes" || strPlayAgain == "yes")
                {
                    return true;
                }
                Console.WriteLine("\nPlease enter either a Y or a N.\n");
            }
        }

        /// <summary>
        /// Recursive function for input of number of Guesses
        /// </summary>
        /// <returns>The Number of Guesses</returns>
        private static int GetNumberOfGuesses()
        {
            Console.Clear();
            Console.WriteLine("How many guesses would you like to have?\n");
            int intGuesses = 0;
            try
            {
                intGuesses = Int32.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("\nNumber of guesses must be an integer.\n");
                System.Threading.Thread.Sleep(2000);
                intGuesses = GetNumberOfGuesses();
            }
            return intGuesses;
        }

        #endregion
    }
}
