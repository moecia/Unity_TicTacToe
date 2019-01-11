using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TicTactoe
{
    /// <summary>
    /// Cell is an object holding occupied player information, row and column.
    /// </summary>
    public class Cell : MonoBehaviour, IPointerClickHandler
    {
        public GameObject spriteChip;
        public GameObject spriteClub;

        public int Row { get; private set; }
        public int Col { get; private set; }

        public Player OccupiedPlayer { get; private set; }

        private event Action<Cell> onCellClick;

        /// <summary>
        /// Initialize a cell object
        /// </summary>
        /// <param name="row">Indicate which row of the board it is on.</param>
        /// <param name="col">Indicate which column of the board it is on.</param>
        /// <param name="onCellClick">OnCellClick action delegate</param>
        public void Init(int row, int col, Action<Cell> onCellClick)
        {
            Row = row;
            Col = col;

            name = string.Format("Cell [{0}, {1}]", row, col);
            this.onCellClick = onCellClick;
        }

        /// <summary>
        /// Set the ocupied player of the cell.
        /// </summary>
        /// <param name="occupiedPlayer"></param>
        public void Set(Player occupiedPlayer)
        {
            // Point to current player.
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

        /// <summary>
        /// Delete cell data and reset cell image.
        /// </summary>
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
