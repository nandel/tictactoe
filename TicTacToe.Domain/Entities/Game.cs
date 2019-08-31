using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Domain.ValueObjects;

namespace TicTacToe.Domain.Entities
{
    public class Game
    {
        public Guid Id { get; private set; }
        public Player FirstPlayer { get; private set; }
        public IList<Movement> Movements { get; private set; }
        public Player PlayerTurn { get; private set; }
        public Player? Winner { get; private set; }
        public bool IsFinish => Winner.HasValue || Movements.Count == 9;

        public Game(Player? firstPlayer = null)
        {
            if (!firstPlayer.HasValue)
            {
                var random = new Random();
                firstPlayer = random.Next() % 2 == 0 ? Player.X : Player.O;
            }

            Id = Guid.NewGuid();
            FirstPlayer = firstPlayer.Value;
            PlayerTurn = firstPlayer.Value;
            Movements = new List<Movement>();
        }

        public Movement MakeAMove(Player player, byte x, byte y)
        {
            // verifica se o jogo ja acabou
            if (IsFinish)
            {
                throw new InvalidOperationException("Partida finalizada");
            }

            // verifica se é a vez do jogador
            if (PlayerTurn != player)
            {
                throw new InvalidOperationException($"Não é o turno do jogador");
            }

            // verifica se posição esta disponivei
            if (Movements.Any(m => m.Position.X == x && m.Position.Y == y))
            {
                throw new InvalidOperationException("Posição já tomada anteriormente");
            }

            // Adiciona o movimento
            ToggleTurn();
            var move = new Movement(player, new Position(x, y));
            Movements.Add(move);

            // Tentamos pegar o vencedor
            Winner = GetWinner();

            return move;
        }

        private Player? GetWinner()
        {
            // Verificação horizontal
            for (var i = 0; i <= 2; i++)
            {
                var moves = Movements.Where(x => x.Position.X == i);
                if (IsVictorySet(moves))
                {
                    return moves.First().Player;
                }
            }

            // Verificação vertical
            for (var i = 0; i <= 2; i++)
            {
                var moves = Movements.Where(move => move.Position.Y == i).ToList();
                if (IsVictorySet(moves))
                {
                    return moves.First().Player;
                }
            }

            // Verificação diagonal 1 "/"
            var diagonalMoves1 = Movements.Where(move => move.Position.X == move.Position.Y).ToList();
            if (IsVictorySet(diagonalMoves1))
            {
                return diagonalMoves1.First().Player;
            }

            // verificação diagonal 2 "\"
            var diagonalMoves2 = Movements.Where(move => move.Position.X + move.Position.Y == 2);
            if (IsVictorySet(diagonalMoves2))
            {
                return diagonalMoves2.First().Player;
            }

            // Não foi encontrado o ganhador
            return null;
        }

        private void ToggleTurn()
        {
            if (PlayerTurn == Player.X)
            {
                PlayerTurn = Player.O;
            }
            else
            {
                PlayerTurn = Player.X;
            }
        }

        /// <summary>
        /// Verifica se no conjunto possui um vencedor.
        /// 
        /// O Conjunto deve então ter 3 jogadas e todas do mesmo jogador
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        private static bool IsVictorySet(IEnumerable<Movement> moves)
        {
            if (moves != null && moves.Count() == 3) // pode ser a verificação, porem, garante a execução em outros fluxos
            {
                var player = moves.First().Player;
                if (moves.Where(x => x.Player == player).Count() == 3)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
