using System;

namespace IoCCinema.Business.Lotery
{
    /// <summary>
    /// Represents win chance defined as percentage of wining
    /// </summary>
    public class WinChance
    {
        public int PercentageOfWin { get; private set; }

        public WinChance(int percentageOfWin)
        {
            if (percentageOfWin < 0 || percentageOfWin > 100)
            {
                throw new ArgumentException("drawed number should be between 0 and 100");
            }

            PercentageOfWin = percentageOfWin;
        }

        internal bool IsSatisfiedBy(Draw draw)
        {
            return draw.Value < PercentageOfWin;
        }
    }
}
