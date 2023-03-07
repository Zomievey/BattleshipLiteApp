using BattleshipLiteLibrary.Models;
using BattleshipLiteLibrary;
using System;

namespace BattleShipLite
{
    public class ConsoleDisplayLogic
    {
        public static void IdentifyWinner(PlayerInfoModel winner)
        {
            Console.WriteLine($"Congratulations to {winner.UsersName}, you won!");
            Console.WriteLine($"{winner.UsersName} took {GameLogic.GetShotCount(winner)} shots.");
        }

        public static void RecordPlayerShot(PlayerInfoModel activePlayer, PlayerInfoModel opponent)
        {

            bool isValidShot = false;
            string row = "";
            int column = 0;

            do
            {
                string shot = AskForShot(activePlayer);
                try
                {
                    (row, column) = GameLogic.SplitShotIntoRowAndColumn(shot);
                    isValidShot = GameLogic.ValidateShot(activePlayer, row, column);
                    
                }
                catch (Exception)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                    isValidShot = false;
                }

                if (isValidShot == false)
                {
                    Console.WriteLine("Invalid shot, please choose another location");
                }


            } while (isValidShot == false); //can also be written !isValidShot (not isValidShot)

            bool isAHit = GameLogic.IdentifyShotResult(opponent, row, column);

            GameLogic.MarkShotResult(activePlayer, row, column, isAHit);
            GameLogic.DisplayShotResults(row, column, isAHit);

        }

        public static string AskForShot(PlayerInfoModel player)
        {
            Console.Write($"{player.UsersName}, please enter your shot selection: ");
            string output = Console.ReadLine();

            return output;
        }

        public static void DisplayShotGrid(PlayerInfoModel activePlayer)
        {
            string currentRow = activePlayer.ShotGrid[0].SpotLetter;

            foreach (var gridSpot in activePlayer.ShotGrid)

            {
                if (gridSpot.SpotLetter != currentRow)
                {
                    {
                        Console.WriteLine();
                        currentRow = gridSpot.SpotLetter;
                    }
                }
                if (gridSpot.Status == GridSpotStatus.Empty)
                {
                    Console.Write($" {gridSpot.SpotLetter}{gridSpot.SpotNumber} ");


                }
                else if (gridSpot.Status == GridSpotStatus.Hit)
                {
                    Console.Write(" X  "); // x for hit
                }
                else if (gridSpot.Status == GridSpotStatus.Miss)
                {
                    Console.Write(" O  "); // o for miss
                }
                else if (gridSpot.Status == GridSpotStatus.Sunk)
                {
                    IdentifyWinner(activePlayer);
                    Console.Write(" S  "); // s for sunk
                }
                else
                {
                    Console.Write(" ?  "); // that way if we ever see a "?" we know theres a bug somewhere
                }             
            }

            Console.WriteLine();
            Console.WriteLine();
        }
        public static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to Battleship Lite:");
            Console.WriteLine("created by Haylee Hunter");
            Console.WriteLine();
        }

        public static PlayerInfoModel CreatePlayer(string playerTitle)
        {
            PlayerInfoModel output = new PlayerInfoModel();

            Console.WriteLine($"Player information for {playerTitle}");

            // Ask the user for their name
            output.UsersName = AskForUsersName();

            // Load up the shot grid
            GameLogic.InitializeGrid(output);

            // Ask the user for their 5 ship placements
            PlaceShips(output);

            // Clear
            Console.Clear();

            return output;
        }



        public static string AskForUsersName()
        {
            Console.Write("What is your name: ");
            string output = Console.ReadLine();

            return output;
        }

        public static void PlaceShips(PlayerInfoModel model)
        {
            do
            {
                Console.WriteLine("The grid consists of A-E ; and spaces 1-5.");
                Console.WriteLine();
                Console.Write($"Where do you want to place ship number {model.ShipLocations.Count + 1}: ");
                string location = Console.ReadLine();

                bool isValidLocation = false;

                try
                {
                    isValidLocation = GameLogic.PlaceShips(model, location);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);

                }
                if (isValidLocation == false)
                {
                    Console.WriteLine("That was not a valid location. Please try again.");
                }
            } while (model.ShipLocations.Count < 5);
        }

        
    }
}
