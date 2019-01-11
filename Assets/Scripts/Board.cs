using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TicTactoe
{
    /// <summary>
    /// Board store all cells informations, providing methods for build/ delete/ reset cells' data and decides is a winner exist for every round.
    /// </summary>
    [RequireComponent(typeof(GridLayoutGroup))]
    [RequireComponent(typeof(RectTransform))]  
    public class Board : MonoBehaviour
    {
        public static Board Instance { get; private set; }
        // How many chess in a line that player can win the game.
        public int ItemToWin { get; private set; }

        public GameObject cell_prefab;

        public Cell[,] Cells { get; private set; }

        private Player currentPlayer;
        private event Action<Player, Cell> onBoardChange;

        public void Init()
        {
            if (Instance != null)
                return;
            Instance = this;

            ItemToWin = 3;
            print(DateTime.Now.ToString() + "： Board instance initialization complete.");
        }

        /// <summary>
        /// Build a row * col size board.
        /// </summary>
        /// <param name="row">How many rows of the board in total</param>
        /// <param name="col">How many columns of the board in total</param>
        /// <param name="onBoardChange">OnBoardChange action delegate</param>
        public void BuildCells(int row, int col, Action<Player, Cell> onBoardChange)
        {
            this.onBoardChange = onBoardChange;

            Vector2 panelSize = GetComponent<RectTransform>().sizeDelta;
            GetComponent<GridLayoutGroup>().cellSize = new Vector2(panelSize.x / row, panelSize.x / col);

            Cells = new Cell[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    GameObject tmp = Instantiate(cell_prefab);
                    tmp.transform.SetParent(transform);
                    tmp.transform.localScale = new Vector3(1, 1, 1);

                    Cell cell = tmp.GetComponent<Cell>();
                    cell.Init(i, j, OnCellClick);
                    Cells[i, j] = cell;
                }
            }            
            print(DateTime.Now.ToString() + "： Board generation complete.");
        }

        // Delete all cell objects.
        public void DeleteCells()
        {
            foreach (Cell cell in Cells)
            {
                Destroy(cell.transform.gameObject);
            }
            print(DateTime.Now.ToString() + "： Board Delete complete.");
        }

        // Delete all cell data, but it won't destory cell objects.
        public void ResetCells()
        {
            foreach (Cell cell in Cells)
            {
                cell.Clear();
            }
            print(DateTime.Now.ToString() + "： Board Reset complete.");
        }

        public void SetPlayer(Player player)
        {
            currentPlayer = player;
            UI_Manager.Instance.FlipIndicator(currentPlayer);
        }

        public Player WinCheck(Cell cell)
        {
            if (CheckRow(cell.Row, cell.Col) || CheckCol(cell.Row, cell.Col) || CheckDiag(cell.Row, cell.Col))
                return cell.OccupiedPlayer;
            return null;
        }

        private void OnCellClick(Cell cell)
        {
            // Debug.Log(currentPlayer.Name);          
            if (currentPlayer != null && cell.OccupiedPlayer.Index == -1)
            {
                cell.Set(currentPlayer);
                if (onBoardChange != null)
                {
                   onBoardChange(currentPlayer, cell);
                }             
            }          
        }

        private bool CheckCol(int row, int col)
        {
            int counter = 1;
            for (int i = row - 1; i >= 0; i--)
            {
                if (Cells[i, col].OccupiedPlayer.PlayerSymbol == currentPlayer.PlayerSymbol)
                    counter++;
                else
                    break;
            }
            for (int i = row + 1; i < Cells.GetLength(0); i++)
            {
                if (Cells[i, col].OccupiedPlayer.PlayerSymbol == currentPlayer.PlayerSymbol)
                    counter++;
                else
                    break;
            }

            //Debug.Log(string.Format("Round {0}, Player {1} has {2} items in column {3}.", GameManager.Instance.Round, currentPlayer.Index, counter, col));
            return counter >= ItemToWin ? true : false;
        }

        private bool CheckRow(int row, int col)
        {
            int counter = 1;
            for (int i = col - 1; i >= 0; i--)
            {
                if (Cells[row, i].OccupiedPlayer.PlayerSymbol == currentPlayer.PlayerSymbol)
                    counter++;
                else
                    break;
            }
            for (int i = col + 1; i < Cells.GetLength(0); i++)
            {
                if (Cells[row, i].OccupiedPlayer.PlayerSymbol == currentPlayer.PlayerSymbol)
                    counter++;
                else
                    break;
            }

            //Debug.Log(string.Format("Round {0}, Player {1} has {2} items in row {3}.", GameManager.Instance.Round, currentPlayer.Index, counter, row));
            return counter >= ItemToWin ? true : false;
        }

        private bool CheckDiag(int row, int col)
        {
            int counter_0 = 1;

            int i = row - 1, j = col - 1;
            while (i >= 0 && j >= 0)
            {
                if (Cells[i--, j--].OccupiedPlayer.PlayerSymbol == currentPlayer.PlayerSymbol)
                    counter_0++;
                else
                    break;
            }
            i = row + 1; j = col + 1;
            while (i < Cells.GetLength(0) && j < Cells.GetLength(1))
            {
                if (Cells[i++, j++].OccupiedPlayer.PlayerSymbol == currentPlayer.PlayerSymbol)
                    counter_0++;
                else
                    break;
            }
            // Debug.Log(string.Format("Round {0}, Player {1} has {2} items in the right diagonal of row {3} col {4}.", GameManager.Instance.Round, currentPlayer.Index, counter_0, row, col));
            int counter_1 = 1;
            i = row - 1; j = col + 1;
            while (i >= 0 && j < Cells.GetLength(1))
            {
                if (Cells[i--, j++].OccupiedPlayer.PlayerSymbol == currentPlayer.PlayerSymbol)
                    counter_1++;
                else
                    break;
            }
            i = row + 1; j = col - 1;
            while (i < Cells.GetLength(0) && j >= 0)
            {
                if (Cells[i++, j--].OccupiedPlayer.PlayerSymbol == currentPlayer.PlayerSymbol)
                    counter_1++;
                else
                    break;
            }

            //Debug.Log(string.Format("Round {0}, Player {1} has {2} items in the left diagonal of row {3} col {4}." , GameManager.Instance.Round, currentPlayer.Index, counter_1, row, col));
            return (counter_0 >= ItemToWin || counter_1 >= ItemToWin) ? true : false;
        }       
    }
}
