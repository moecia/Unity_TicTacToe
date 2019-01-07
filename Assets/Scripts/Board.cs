using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TicTactoe
{
    [RequireComponent(typeof(GridLayoutGroup))]
    [RequireComponent(typeof(RectTransform))]
    public class Board : MonoBehaviour
    {
        public static Board Instance { get; private set; }

        public int ItemToWin { get; private set; }

        public GameObject cell_prefab;
        private Cell[,] cells;

        private Player currentPlayer;
        private Cell lastChangedCell;
        private event Action<Player, Cell> onBoardChange;

        public void Init()
        {
            if (Instance != null)
                return;
            Instance = this;

            ItemToWin = 3;

            print(DateTime.Now.ToString() + "： Board instance initialization complete.");
        }

        public static void BuildBoard(int row, int col, Action<Player, Cell> onBoardChange)
        {
            Instance.onBoardChange = onBoardChange;

            Vector2 panelSize = Instance.GetComponent<RectTransform>().sizeDelta;
            Instance.GetComponent<GridLayoutGroup>().cellSize = new Vector2(panelSize.x / row, panelSize.x / col);

            Instance.cells = new Cell[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    GameObject tmp = Instantiate(Instance.cell_prefab);
                    tmp.transform.SetParent(Instance.transform);
                    tmp.transform.localScale = new Vector3(1, 1, 1);

                    Cell cell = tmp.GetComponent<Cell>();
                    cell.Init(i, j, Instance.OnCellClick);
                    Instance.cells[i, j] = cell;
                }
            }            
            print(DateTime.Now.ToString() + "： Board generation complete.");
        }

        public static void ResetBoard()
        {
            foreach (Cell cell in Instance.cells)
            {
                cell.Clear();
            }
            print(DateTime.Now.ToString() + "： Board Reset complete.");
        }

        public static void SetPlayer(Player player)
        {
            Instance.currentPlayer = player;
        }

        public static Player WinCheck(Cell cell)
        {
            if (Instance.CheckRow(cell.Row, cell.Col) || Instance.CheckCol(cell.Row, cell.Col) || Instance.CheckDiag(cell.Row, cell.Col))
                return cell.OccupiedPlayer;
            return null;
        }

        private void OnCellClick(Cell cell)
        {
            // Debug.Log(currentPlayer.Name);          
            if (Instance.currentPlayer != null && cell.OccupiedPlayer.Index == -1)
            {
                cell.Set(Instance.currentPlayer);               
                Instance.lastChangedCell = cell;
                if (Instance.onBoardChange != null)
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
                if (Instance.cells[i, col].OccupiedPlayer.PlayerSymbol == currentPlayer.PlayerSymbol)
                    counter++;
                else
                    break;
            }
            for (int i = row + 1; i < cells.GetLength(0); i++)
            {
                if (Instance.cells[i, col].OccupiedPlayer.PlayerSymbol == currentPlayer.PlayerSymbol)
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
                if (Instance.cells[row, i].OccupiedPlayer.PlayerSymbol == currentPlayer.PlayerSymbol)
                    counter++;
                else
                    break;
            }
            for (int i = col + 1; i < cells.GetLength(0); i++)
            {
                if (Instance.cells[row, i].OccupiedPlayer.PlayerSymbol == currentPlayer.PlayerSymbol)
                    counter++;
                else
                    break;
            }

            //Debug.Log(string.Format("Round {0}, Player {1} has {2} items in row {3}.", GameManager.Instance.Round, currentPlayer.Index, counter, row));
            return counter >= ItemToWin ? true : false;
        }

        private bool CheckDiag(int row, int col)
        {
            int counter = 1;

            int i = row - 1, j = col - 1;
            while (i >= 0 && j >= 0)
            {
                if (Instance.cells[i--, j--].OccupiedPlayer.PlayerSymbol == currentPlayer.PlayerSymbol)
                    counter++;
                else
                    break;
            }
            i = row + 1; j = col + 1;
            while (i < cells.GetLength(0) && j < cells.GetLength(1))
            {
                if (Instance.cells[i++, j++].OccupiedPlayer.PlayerSymbol == currentPlayer.PlayerSymbol)
                    counter++;
                else
                    break;
            }
            i = row - 1; j = col + 1;
            while (i >= 0 && j < cells.GetLength(1))
            {
                if (Instance.cells[i--, j++].OccupiedPlayer.PlayerSymbol == currentPlayer.PlayerSymbol)
                    counter++;
                else
                    break;
            }
            i = row + 1; j = col - 1;
            while (i < cells.GetLength(0) && j >= 0)
            {
                if (Instance.cells[i++, j--].OccupiedPlayer.PlayerSymbol == currentPlayer.PlayerSymbol)
                    counter++;
                else
                    break;
            }

            Debug.Log(string.Format("Round {0}, Player {1} has {2} items in the diagonal of row {3} col {4}.", GameManager.Instance.Round, currentPlayer.Index, counter, row, col));
            return counter >= ItemToWin ? true : false;
        }       
    }
}
