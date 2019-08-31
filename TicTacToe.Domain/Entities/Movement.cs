using System;
using TicTacToe.Domain.ValueObjects;

namespace TicTacToe.Domain.Entities
{
    public class Movement
    {
        public Guid Id { get; private set; }
        public Player Player { get; private set; }
        public Position Position { get; private set; }

        public Movement(Player player, Position pos)
        {
            Id = Guid.NewGuid();
            Player = player;
            Position = pos;
        }


        public override string ToString()
        {
            return $"{Player} {Position.X} {Position.Y}";
        }
    }
}
