using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilienDuell
{
    public class Answer
    {
        public Answer() { }
        public Answer(Answer fromExisting)
        {
            if (!fromExisting.HasAnswer)
                return;
            Text = PadRight(fromExisting.Text);
            Points = fromExisting.Points;
            PointsLabel = fromExisting.Points.ToString();
        }

        private string PadRight(string text)
        {
            if (text.Length >= 39)
                return text;

            var rem = 39 - text.Length;
            for (int i = 0; i < rem; i++)
            {
                text += "_";
            }
            return text + " ";
        }

        public string Text { get; set; }
        public int Points { get; set; }

        public string PointsLabel { get; set; }

        public bool HasAnswer { get { return !string.IsNullOrEmpty(Text); } }

        public static Answer CreateEmpty()
        {
            return new Answer()
            {
                Text = "_______________________________________ ",
                PointsLabel = "--"
            };
        }
    }
}
