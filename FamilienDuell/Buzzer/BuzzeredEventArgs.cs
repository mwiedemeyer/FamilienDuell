using System;
namespace FamilienDuell
{
    public class BuzzeredEventArgs
    {
        public int Player { get; private set; }

        internal BuzzeredEventArgs(int player)
        {
            Player = player;
        }
    }
}