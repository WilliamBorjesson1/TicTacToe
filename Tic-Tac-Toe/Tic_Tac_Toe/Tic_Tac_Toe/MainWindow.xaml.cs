using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        // Holds the current results of cells in the active game.
        private MarkType[] nResults;

        // true if its player 1's turn (X) and false if its player 2's turn (O)
        private bool nPlayer1Turn;
        //True if the gam has ended.
        private bool nGameEnded;

        #endregion


        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }
        #endregion

        // Starts a new game and clear all the values back to the start.
        private void NewGame()
        {
            //Create a new blank array of fre cells.
            nResults = new MarkType[9];

            for (var i = 0; i < nResults.Length; i++)
            {
                nResults[i] = MarkType.Free;
            }

            // Make sure player 1 starts the game
            nPlayer1Turn = true;

            //Interate every button on the grid.
            Container.Children.Cast<Button>().ToList().ForEach(Button =>
     
            {
            // Change background, foreground, content
                Button.Content = string.Empty;
                Button.Background = Brushes.White;
                Button.Foreground = Brushes.Blue;
            
            });

            // Make sure that the game hasn't ended.
            nGameEnded = false;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (nGameEnded == true)
            {
                NewGame();
                return;
            }

            // cast the sender to a button.
            var button = (Button)sender;

            // find the buttons position in the array.
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            // dopn't do anything if the cell has a value in it.
            if (nResults[index] != MarkType.Free)
            {
                return;
            }

            // set the cell value based on which player's turn it is.
            if (nPlayer1Turn)
            {
                nResults[index] = MarkType.Cross;
            }
            else
            {
                nResults[index] = MarkType.Nought;
            }
            // alternative solution:

            // nResults[index] = nPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            // Set button text to the results.
            button.Content = nPlayer1Turn ? "X" : "O";

            // Set noughts colour to green.
            if (nPlayer1Turn == false)
            {
                button.Foreground = Brushes.Red;
            }

            // toggle the players turns.
            nPlayer1Turn ^= true;

            // Alternative solution:

            /*
            if (nPlayer1Turn)
            {
                nPlayer1Turn = false;
            }
            else
            {
                nPlayer1Turn = true;
            }
            
             */

            // Check for a winner.
            CheckForWinner();
        }

        private void CheckForWinner()
        {
            //check for horizontal wins
            //
            // Row - 0
            //
            if(nResults[0] != MarkType.Free && (nResults[0] & nResults[1] & nResults[2]) == nResults[0])
            {
                //Game ends
                nGameEnded = true;

                //highlight winning cells in green.
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            //
            // Row - 1
            //
            if (nResults[3] != MarkType.Free && (nResults[3] & nResults[4] & nResults[5]) == nResults[3])
            {
                //Game ends
                nGameEnded = true;

                //highlight winning cells in green.
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            //
            // Row - 2
            //
            if (nResults[6] != MarkType.Free && (nResults[6] & nResults[7] & nResults[8]) == nResults[6])
            {
                //Game ends
                nGameEnded = true;

                //highlight winning cells in green.
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }

            //check for Vertical wins
            //
            // Column - 0
            //
            if (nResults[0] != MarkType.Free && (nResults[0] & nResults[3] & nResults[6]) == nResults[0])
            {
                //Game ends
                nGameEnded = true;

                //highlight winning cells in green.
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            
            //check for Vertical wins
            //
            // Column - 1
            //
            if (nResults[1] != MarkType.Free && (nResults[1] & nResults[4] & nResults[7]) == nResults[1])
            {
                //Game ends
                nGameEnded = true;

                //highlight winning cells in green.
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }

            //check for Vertical wins
            //
            // Column - 2
            //
            if (nResults[2] != MarkType.Free && (nResults[2] & nResults[5] & nResults[8]) == nResults[2])
            {
                //Game ends
                nGameEnded = true;

                //highlight winning cells in green.
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }

            //check for diagnolly wins
            //
            // Diagnolly -  Right 
            //
            if (nResults[2] != MarkType.Free && (nResults[2] & nResults[4] & nResults[6]) == nResults[2])
            {
                //Game ends
                nGameEnded = true;

                //highlight winning cells in green.
                Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.Green;
            }
            
            //check for diagnolly wins
            //
            // Diagnolly -  Left 
            //
            if (nResults[0] != MarkType.Free && (nResults[0] & nResults[4] & nResults[8]) == nResults[0])
            {
                //Game ends
                nGameEnded = true;

                //highlight winning cells in green.
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }



            // Check for no winner and full board.
            if (!nResults.Any(result => result == MarkType.Free) && !nGameEnded)
            {
                // game ended
                nGameEnded = true;

                //turn all cells LightGray.
                Container.Children.Cast<Button>().ToList().ForEach(Button =>

                {
                    // Change background, foreground, content
                    Button.Background = Brushes.LightGray;
                });
            }
        }
    }
}
