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

        public Player(string name, int index, PlayerSymbol playerSymbol)
        {
            Name = name;
            Index = index;
            PlayerSymbol = playerSymbol;
            Score = 0;
        }

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
    }
}
