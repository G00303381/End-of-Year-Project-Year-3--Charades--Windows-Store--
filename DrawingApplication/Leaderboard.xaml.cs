using DrawingApplication.Models;
using DrawingApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    ///

    public sealed partial class Leaderboard : Page
    {
        // New code:
        LeaderboardViewModel viewModel = new LeaderboardViewModel(App.MobileService);

        public Leaderboard()
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }
        protected override async void OnNavigatedTo(NavigationEventArgs args)
        {
            await viewModel.GetAllPlayersAsync();

            if (viewModel.Players.Count > 0)
            {
                PlayerListBox.SelectedIndex = 0;
            }

            await viewModel.GetAllRanksAsync();

        }

        private async void SubmitScoreButton_Click(object sender, RoutedEventArgs e)
        {
            var player = this.PlayerListBox.SelectedItem as Player;
            if (player != null)
            {
                int score;
                if (Int32.TryParse(this.PlayerScoreTextBox.Text, out score))
                {
                    await viewModel.SubmitScoreAsync(player, score);
                }
                else
                {
                    viewModel.ErrorMessage = "Please submit a valid score.";
                }
            }
        }

        private async void AddPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            var player = new Player
            {
                Name = AddPlayerTextBox.Text
            };

            await viewModel.AddPlayerAsync(player);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

    }
}
