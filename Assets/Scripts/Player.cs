using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTactoe
{
    public enum PlayerSymbol { Chip, Club, Empty };

    public class Player
    {
        public string Name { get; private set; }
        public int Index { get; private set; }
        public int Score { get; private set; }
        public PlayerSymbol PlayerSymbol { get; private set; }

        /// <summary>
        /// Player constructor
        /// </summary>
        /// <param name="name">Player's name, should be unique.</param>
        /// <param name="index">Player's name, should be unique.</param>
        /// <param name="playerSymbol">Player's symbol, should be unique.</param>
        public Player(string name, int index, PlayerSymbol playerSymbol)
        {
            Name = name;
            Index = index;
            PlayerSymbol = playerSymbol;
            Score = 0;
        }

        /// <summary>
        /// Player default constructor
        /// </summary>
        public Player()
        {
            Name = "No User";
            Index = -1;
            PlayerSymbol = PlayerSymbol.Empty;
            Score = 0;
        }

        public void IncrementScore()
        {
            Score += 1;
        }

        public void ResetScore()
        {
            Score = 0;
        }
    }
}
