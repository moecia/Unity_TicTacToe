using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TicTactoe
{
    /// <summary>
    /// Store all UI elements and interactions.
    /// </summary>
    public class UI_Manager : MonoBehaviour
    {
        public static UI_Manager Instance { get; private set; }

        public Text[] playerScores;
       
        public GameObject gameOverMenu;
        // Winner nofity will be shown when one-side win the game.
        public Text winnerNofity;
        // An indacator as a reminder that tells player who should go now.
        public GameObject[] indicator;
        public void Init()
        {
            if (Instance != null)
                return;
            Instance = this;
        }

        public void GameOverText(string winnerName = "")
        {
            if (winnerName == "")
                winnerNofity.text = "Draw!";
            else
                winnerNofity.text = winnerName + " win this turn!";
        }

        public void UpdateScore(Player[] players)
        {
            for (int i = 0; i < players.Length; i++)
                playerScores[i].text = players[i].Score.ToString();
        }

        public void FlipIndicator(Player player)
        {
            if (player.Index == 0)
            {
                indicator[0].SetActive(true);
                indicator[1].SetActive(false);
            }
            else
            {
                indicator[0].SetActive(false);
                indicator[1].SetActive(true);
            }
        }
    }
}
