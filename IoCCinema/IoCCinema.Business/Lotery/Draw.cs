using System;

namespace IoCCinema.Business.Lotery
{
    /// <summary>
    /// Represent one number that been randomly choosen from 100 available
    /// </summary>
    public class Draw
    {
        public int Value { get; private set; }

        public Draw(int drawedNumber)
        {
            if (drawedNumber < 0 || drawedNumber > 100)
            {
                throw new ArgumentException("drawed number should be between 0 and 100");
            }

            Value = drawedNumber;
        }
    }
}
