using System;
using System.Collections.Generic;

namespace FamilienDuell
{
    public class Question
    {
        public Question()
        {
            Text = string.Empty;
            Multiplier = 1;
            A1 = new Answer();
            A2 = new Answer();
            A3 = new Answer();
            A4 = new Answer();
            A5 = new Answer();
            A6 = new Answer();
        }

        public string Text { get; set; }
        public int Multiplier { get; set; }
        public Answer A1 { get; set; }
        public Answer A2 { get; set; }
        public Answer A3 { get; set; }
        public Answer A4 { get; set; }
        public Answer A5 { get; set; }
        public Answer A6 { get; set; }

        public int AvailableAnswers
        {
            get
            {
                if (A2 == null || string.IsNullOrEmpty(A2.Text))
                    return 1;
                if (A3 == null || string.IsNullOrEmpty(A3.Text))
                    return 2;
                if (A4 == null || string.IsNullOrEmpty(A4.Text))
                    return 3;
                if (A5 == null || string.IsNullOrEmpty(A5.Text))
                    return 4;
                if (A6 == null || string.IsNullOrEmpty(A6.Text))
                    return 5;
                return 6;
            }
        }
    }

    public class QuestionContext
    {
        public List<Question> Questions { get; set; }
    }
}