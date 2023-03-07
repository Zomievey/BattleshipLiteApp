using BattleshipLiteLibrary;
using BattleshipLiteLibrary.Models;
using System;
using BattleShipLite;



namespace BattleshipLite
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleDisplayLogic.WelcomeMessage();

            PlayerInfoModel activePlayer = ConsoleDisplayLogic.CreatePlayer("Player 1");
            PlayerInfoModel opponent = ConsoleDisplayLogic.CreatePlayer("Player 2");

            PlayerInfoModel winner = null;

            do
            {

                ConsoleDisplayLogic.DisplayShotGrid(activePlayer);

                ConsoleDisplayLogic.RecordPlayerShot(activePlayer, opponent);

                bool doesGameContinue = GameLogic.PlayerStillActive(opponent);

                if (doesGameContinue == true)
                {

                    //swap using a temp variable
                    //PlayerInfoModel tempHolder = opponent;
                    //opponent = activePlayer;
                    //activePlayer = tempHolder;

                    // ***********OR*************

                    // use a  Tuple
                    (activePlayer, opponent) = (opponent, activePlayer);

                }
                else
                {
                    winner = activePlayer;
                }

            } while (winner == null);
           

            ConsoleDisplayLogic.IdentifyWinner(winner);

            Console.ReadLine();
        }


    }
}
