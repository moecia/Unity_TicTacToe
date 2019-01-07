using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TicTactoe
{
    public class Cell : MonoBehaviour, IPointerClickHandler
    {
        public GameObject spriteChip;
        public GameObject spriteClub;

        public int Row { get; private set; }
        public int Col { get; private set; }

        public Player OccupiedPlayer { get; private set; }

        private event Action<Cell> onCellClick;

        public void Init(int row, int col, Action<Cell> onCellClick)
        {
            Row = row;
            Col = col;
            OccupiedPlayer = new Player();
            name = string.Format("Cell [{0}, {1}]", row, col);
            this.onCellClick = onCellClick;
        }

        public void Set(Player occupiedPlayer)
        {
            OccupiedPlayer = occupiedPlayer;

            switch (occupiedPlayer.PlayerSymbol)
            {
                case PlayerSymbol.Chip:
                    spriteChip.SetActive(true);
                    spriteClub.SetActive(false);
                    break;
                case PlayerSymbol.Club:
                    spriteChip.SetActive(false);
                    spriteClub.SetActive(true);
                    break;
            }
        }

        public void Clear()
        {
            OccupiedPlayer = new Player();
            spriteChip.SetActive(false);
            spriteClub.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (onCellClick != null)
            {
                onCellClick(this);
            }
        }

    }
}
