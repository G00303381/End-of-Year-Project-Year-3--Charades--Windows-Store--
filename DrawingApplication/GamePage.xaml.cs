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
    public sealed partial class GamePage : Page
    {
       
        DispatcherTimer myDispatcherTimer = new DispatcherTimer();

        Random random = new Random();
        int timeLeft, secondInterval = 0;
        string drawThis;
        bool isButtonsVisible;
        
        public GamePage()
        {
            InitializeComponent();
            myDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            myDispatcherTimer.Tick += Each_Tick;         
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {           
            checkPlayersAndScore();
            checkWords();
            checkPlayers();
            checkAssigned();
            //set secondinterval 0

        }
        
        private void btnAssignOrder_Click(object sender, RoutedEventArgs e)
        {
            int numOfPlayers = globalVar.NumOfPlayers;
            //int randomNum;

            addPlayers(numOfPlayers);
            orderPlayers();

            btnAssignOrder.Visibility = Visibility.Collapsed;
            tbNowDrawingPlayer.Text = globalVar.playerListOrdered[globalVar.NumOfPlayersThatDrew].ToString();
            globalVar.isAssigned = true;
            //playersListOrdered.RemoveAt(0);
        }

        private void btnReady_Click(object sender, RoutedEventArgs e)
        {
            if (isButtonsVisible == true)
            {
                var error = new MessageDialog("Please select who guessed the word.");
            }
            else if(globalVar.isAssigned == true)
            {
                timeLeft = 5;
                tbTimeLeft.Text = "Word showing in: 5";
                myDispatcherTimer.Start();
                btnReady.Visibility = Visibility.Collapsed;
                tbTimeLeft.Visibility = Visibility.Visible;

                whatToDraw();
                tbWhatToDraw.Text = drawThis;
            }
            else
            {
                var error = new MessageDialog("Please assign order first.");
            }
                      
        }

        public void Each_Tick(object sender, object e)
        {
            timeLeft--;
            tbTimeLeft.Text = "Word showing in: " + timeLeft.ToString();

            if (secondInterval == 1)
            {
                tbTimeLeft2.Text = "Starting to draw in: " + timeLeft.ToString();
            }         
            if (timeLeft == 0 && secondInterval == 0)
            {
                secondInterval++;
                timeLeft = 6;
                //tbTimeLeft2.Text = "";
                tbTimeLeft.Visibility = Visibility.Collapsed;
                tbTimeLeft2.Visibility = Visibility.Visible;
                tbWhatToDraw.Visibility = Visibility.Visible;
            }
            if (timeLeft == 0 && secondInterval == 1)
            {
                //draw 
                Frame.Navigate(typeof(Drawing));

                secondInterval = 0;
                tbTimeLeft.Visibility = Visibility.Collapsed;
                tbTimeLeft2.Visibility = Visibility.Collapsed;
                tbWhatToDraw.Visibility = Visibility.Collapsed;
                btnReady.Visibility = Visibility.Visible;
                myDispatcherTimer.Stop();
            }

        }

        private void checkAssigned()
        {
            if (globalVar.isAssigned == true)
            {
                btnAssignOrder.Visibility = Visibility.Collapsed;
            }
        }

        private void checkPlayers()
        {
            if (globalVar.NumOfPlayers == globalVar.NumOfPlayersThatDrew)
            {
                globalVar.NumOfPlayersThatDrew = 0;
                tbNowDrawingPlayer.Text = globalVar.playerListOrdered[globalVar.NumOfPlayersThatDrew].ToString();
                //playersListOrdered = playersListBackup;
            }
            else if (globalVar.playerListOrdered.Count() != 0)
            {
                tbNowDrawingPlayer.Text = globalVar.playerListOrdered[globalVar.NumOfPlayersThatDrew].ToString();
                //playersListOrdered.RemoveAt(0);
            }
        }

        private void checkPlayersAndScore()
        {
            int numOfPlayers = globalVar.NumOfPlayers;

            if (numOfPlayers == 1)
            {
                tbPlayer1.Visibility = Visibility.Visible;
                tbScorePlayer1.Visibility = Visibility.Visible;

                if (globalVar.isDrawingDone == true)
                {
                    isButtonsVisible = true;
                    btnGuessPlayer1.Visibility = Visibility.Visible;
                }

                tbPlayer1.Text = globalVar.Player1;
                tbScorePlayer1.Text = globalVar.Player1Score.ToString();
            }
            else if (numOfPlayers == 2)
            {
                tbPlayer1.Visibility = Visibility.Visible;
                tbScorePlayer1.Visibility = Visibility.Visible;               
                tbPlayer2.Visibility = Visibility.Visible;
                tbScorePlayer2.Visibility = Visibility.Visible;
                
                if (globalVar.isDrawingDone == true)
                {
                    isButtonsVisible = true;
                    btnGuessPlayer1.Visibility = Visibility.Visible;
                    btnGuessPlayer2.Visibility = Visibility.Visible;
                }

                tbPlayer1.Text = globalVar.Player1;
                tbScorePlayer1.Text = globalVar.Player1Score.ToString();
                tbPlayer2.Text = globalVar.Player2;
                tbScorePlayer2.Text = globalVar.Player2Score.ToString();
            }
            else if (numOfPlayers == 3)
            {
                tbPlayer1.Visibility = Visibility.Visible;
                tbScorePlayer1.Visibility = Visibility.Visible;            
                tbPlayer2.Visibility = Visibility.Visible;
                tbScorePlayer2.Visibility = Visibility.Visible;                
                tbPlayer3.Visibility = Visibility.Visible;
                tbScorePlayer3.Visibility = Visibility.Visible;
                
                if (globalVar.isDrawingDone == true)
                {
                    isButtonsVisible = true;
                    btnGuessPlayer1.Visibility = Visibility.Visible;
                    btnGuessPlayer2.Visibility = Visibility.Visible;
                    btnGuessPlayer3.Visibility = Visibility.Visible;
                }

                tbPlayer1.Text = globalVar.Player1;
                tbScorePlayer1.Text = globalVar.Player1Score.ToString();
                tbPlayer2.Text = globalVar.Player2;
                tbScorePlayer2.Text = globalVar.Player2Score.ToString();
                tbPlayer3.Text = globalVar.Player3;
                tbScorePlayer3.Text = globalVar.Player3Score.ToString();
            }
            else if (numOfPlayers == 4)
            {
                tbPlayer1.Visibility = Visibility.Visible;
                tbScorePlayer1.Visibility = Visibility.Visible;              
                tbPlayer2.Visibility = Visibility.Visible;
                tbScorePlayer2.Visibility = Visibility.Visible;                
                tbPlayer3.Visibility = Visibility.Visible;
                tbScorePlayer3.Visibility = Visibility.Visible;               
                tbPlayer4.Visibility = Visibility.Visible;
                tbScorePlayer4.Visibility = Visibility.Visible;
                
                if (globalVar.isDrawingDone == true)
                {
                    isButtonsVisible = true;
                    btnGuessPlayer1.Visibility = Visibility.Visible;
                    btnGuessPlayer2.Visibility = Visibility.Visible;
                    btnGuessPlayer3.Visibility = Visibility.Visible;
                    btnGuessPlayer4.Visibility = Visibility.Visible;
                }

                tbPlayer1.Text = globalVar.Player1;
                tbScorePlayer1.Text = globalVar.Player1Score.ToString();
                tbPlayer2.Text = globalVar.Player2;
                tbScorePlayer2.Text = globalVar.Player2Score.ToString();
                tbPlayer3.Text = globalVar.Player3;
                tbScorePlayer3.Text = globalVar.Player3Score.ToString();
                tbPlayer4.Text = globalVar.Player4;
                tbScorePlayer4.Text = globalVar.Player4Score.ToString();
            }
            else if (numOfPlayers == 5)
            {
                tbPlayer1.Visibility = Visibility.Visible;
                tbScorePlayer1.Visibility = Visibility.Visible;                
                tbPlayer2.Visibility = Visibility.Visible;
                tbScorePlayer2.Visibility = Visibility.Visible;              
                tbPlayer3.Visibility = Visibility.Visible;
                tbScorePlayer3.Visibility = Visibility.Visible;                
                tbPlayer4.Visibility = Visibility.Visible;
                tbScorePlayer4.Visibility = Visibility.Visible;                
                tbPlayer5.Visibility = Visibility.Visible;
                tbScorePlayer5.Visibility = Visibility.Visible;
                
                if (globalVar.isDrawingDone == true)
                {
                    isButtonsVisible = true;
                    btnGuessPlayer1.Visibility = Visibility.Visible;
                    btnGuessPlayer2.Visibility = Visibility.Visible;
                    btnGuessPlayer3.Visibility = Visibility.Visible;
                    btnGuessPlayer4.Visibility = Visibility.Visible;
                    btnGuessPlayer5.Visibility = Visibility.Visible;
                }

                tbPlayer1.Text = globalVar.Player1;
                tbScorePlayer1.Text = globalVar.Player1Score.ToString();
                tbPlayer2.Text = globalVar.Player2;
                tbScorePlayer2.Text = globalVar.Player2Score.ToString();
                tbPlayer3.Text = globalVar.Player3;
                tbScorePlayer3.Text = globalVar.Player3Score.ToString();
                tbPlayer4.Text = globalVar.Player4;
                tbScorePlayer4.Text = globalVar.Player4Score.ToString();
                tbPlayer5.Text = globalVar.Player5;
                tbScorePlayer5.Text = globalVar.Player5Score.ToString();
            }
            else if (numOfPlayers == 6)
            {
                tbPlayer1.Visibility = Visibility.Visible;
                tbScorePlayer1.Visibility = Visibility.Visible;                
                tbPlayer2.Visibility = Visibility.Visible;
                tbScorePlayer2.Visibility = Visibility.Visible;               
                tbPlayer3.Visibility = Visibility.Visible;
                tbScorePlayer3.Visibility = Visibility.Visible;                
                tbPlayer4.Visibility = Visibility.Visible;
                tbScorePlayer4.Visibility = Visibility.Visible;                
                tbPlayer5.Visibility = Visibility.Visible;
                tbScorePlayer5.Visibility = Visibility.Visible;               
                tbPlayer6.Visibility = Visibility.Visible;
                tbScorePlayer6.Visibility = Visibility.Visible;
                

                if (globalVar.isDrawingDone == true)
                {
                    isButtonsVisible = true;
                    btnGuessPlayer1.Visibility = Visibility.Visible;
                    btnGuessPlayer2.Visibility = Visibility.Visible;
                    btnGuessPlayer3.Visibility = Visibility.Visible;
                    btnGuessPlayer4.Visibility = Visibility.Visible;
                    btnGuessPlayer5.Visibility = Visibility.Visible;
                    btnGuessPlayer6.Visibility = Visibility.Visible;
                }

                tbPlayer1.Text = globalVar.Player1;
                tbScorePlayer1.Text = globalVar.Player1Score.ToString();
                tbPlayer2.Text = globalVar.Player2;
                tbScorePlayer2.Text = globalVar.Player2Score.ToString();
                tbPlayer3.Text = globalVar.Player3;
                tbScorePlayer3.Text = globalVar.Player3Score.ToString();
                tbPlayer4.Text = globalVar.Player4;
                tbScorePlayer4.Text = globalVar.Player4Score.ToString();
                tbPlayer5.Text = globalVar.Player5;
                tbScorePlayer5.Text = globalVar.Player5Score.ToString();
                tbPlayer6.Text = globalVar.Player6;
                tbScorePlayer6.Text = globalVar.Player6Score.ToString();
            }
            else
            {
                tbPlayer1.Visibility = Visibility.Visible;
                tbScorePlayer1.Visibility = Visibility.Visible;              
                tbPlayer2.Visibility = Visibility.Visible;
                tbScorePlayer2.Visibility = Visibility.Visible;                
                tbPlayer3.Visibility = Visibility.Visible;
                tbScorePlayer3.Visibility = Visibility.Visible;                
                tbPlayer4.Visibility = Visibility.Visible;
                tbScorePlayer4.Visibility = Visibility.Visible;                
                tbPlayer5.Visibility = Visibility.Visible;
                tbScorePlayer5.Visibility = Visibility.Visible;                
                tbPlayer6.Visibility = Visibility.Visible;
                tbScorePlayer6.Visibility = Visibility.Visible;                
                tbPlayer7.Visibility = Visibility.Visible;
                tbScorePlayer7.Visibility = Visibility.Visible;
                

                if (globalVar.isDrawingDone == true)
                {
                    isButtonsVisible = true;
                    btnGuessPlayer1.Visibility = Visibility.Visible;
                    btnGuessPlayer2.Visibility = Visibility.Visible;
                    btnGuessPlayer3.Visibility = Visibility.Visible;
                    btnGuessPlayer4.Visibility = Visibility.Visible;
                    btnGuessPlayer5.Visibility = Visibility.Visible;
                    btnGuessPlayer6.Visibility = Visibility.Visible;
                    btnGuessPlayer7.Visibility = Visibility.Visible;
                }

                tbPlayer1.Text = globalVar.Player1;
                tbScorePlayer1.Text = globalVar.Player1Score.ToString();
                tbPlayer2.Text = globalVar.Player2;
                tbScorePlayer2.Text = globalVar.Player2Score.ToString();
                tbPlayer3.Text = globalVar.Player3;
                tbScorePlayer3.Text = globalVar.Player3Score.ToString();
                tbPlayer4.Text = globalVar.Player4;
                tbScorePlayer4.Text = globalVar.Player4Score.ToString();
                tbPlayer5.Text = globalVar.Player5;
                tbScorePlayer5.Text = globalVar.Player5Score.ToString();
                tbPlayer6.Text = globalVar.Player6;
                tbScorePlayer6.Text = globalVar.Player6Score.ToString();
                tbPlayer7.Text = globalVar.Player7;
                tbScorePlayer7.Text = globalVar.Player7Score.ToString();
            }
        }

        private void addPlayers(int players)
        {
            int numOfPlayers = players;

            if (numOfPlayers == 1)
            {
                globalVar.playerList.Add(globalVar.Player1);
            }
            else if (numOfPlayers == 2)
            {
                globalVar.playerList.Add(globalVar.Player1);
                globalVar.playerList.Add(globalVar.Player2);
            }
            else if (numOfPlayers == 3)
            {
                globalVar.playerList.Add(globalVar.Player1);
                globalVar.playerList.Add(globalVar.Player2);
                globalVar.playerList.Add(globalVar.Player3);
            }
            else if (numOfPlayers == 4)
            {
                globalVar.playerList.Add(globalVar.Player1);
                globalVar.playerList.Add(globalVar.Player2);
                globalVar.playerList.Add(globalVar.Player3);
                globalVar.playerList.Add(globalVar.Player4);
            }
            else if (numOfPlayers == 5)
            {
                globalVar.playerList.Add(globalVar.Player1);
                globalVar.playerList.Add(globalVar.Player2);
                globalVar.playerList.Add(globalVar.Player3);
                globalVar.playerList.Add(globalVar.Player4);
                globalVar.playerList.Add(globalVar.Player5);
            }
            else if (numOfPlayers == 6)
            {
                globalVar.playerList.Add(globalVar.Player1);
                globalVar.playerList.Add(globalVar.Player2);
                globalVar.playerList.Add(globalVar.Player3);
                globalVar.playerList.Add(globalVar.Player4);
                globalVar.playerList.Add(globalVar.Player5);
                globalVar.playerList.Add(globalVar.Player6);
            }
            else if (numOfPlayers == 7)
            {
                globalVar.playerList.Add(globalVar.Player1);
                globalVar.playerList.Add(globalVar.Player2);
                globalVar.playerList.Add(globalVar.Player3);
                globalVar.playerList.Add(globalVar.Player4);
                globalVar.playerList.Add(globalVar.Player5);
                globalVar.playerList.Add(globalVar.Player6);
                globalVar.playerList.Add(globalVar.Player7);
            }
            tbNowDrawingPlayer.Text = globalVar.playerList.Count().ToString();
        }

        private void orderPlayers()
        {
            while (globalVar.playerList.Count() > 0)
            {
                int rnd = random.Next(0, globalVar.playerList.Count());
                globalVar.playerListOrdered.Add(globalVar.playerList[rnd]);
                globalVar.playerList.RemoveAt(rnd);
            }

            
            //int randomNum;
            //for (int i = 0; i <= globalVar.playersList.Count()+1; i++)
            //{
            //    if (globalVar.playersList.Count() == 1)
            //    {
            //        globalVar.playersListOrdered.Add(globalVar.playersList[0]);
            //        globalVar.playersList.RemoveAt(0);
            //    }
            //    else
            //    {
            //        randomNum = random.Next(0, globalVar.playersList.Count());
            //        globalVar.playersListOrdered.Add(globalVar.playersList[randomNum]);
            //        globalVar.playersList.RemoveAt(randomNum);

            //    }               
            //}

            //globalVar.playersListBackup = globalVar.playersListOrdered;
        }

        private void whatToDraw()
        {
            int randomNum;

            if (globalVar.Words.Count() == 0)
            {
                globalVar.Words = globalVar.WordsBackup;
            }
            randomNum = random.Next(0, globalVar.Words.Count());
            drawThis = globalVar.Words[randomNum];
            globalVar.Words.RemoveAt(randomNum);
        }

        private void checkWords()
        {
            if (globalVar.Words.Count() == 0)
            {
                globalVar.Words = globalVar.WordsBackup;
            }
        }

        private void hideButtons()
        {
            isButtonsVisible = false;
            btnGuessPlayer1.Visibility = Visibility.Collapsed;
            btnGuessPlayer2.Visibility = Visibility.Collapsed;
            btnGuessPlayer3.Visibility = Visibility.Collapsed;
            btnGuessPlayer4.Visibility = Visibility.Collapsed;
            btnGuessPlayer5.Visibility = Visibility.Collapsed;
            btnGuessPlayer6.Visibility = Visibility.Collapsed;
            btnGuessPlayer7.Visibility = Visibility.Collapsed;
        }

        private void btnGuessPlayer1_Click(object sender, RoutedEventArgs e)
        {
            globalVar.Player1Score += 25;
            tbScorePlayer1.Text = globalVar.Player1Score.ToString();
            tbScorePlayer1.Visibility = Visibility.Visible;
            hideButtons();
        }

        private void btnGuessPlayer2_Click(object sender, RoutedEventArgs e)
        {
            globalVar.Player2Score += 25;
            tbScorePlayer2.Text = globalVar.Player2Score.ToString();
            tbScorePlayer2.Visibility = Visibility.Visible;
            hideButtons();
        }

        private void btnGuessPlayer3_Click(object sender, RoutedEventArgs e)
        {
            globalVar.Player3Score += 25;
            tbScorePlayer3.Text = globalVar.Player3Score.ToString();
            tbScorePlayer3.Visibility = Visibility.Visible;
            hideButtons();
        }

        private void btnGuessPlayer4_Click(object sender, RoutedEventArgs e)
        {
            globalVar.Player4Score += 25;
            tbScorePlayer4.Text = globalVar.Player4Score.ToString();
            tbScorePlayer4.Visibility = Visibility.Visible;
            hideButtons();
        }

        private void btnGuessPlayer5_Click(object sender, RoutedEventArgs e)
        {
            globalVar.Player5Score += 25;
            tbScorePlayer5.Text = globalVar.Player5Score.ToString();
            tbScorePlayer5.Visibility = Visibility.Visible;
            hideButtons();
        }

        private void btnGuessPlayer6_Click(object sender, RoutedEventArgs e)
        {
            globalVar.Player6Score += 25;
            tbScorePlayer6.Text = globalVar.Player6Score.ToString();
            tbScorePlayer6.Visibility = Visibility.Visible;
            hideButtons();
        }

        private void btnGuessPlayer7_Click(object sender, RoutedEventArgs e)
        {
            globalVar.Player7Score += 25;
            tbScorePlayer7.Text = globalVar.Player7Score.ToString();
            tbScorePlayer7.Visibility = Visibility.Visible;
            hideButtons();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignUp));
        }

     

    }
}
