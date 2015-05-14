using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace FamilienDuell
{
    public partial class ControllerWindow : Window
    {
        public ControllerWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartNextRound();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = App.GameController;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            App.GameController.WrongAnswer();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ShowQuestion();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var b = (Button)sender;
            var answer = int.Parse(b.Tag.ToString());

            App.GameController.CorrectAnswer(answer);

            b.IsEnabled = false;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            WinThisRound(1);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            WinThisRound(2);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            App.GameController.PlaySound(GameSound.Intro);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            ShowAllAnswers();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            App.GameController.ClearWrongAnswers();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            PressGameKey(e);
        }

        internal void PressGameKey(KeyEventArgs e)
        {
            if (TextBoxNameTeam1.IsKeyboardFocused || TextBoxNameTeam2.IsKeyboardFocused)
                return;

            if (e.Key == Key.D1 && ButtonCorrect1.IsEnabled)
            {
                App.GameController.CorrectAnswer(1);
                ButtonCorrect1.IsEnabled = false;
            }
            if (e.Key == Key.D2 && ButtonCorrect2.IsEnabled)
            {
                App.GameController.CorrectAnswer(2);
                ButtonCorrect2.IsEnabled = false;
            }
            if (e.Key == Key.D3 && ButtonCorrect3.IsEnabled)
            {
                App.GameController.CorrectAnswer(3);
                ButtonCorrect3.IsEnabled = false;
            }
            if (e.Key == Key.D4 && ButtonCorrect4.IsEnabled)
            {
                App.GameController.CorrectAnswer(4);
                ButtonCorrect4.IsEnabled = false;
            }
            if (e.Key == Key.D5 && ButtonCorrect5.IsEnabled)
            {
                App.GameController.CorrectAnswer(5);
                ButtonCorrect5.IsEnabled = false;
            }
            if (e.Key == Key.D6 && ButtonCorrect6.IsEnabled)
            {
                App.GameController.CorrectAnswer(6);
                ButtonCorrect6.IsEnabled = false;
            }
            if (e.Key == Key.F)
            {
                App.GameController.WrongAnswer();
            }
            if (e.Key == Key.A)
            {
                WinThisRound(1);
            }
            if (e.Key == Key.L)
            {
                WinThisRound(2);
            }
            if (e.Key == Key.N)
            {
                StartNextRound();
            }
            if (e.Key == Key.D)
            {
                App.GameController.ClearWrongAnswers();
            }
            if (e.Key == Key.Z)
            {
                ShowQuestion();
            }
            if (e.Key == Key.G)
            {
                ShowAllAnswers();
            }
        }

        private void StartNextRound()
        {
            if (!ButtonNextRound.IsEnabled)
                return;

            App.GameController.StartNextRound();

            ButtonShowQuestion.IsEnabled = true;
            ButtonCorrect1.IsEnabled = true;
            ButtonCorrect2.IsEnabled = true;
            ButtonCorrect3.IsEnabled = true;
            ButtonCorrect4.IsEnabled = true;
            ButtonCorrect5.IsEnabled = true;
            ButtonCorrect6.IsEnabled = true;
            ButtonShowAll.IsEnabled = true;
            ButtonWinTeam1.IsEnabled = true;
            ButtonWinTeam2.IsEnabled = true;


            if (App.GameController.Round >= App.GameController.AvailableRounds)
                ButtonNextRound.IsEnabled = false;
        }

        private void ShowAllAnswers()
        {
            if (!ButtonShowAll.IsEnabled)
                return;

            App.GameController.ShowAllAnswers();
            ButtonShowAll.IsEnabled = false;

            ButtonCorrect1.IsEnabled = false;
            ButtonCorrect2.IsEnabled = false;
            ButtonCorrect3.IsEnabled = false;
            ButtonCorrect4.IsEnabled = false;
            ButtonCorrect5.IsEnabled = false;
            ButtonCorrect6.IsEnabled = false;
        }

        private void WinThisRound(int p)
        {
            if (!ButtonWinTeam1.IsEnabled && !ButtonWinTeam2.IsEnabled)
                return;

            App.GameController.WinThisRound(p);

            ButtonWinTeam1.IsEnabled = false;
            ButtonWinTeam2.IsEnabled = false;
        }

        private void ShowQuestion()
        {
            if (!ButtonShowQuestion.IsEnabled)
                return;

            App.GameController.ShowQuestion();
            ButtonShowQuestion.IsEnabled = false;
        }
    }
}
