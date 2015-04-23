using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DrawingApplication
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUp : Page
    {
        public SignUp()
        {
            this.InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (tbPlayer1.Visibility == Visibility.Visible && tbPlayer1.Text == "")
            {
                var error = new MessageDialog("Please fill name for player 1.");
            }
            else if (tbPlayer2.Visibility == Visibility.Visible && tbPlayer2.Text == "")
            {
                var error = new MessageDialog("Please fill name for player 1.");
            }
            else if (tbPlayer3.Visibility == Visibility.Visible && tbPlayer3.Text == "")
            {
               var error = new MessageDialog("Please fill name for player 1.");
            }
            else if (tbPlayer4.Visibility == Visibility.Visible && tbPlayer4.Text == "")
            {
                var error = new MessageDialog("Please fill name for player 1.");
            }
            else if (tbPlayer5.Visibility == Visibility.Visible && tbPlayer5.Text == "")
            {
               var error = new MessageDialog("Please fill name for player 1.");
            }
            else if (tbPlayer6.Visibility == Visibility.Visible && tbPlayer6.Text == "")
            {
                var error = new MessageDialog("Please fill name for player 1.");
            }
            else if (tbPlayer7.Visibility == Visibility.Visible && tbPlayer7.Text == "")
            {
                var error = new MessageDialog("Please fill name for player 1.");
            }
            else
            {
                globalVar.Player1 = tbPlayer1.Text;
                globalVar.Player2 = tbPlayer2.Text;
                globalVar.Player3 = tbPlayer3.Text;
                globalVar.Player4 = tbPlayer4.Text;
                globalVar.Player5 = tbPlayer5.Text;
                globalVar.Player6 = tbPlayer6.Text;
                globalVar.Player7 = tbPlayer7.Text;

                Frame.Navigate(typeof(GamePage));

            }

           
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            onePlayer();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            twoPlayers();
        }
        
        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            threePlayers();
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            fourPlayers();
        }
        
        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            fivePlayers();
        }
        
        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            sixPlayers();
        }
        
        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            sevenPlayers();
        }

        private void onePlayer()
        {
            globalVar.NumOfPlayers = 1;
            txtNoOfPlayers.Visibility = Visibility.Collapsed;
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn3.Visibility = Visibility.Collapsed;
            btn4.Visibility = Visibility.Collapsed;
            btn5.Visibility = Visibility.Collapsed;
            btn6.Visibility = Visibility.Collapsed;
            btn7.Visibility = Visibility.Collapsed;
            txtPlayer.Visibility = Visibility.Visible;
            txtPlayer1.Visibility = Visibility.Visible;
            tbPlayer1.Visibility = Visibility.Visible;
            btnSubmit.Visibility = Visibility.Visible;
        }

        private void twoPlayers()
        {
            globalVar.NumOfPlayers = 2;
            txtNoOfPlayers.Visibility = Visibility.Collapsed;
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn3.Visibility = Visibility.Collapsed;
            btn4.Visibility = Visibility.Collapsed;
            btn5.Visibility = Visibility.Collapsed;
            btn6.Visibility = Visibility.Collapsed;
            btn7.Visibility = Visibility.Collapsed;
            txtPlayer.Visibility = Visibility.Visible;
            txtPlayer1.Visibility = Visibility.Visible;
            txtPlayer2.Visibility = Visibility.Visible;
            tbPlayer1.Visibility = Visibility.Visible;
            tbPlayer2.Visibility = Visibility.Visible;
            btnSubmit.Visibility = Visibility.Visible;
        }

        private void threePlayers()
        {
            globalVar.NumOfPlayers = 3;
            txtNoOfPlayers.Visibility = Visibility.Collapsed;
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn3.Visibility = Visibility.Collapsed;
            btn4.Visibility = Visibility.Collapsed;
            btn5.Visibility = Visibility.Collapsed;
            btn6.Visibility = Visibility.Collapsed;
            btn7.Visibility = Visibility.Collapsed;
            txtPlayer.Visibility = Visibility.Visible;
            txtPlayer1.Visibility = Visibility.Visible;
            txtPlayer2.Visibility = Visibility.Visible;
            txtPlayer3.Visibility = Visibility.Visible;
            tbPlayer1.Visibility = Visibility.Visible;
            tbPlayer2.Visibility = Visibility.Visible;
            tbPlayer3.Visibility = Visibility.Visible;
            btnSubmit.Visibility = Visibility.Visible;
        }

        private void fourPlayers()
        {
            globalVar.NumOfPlayers = 4;
            txtNoOfPlayers.Visibility = Visibility.Collapsed;
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn3.Visibility = Visibility.Collapsed;
            btn4.Visibility = Visibility.Collapsed;
            btn5.Visibility = Visibility.Collapsed;
            btn6.Visibility = Visibility.Collapsed;
            btn7.Visibility = Visibility.Collapsed;
            txtPlayer.Visibility = Visibility.Visible;
            txtPlayer1.Visibility = Visibility.Visible;
            txtPlayer2.Visibility = Visibility.Visible;
            txtPlayer3.Visibility = Visibility.Visible;
            txtPlayer4.Visibility = Visibility.Visible;
            tbPlayer1.Visibility = Visibility.Visible;
            tbPlayer2.Visibility = Visibility.Visible;
            tbPlayer3.Visibility = Visibility.Visible;
            tbPlayer4.Visibility = Visibility.Visible;
            btnSubmit.Visibility = Visibility.Visible;
        }

        private void fivePlayers()
        {
            globalVar.NumOfPlayers = 5;
            txtNoOfPlayers.Visibility = Visibility.Collapsed;
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn3.Visibility = Visibility.Collapsed;
            btn4.Visibility = Visibility.Collapsed;
            btn5.Visibility = Visibility.Collapsed;
            btn6.Visibility = Visibility.Collapsed;
            btn7.Visibility = Visibility.Collapsed;
            txtPlayer.Visibility = Visibility.Visible;
            txtPlayer1.Visibility = Visibility.Visible;
            txtPlayer2.Visibility = Visibility.Visible;
            txtPlayer3.Visibility = Visibility.Visible;
            txtPlayer4.Visibility = Visibility.Visible;
            txtPlayer5.Visibility = Visibility.Visible;
            tbPlayer1.Visibility = Visibility.Visible;
            tbPlayer2.Visibility = Visibility.Visible;
            tbPlayer3.Visibility = Visibility.Visible;
            tbPlayer4.Visibility = Visibility.Visible;
            tbPlayer5.Visibility = Visibility.Visible;
            btnSubmit.Visibility = Visibility.Visible;
        }

        private void sixPlayers()
        {
            globalVar.NumOfPlayers = 6;
            txtNoOfPlayers.Visibility = Visibility.Collapsed;
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn3.Visibility = Visibility.Collapsed;
            btn4.Visibility = Visibility.Collapsed;
            btn5.Visibility = Visibility.Collapsed;
            btn6.Visibility = Visibility.Collapsed;
            btn7.Visibility = Visibility.Collapsed;
            txtPlayer.Visibility = Visibility.Visible;
            txtPlayer1.Visibility = Visibility.Visible;
            txtPlayer2.Visibility = Visibility.Visible;
            txtPlayer3.Visibility = Visibility.Visible;
            txtPlayer4.Visibility = Visibility.Visible;
            txtPlayer5.Visibility = Visibility.Visible;
            txtPlayer6.Visibility = Visibility.Visible;
            tbPlayer1.Visibility = Visibility.Visible;
            tbPlayer2.Visibility = Visibility.Visible;
            tbPlayer3.Visibility = Visibility.Visible;
            tbPlayer4.Visibility = Visibility.Visible;
            tbPlayer5.Visibility = Visibility.Visible;
            tbPlayer6.Visibility = Visibility.Visible;
            btnSubmit.Visibility = Visibility.Visible;
        }

        private void sevenPlayers()
        {
            globalVar.NumOfPlayers = 7;
            txtNoOfPlayers.Visibility = Visibility.Collapsed;
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn3.Visibility = Visibility.Collapsed;
            btn4.Visibility = Visibility.Collapsed;
            btn5.Visibility = Visibility.Collapsed;
            btn6.Visibility = Visibility.Collapsed;
            btn7.Visibility = Visibility.Collapsed;
            txtPlayer.Visibility = Visibility.Visible;
            txtPlayer1.Visibility = Visibility.Visible;
            txtPlayer2.Visibility = Visibility.Visible;
            txtPlayer3.Visibility = Visibility.Visible;
            txtPlayer4.Visibility = Visibility.Visible;
            txtPlayer5.Visibility = Visibility.Visible;
            txtPlayer6.Visibility = Visibility.Visible;
            txtPlayer7.Visibility = Visibility.Visible;
            tbPlayer1.Visibility = Visibility.Visible;
            tbPlayer2.Visibility = Visibility.Visible;
            tbPlayer3.Visibility = Visibility.Visible;
            tbPlayer4.Visibility = Visibility.Visible;
            tbPlayer5.Visibility = Visibility.Visible;
            tbPlayer6.Visibility = Visibility.Visible;
            tbPlayer7.Visibility = Visibility.Visible;
            btnSubmit.Visibility = Visibility.Visible;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

    }
}
