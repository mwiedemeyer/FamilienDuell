using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace FamilienDuell
{
    public class GameController : INotifyPropertyChanged
    {
        private SoundPlayer _soundPlayer;
        private IBuzzer _buzzer;

        private string _player1Name = "Team 1";
        private string _player2Name = "Team 2";

        private bool _player1HasBuzzered;
        private bool _player2HasBuzzered;
        private DispatcherTimer _resetBuzzerTimer;
        private bool _isBuzzerAllowed = true;

        private QuestionViewModel _questionViewModel;

        private QuestionContext _questions = null;
        private Question _currentQuestion;
        private int _round = 0;
        private int _thisRoundPoints;
        private int _player1Points;
        private int _player2Points;
        private string _wrongAnswers;

        #region Properties

        public QuestionViewModel QuestionViewModel
        {
            get { return _questionViewModel; }
            set
            {
                if (value != _questionViewModel)
                {
                    _questionViewModel = value;
                    NotifyPropertyChanged("QuestionViewModel");
                }
            }
        }

        public string Player1Name
        {
            get { return _player1Name; }
            set
            {
                if (value != _player1Name)
                {
                    _player1Name = value;
                    NotifyPropertyChanged("Player1Name");
                }
            }
        }

        public string Player2Name
        {
            get { return _player2Name; }
            set
            {
                if (value != _player2Name)
                {
                    _player2Name = value;
                    NotifyPropertyChanged("Player2Name");
                }
            }
        }

        public bool IsBuzzerAllowed
        {
            get { return _isBuzzerAllowed; }
            set
            {
                if (value != _isBuzzerAllowed)
                {
                    _isBuzzerAllowed = value;
                    NotifyPropertyChanged("IsBuzzerAllowed");
                }
            }
        }

        public bool Player1HasBuzzered
        {
            get { return _player1HasBuzzered; }
            set
            {
                if (value != _player1HasBuzzered)
                {
                    _player1HasBuzzered = value;
                    NotifyPropertyChanged("Player1HasBuzzered");
                }
            }
        }

        public bool Player2HasBuzzered
        {
            get { return _player2HasBuzzered; }
            set
            {
                if (value != _player2HasBuzzered)
                {
                    _player2HasBuzzered = value;
                    NotifyPropertyChanged("Player2HasBuzzered");
                }
            }
        }

        public Question CurrentQuestion
        {
            get { return _currentQuestion; }
            set
            {
                if (value != _currentQuestion)
                {
                    _currentQuestion = value;
                    NotifyPropertyChanged("CurrentQuestion");
                }
            }
        }

        public int Round
        {
            get { return _round; }
            set
            {
                if (value != _round)
                {
                    _round = value;
                    NotifyPropertyChanged("Round");
                }
            }
        }

        public int AvailableRounds
        {
            get { return _questions.Questions.Count; }
        }

        public int ThisRoundPoints
        {
            get { return _thisRoundPoints; }
            set
            {
                if (value != _thisRoundPoints)
                {
                    _thisRoundPoints = value;
                    NotifyPropertyChanged("ThisRoundPoints");
                }
            }
        }

        public int Player1Points
        {
            get { return _player1Points; }
            set
            {
                if (value != _player1Points)
                {
                    _player1Points = value;
                    NotifyPropertyChanged("Player1Points");
                }
            }
        }

        public int Player2Points
        {
            get { return _player2Points; }
            set
            {
                if (value != _player2Points)
                {
                    _player2Points = value;
                    NotifyPropertyChanged("Player2Points");
                }
            }
        }

        public string WrongAnswers
        {
            get { return _wrongAnswers; }
            set
            {
                if (value != _wrongAnswers)
                {
                    _wrongAnswers = value;
                    NotifyPropertyChanged("WrongAnswers");
                }
            }
        }

        #endregion

        public GameController(IBuzzer buzzer)
        {
            _buzzer = buzzer;
            _buzzer.Buzzered += Buzzer_Buzzered;
            _soundPlayer = new SoundPlayer();

            _resetBuzzerTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(5), IsEnabled = false };
            _resetBuzzerTimer.Tick += ResetBuzzerTimer_Tick;

            CurrentQuestion = new Question();
            QuestionViewModel = new QuestionViewModel(0)
            {
                QuestionText = "Familien Duell"
            };
        }

        #region Buzzer Logic

        private void ResetBuzzerTimer_Tick(object sender, EventArgs e)
        {
            Player1HasBuzzered = false;
            Player2HasBuzzered = false;
            _resetBuzzerTimer.Stop();
        }

        private void Buzzer_Buzzered(object sender, BuzzeredEventArgs e)
        {
            Buzzered(e.Player);
        }

        internal void Buzzered(int player)
        {
            if (!IsBuzzerAllowed)
                return;

            if (Player1HasBuzzered || Player2HasBuzzered)
                return;

            Player1HasBuzzered = false;
            Player2HasBuzzered = false;

            if (player == 1)
                Player1HasBuzzered = true;
            if (player == 2)
                Player2HasBuzzered = true;

            PlaySound(GameSound.Buzzer);

            if (Round > 0)
                IsBuzzerAllowed = false;

            _resetBuzzerTimer.Start();
        }

        #endregion

        #region Public Methods

        public void StartNextRound()
        {
            IsBuzzerAllowed = false;

            if (_questions == null)
            {
                var fileContent = System.IO.File.ReadAllText("Resources\\Questions.json");
                _questions = JsonConvert.DeserializeObject<QuestionContext>(fileContent);
            }

            PlaySound(GameSound.Round);

            ThisRoundPoints = 0;
            WrongAnswers = string.Empty;

            if (Round >= _questions.Questions.Count)
            {
                return;
            }

            CurrentQuestion = _questions.Questions[Round];
            Round = Round + 1;
            QuestionViewModel = new QuestionViewModel(CurrentQuestion.AvailableAnswers);
        }

        public void ShowQuestion()
        {
            QuestionViewModel.QuestionText = CurrentQuestion.Text;
            IsBuzzerAllowed = true;
        }

        public void ShowAllAnswers()
        {
            QuestionViewModel.A1 = new Answer(CurrentQuestion.A1);
            QuestionViewModel.A2 = new Answer(CurrentQuestion.A2);
            QuestionViewModel.A3 = new Answer(CurrentQuestion.A3);
            QuestionViewModel.A4 = new Answer(CurrentQuestion.A4);
            QuestionViewModel.A5 = new Answer(CurrentQuestion.A5);
            QuestionViewModel.A6 = new Answer(CurrentQuestion.A6);
        }

        public void CorrectAnswer(int number)
        {
            int points = 0;
            if (number == 1)
            {
                QuestionViewModel.A1 = new Answer(CurrentQuestion.A1);
                points = CurrentQuestion.A1.Points;
            }
            if (number == 2)
            {
                QuestionViewModel.A2 = new Answer(CurrentQuestion.A2);
                points = CurrentQuestion.A2.Points;
            }
            if (number == 3)
            {
                QuestionViewModel.A3 = new Answer(CurrentQuestion.A3);
                points = CurrentQuestion.A3.Points;
            }
            if (number == 4)
            {
                QuestionViewModel.A4 = new Answer(CurrentQuestion.A4);
                points = CurrentQuestion.A4.Points;
            }
            if (number == 5)
            {
                QuestionViewModel.A5 = new Answer(CurrentQuestion.A5);
                points = CurrentQuestion.A5.Points;
            }
            if (number == 6)
            {
                QuestionViewModel.A6 = new Answer(CurrentQuestion.A6);
                points = CurrentQuestion.A6.Points;
            }

            ThisRoundPoints = ThisRoundPoints + (points * CurrentQuestion.Multiplier);
            PlaySound(GameSound.Reveal);
        }

        public void WrongAnswer()
        {
            WrongAnswers = ("X " + WrongAnswers).TrimEnd();
            PlaySound(GameSound.Wrong);
        }

        public void ClearWrongAnswers()
        {
            WrongAnswers = "";
        }

        public void WinThisRound(int player)
        {
            if (player == 1)
                Player1Points += ThisRoundPoints;
            if (player == 2)
                Player2Points += ThisRoundPoints;
            ThisRoundPoints = 0;
        }

        public void PlaySound(GameSound sound)
        {
            _soundPlayer.SoundLocation = "Resources\\" + sound.ToString() + ".wav";
            _soundPlayer.Play();
        }

        #endregion

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
