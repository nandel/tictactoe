using System;

namespace TicTacToe.Domain.ValueObjects
{
    public class Position
    {
        public byte X { get; private set; }
        public byte Y { get; private set; }

        public Position(byte x, byte y)
        {
            if (!IsValidPosition(x, y))
            {
                throw new InvalidOperationException("Posionamento invalido");
            }

            X = x;
            Y = y;
        }

        private static bool IsValidPosition(byte x, byte y)
        {
            return x >= 0 && x <= 2 && y >= 0 && y <= 2;
        }
    }
}
