using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTactoe
{
    /// <summary>
    /// GameManager in charge of the gameloop. 
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public Player[] Players { get; private set; }
        // Initilize in editor
        public String[] playersName;

        public int Round { get; private set; }

        public void Init()
        {
            if (Instance != null)
                return;
            Instance = this;

            SetPlayer();
        }

        public void SetPlayer()
        {
            Players = new Player[2];
            Players[0] = new Player(playersName[0], 0, PlayerSymbol.Chip);
            Players[1] = new Player(playersName[1], 1, PlayerSymbol.Club);
            Debug.Log("ori: " + Players[0].GetHashCode());
            Debug.Log("ori: " + Players[1].GetHashCode());
        }

        #region Button Calls
        public void NewGame()
        {
            Round = 0;
            Recorder.Instance.Clear();
            int goFirst = UnityEngine.Random.Range(0, 2);
            Board.Instance.SetPlayer(Instance.Players[goFirst]);
            Board.Instance.ResetCells();
        }

        public void SetSize(int size)
        {
            if (Board.Instance.transform.childCount != 0)
                Board.Instance.DeleteCells();
            Board.Instance.BuildCells(size, size, OnBoardChange);
            NewGame();           
        }

        public void ClearScore()
        {
            foreach (Player player in Players)
                player.ResetScore();
            UI_Manager.Instance.UpdateScore(Players);
        }

        public void QuitGame()
        {
            if (Application.isEditor)
                UnityEditor.EditorApplication.isPlaying = false;
            else
                Application.Quit();
        }
        #endregion Button Calls

        private void OnBoardChange(Player player, Cell cell)
        {
            Round++;
            // Check whether winner exist
            Board.Instance.SetPlayer(player);
            Player winner = Board.Instance.WinCheck(cell);
            
            Recorder.Instance.SetStep(Round, player, cell.Row, cell.Col);

            if (winner != null)
            {               
                winner.IncrementScore();
                UI_Manager.Instance.UpdateScore(Players);
                UI_Manager.Instance.gameOverMenu.SetActive(true);
                UI_Manager.Instance.GameOverText(player.Name);
            }
            else
            {
                if (Round == Board.Instance.Cells.Length)
                {
                    UI_Manager.Instance.gameOverMenu.SetActive(true);
                    UI_Manager.Instance.GameOverText();
                    return;
                }              
                // Turn to next player
                int nextIndex = player.Index;
                nextIndex = player.Index < Players.Length - 1 ? nextIndex += 1 : 0;
                Player next = Players[nextIndex];
                Board.Instance.SetPlayer(next);
            }
        }
    }
}
