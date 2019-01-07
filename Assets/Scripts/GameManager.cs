using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTactoe
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public Board board;
        public UI_Manager uiManager;

        public Player[] Players { get; private set; }

        public int Round { get; private set; }
        void Start()
        {
            if (Instance != null)
                return;
            Instance = this;

            board.Init();
            Board.BuildBoard(3, 3, Instance.OnBoardChange);

            uiManager.Init();

            Instance.Players = new Player[2];
            Instance.Players[0] = new Player("Player_0", 0, PlayerSymbol.Chip);
            Instance.Players[1] = new Player("Player_1", 1, PlayerSymbol.Club);

            NewGame();
        }

        public static void NewGame()
        {
            Instance.Round = 0;
            Board.SetPlayer(Instance.Players[0]);
            Board.ResetBoard();                       
        }

        private void OnBoardChange(Player player, Cell cell)
        {
            Round++;

            Board.SetPlayer(player);
            Player winner = Board.WinCheck(cell);

            if (winner != null)
            {
                winner.IncrementScore();
                uiManager.playerScores[0].text = winner.Score.ToString();
                NewGame();
            }
            else
            {
                int nextIndex = player.Index;
                nextIndex = player.Index < Players.Length - 1 ? nextIndex += 1 : 0;
                Player next = Players[nextIndex];
                Board.SetPlayer(next);
            }
        }
    }
}
