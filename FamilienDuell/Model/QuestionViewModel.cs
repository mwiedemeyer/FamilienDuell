using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilienDuell
{
    public class QuestionViewModel : INotifyPropertyChanged
    {
        private string _questionText;
        private Answer _a1 = new Answer();
        private Answer _a2 = new Answer();
        private Answer _a3 = new Answer();
        private Answer _a4 = new Answer();
        private Answer _a5 = new Answer();
        private Answer _a6 = new Answer();

        public QuestionViewModel(int answers)
        {
            if (answers > 0)
                A1 = Answer.CreateEmpty();
            if (answers > 1)
                A2 = Answer.CreateEmpty();
            if (answers > 2)
                A3 = Answer.CreateEmpty();
            if (answers > 3)
                A4 = Answer.CreateEmpty();
            if (answers > 4)
                A5 = Answer.CreateEmpty();
            if (answers > 5)
                A6 = Answer.CreateEmpty();
        }

        public string QuestionText
        {
            get { return _questionText; }
            set
            {
                if (value != _questionText)
                {
                    _questionText = value;
                    NotifyPropertyChanged("QuestionText");
                }
            }
        }

        public Answer A1
        {
            get { return _a1; }
            set
            {
                if (value != _a1)
                {
                    _a1 = value;
                    NotifyPropertyChanged("A1");
                }
            }
        }

        public Answer A2
        {
            get { return _a2; }
            set
            {
                if (value != _a2)
                {
                    _a2 = value;
                    NotifyPropertyChanged("A2");
                }
            }
        }

        public Answer A3
        {
            get { return _a3; }
            set
            {
                if (value != _a3)
                {
                    _a3 = value;
                    NotifyPropertyChanged("A3");
                }
            }
        }

        public Answer A4
        {
            get { return _a4; }
            set
            {
                if (value != _a4)
                {
                    _a4 = value;
                    NotifyPropertyChanged("A4");
                }
            }
        }

        public Answer A5
        {
            get { return _a5; }
            set
            {
                if (value != _a5)
                {
                    _a5 = value;
                    NotifyPropertyChanged("A5");
                }
            }
        }

        public Answer A6
        {
            get { return _a6; }
            set
            {
                if (value != _a6)
                {
                    _a6 = value;
                    NotifyPropertyChanged("A6");
                }
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
