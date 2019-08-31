using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Dtos
{
    public class CreateMovementDto
    {
        public Player Player { get; set; }
        public PositionDto Position { get; set; }
    }
}
