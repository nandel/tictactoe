using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.ValueObjects;
using Xunit;

namespace TicTacToe.Domain.Tests.Entities
{
    public class GameTests
    {
        [Theory( DisplayName = "Primeiro jogador deve ganhar com movimentos horizontais" )]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public static void First_player_should_win_with_horizontal_moves(byte row)
        {
            // arrange
            var oponentRow = (byte) ((row + 1) % 3); // o outro jogador vai tentar na proxima linha
            var game = new Game();

            // Act
            for(byte i = 0; i <= 2; i++)
            {
                game.MakeAMove(game.PlayerTurn, row, i);

                // A jogada do player inicial esperava-se ter ganhado a partida
                if (i != 2)
                {
                    game.MakeAMove(game.PlayerTurn, oponentRow, i);
                }
            }

            // Assert
            Assert.True(game.IsFinish);
            Assert.Equal(game.Winner, game.FirstPlayer);            
            Assert.Equal(5, game.Movements.Count);
        }

        [Theory( DisplayName = "Segundo jogador deve ganhar com movimentos verticais" )]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public static void Second_player_should_win_with_vertical_moves(byte col)
        {
            // arrange 
            var game = new Game();

            // Act
            for (byte i = 0; i <= 2; i++)
            {
                game.MakeAMove(game.PlayerTurn, i, i == col ? (byte)((i + 1) % 3) : i);
                game.MakeAMove(game.PlayerTurn, i, col);
            }

            // Assert
            Assert.True(game.IsFinish);
            Assert.True(game.Winner != game.FirstPlayer);
            Assert.Equal(6, game.Movements.Count);
        }

        [Fact( DisplayName = "Deve ganhar com uma vitória diagonal /" )]
        public static void Should_win_with_diagonal1()
        {
            // arrange
            var game = new Game();

            // act
            game.MakeAMove(game.PlayerTurn, 0, 0);
            game.MakeAMove(game.PlayerTurn, 0, 1);
            game.MakeAMove(game.PlayerTurn, 1, 1);
            game.MakeAMove(game.PlayerTurn, 1, 2);
            game.MakeAMove(game.PlayerTurn, 2, 2);

            // assert
            Assert.True(game.IsFinish);
            Assert.Equal(game.FirstPlayer, game.Winner);
        }

        [Fact( DisplayName = "Deve ganhar com uma vitória na diagonal \\" )]
        public static void Should_with_diagonal_2()
        {
            // arrange
            var game = new Game();

            // act
            game.MakeAMove(game.PlayerTurn, 0, 2);
            game.MakeAMove(game.PlayerTurn, 0, 1);
            game.MakeAMove(game.PlayerTurn, 1, 1);
            game.MakeAMove(game.PlayerTurn, 1, 0);
            game.MakeAMove(game.PlayerTurn, 2, 0);

            // assert
            Assert.True(game.IsFinish);
            Assert.Equal(game.FirstPlayer, game.Winner);
        }

        [Fact( DisplayName = "Deve terminar em impate" )]
        public static void Should_end_on_a_draw()
        {
            // arrange
            var game = new Game();

            // act
            game.MakeAMove(game.PlayerTurn, 0, 0);
            game.MakeAMove(game.PlayerTurn, 1, 0);
            game.MakeAMove(game.PlayerTurn, 0, 1);
            game.MakeAMove(game.PlayerTurn, 1, 1);
            game.MakeAMove(game.PlayerTurn, 1, 2); // bloqueio horizontal
            game.MakeAMove(game.PlayerTurn, 0, 2); // bloqueio horizontal
            game.MakeAMove(game.PlayerTurn, 2, 0);
            game.MakeAMove(game.PlayerTurn, 2, 1);
            game.MakeAMove(game.PlayerTurn, 2, 2);

            // assert
            Assert.True(game.IsFinish);
            Assert.Null(game.Winner);
        }
    }
}
