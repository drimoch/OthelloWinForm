using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OthelloLogic;

namespace OthelloGUI
{
    public partial class GameBoard : Form
    {
        private string k_Player1 = "Red";
        private string k_Player2 = "Yellow";
        private int r_BoardSize;
        private int r_NumOfPlayers;
        private GameManager m_Manager;
        private List<VisualCell> m_VisualCells;

        public GameBoard()
        {
            InitializeComponent();
        }

        public GameBoard(int i_BoardSize, int i_NumOfPlayers)
        {
            r_BoardSize = i_BoardSize;
            r_NumOfPlayers = i_NumOfPlayers;
            m_VisualCells = new List<VisualCell>();
            InitializeComponent();
            startGame();
        }

        private void startGame()
        {
            m_Manager = new GameManager(r_NumOfPlayers, r_BoardSize);
            setTable();
            paintTable();
        }

        private void setTable()
        {
            tableLayoutPanelGame.ColumnCount = r_BoardSize;
            tableLayoutPanelGame.RowCount = r_BoardSize;
            tableLayoutPanelGame.ColumnStyles.Clear();
            tableLayoutPanelGame.RowStyles.Clear();

            for (int i = 0; i < r_BoardSize; i++)
            {
                tableLayoutPanelGame.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, tableLayoutPanelGame.Size.Width / r_BoardSize));
                tableLayoutPanelGame.RowStyles.Add(new RowStyle(SizeType.Percent, tableLayoutPanelGame.Size.Height / r_BoardSize));
            }
        }

        private void paintTable()
        {
            List<Cell> validCells = m_Manager.GetValidCells();
            foreach (Cell cell in m_Manager.Board)
            {
                VisualCell newCell = new VisualCell(cell);
                m_VisualCells.Add(newCell);
                if (cell.CellType != Cell.eType.Empty)
                {
                    PictureBox cellPicture = newCell.SetImage(cell, tableLayoutPanelGame.Size.Height / r_BoardSize, tableLayoutPanelGame.Size.Width / r_BoardSize);
                    tableLayoutPanelGame.Controls.Add(cellPicture, cell.CellLocation.X, cell.CellLocation.Y);
                }
                else
                {
                    if (validCells.Contains(cell))
                    {
                        PictureBox cellPicture = newCell.SetEmptyCell(pictureBox_Click);
                        tableLayoutPanelGame.Controls.Add(cellPicture, cell.CellLocation.X, cell.CellLocation.Y);
                    }
                }
            }
        }

        public void RefreshBoard()
        {
            List<Cell> validCells = m_Manager.GetValidCells();

            foreach (VisualCell visualCell in m_VisualCells)
            {
                if (visualCell.LogicCell.CellType == Cell.eType.Empty)
                {
                    if (validCells.Contains(visualCell.LogicCell))
                    {
                        tableLayoutPanelGame.Controls.Remove(visualCell.Image);
                        PictureBox cellPicture = visualCell.SetEmptyCell(pictureBox_Click);
                        tableLayoutPanelGame.Controls.Add(cellPicture, visualCell.LogicCell.CellLocation.X, visualCell.LogicCell.CellLocation.Y);
                    }
                    else if (visualCell.Image != null)
                    {
                        visualCell.Image.MouseClick -= pictureBox_Click;
                        tableLayoutPanelGame.Controls.Remove(visualCell.Image);
                    }
                }
                else
                {
                    PictureBox cellPicture = visualCell.SetImage(visualCell.LogicCell, tableLayoutPanelGame.Size.Height / r_BoardSize, tableLayoutPanelGame.Size.Width / r_BoardSize);
                }

            }
        }

        private void pictureBox_Click(object sender, MouseEventArgs e)
        {
            int y = tableLayoutPanelGame.GetRow((PictureBox)sender);
            int x = tableLayoutPanelGame.GetColumn((PictureBox)sender);
            GameManager.eResponseCode response = m_Manager.PlayTurn(x, y);
            if (response != GameManager.eResponseCode.OK)
            {
                getErrorMessage(response);
            }
            else
            {

                m_Manager.ChangeCurrentPlayer();

                RefreshBoard();
                //printBoard after play with RefreshBoard
            }
        }

        private void getErrorMessage(GameManager.eResponseCode i_Error)
        {
            string message;
            bool endOfGame = false;

            switch (i_Error)
            {
                case GameManager.eResponseCode.CellIsInvalid:
                    message = "The chosen cell is invalid, please choose another cell";
                    break;
                case GameManager.eResponseCode.NoValidCellsForPlayer:
                    message = "You have no valid cells. The turn will be moved to the second player";
                    break;
                case GameManager.eResponseCode.NoValidCellsForBothPlayers:
                    message = endOfGameMessage();
                    endOfGame = true;
                    break;
                case GameManager.eResponseCode.InvalidMove:
                    message = "The cell you chose doesn't block the competitor's coins, please try again";
                    break;
                case GameManager.eResponseCode.NotEmpty:
                    message = "The chosen cell is not empty, please try again";
                    break;
                case GameManager.eResponseCode.OutOfRange:
                    message = "The chosen cell is out of range, please try again";
                    break;
                default:
                    message = string.Empty;
                    break;
            }

            if (message != string.Empty)
            {
                displayMessageBox(message, endOfGame);
            }
        }

        private void displayMessageBox(string i_Message, bool i_EndOfGame)
        {
            string caption = "Othello";
            MessageBoxButtons buttons;

            if (!i_EndOfGame)
            {
                buttons = MessageBoxButtons.OK;
                MessageBox.Show(i_Message, caption, buttons);
            }
            else
            {
                buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                result = MessageBox.Show(i_Message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    this.Close();
                }
                else
                {
                    m_Manager.InitiateGame(r_NumOfPlayers, r_BoardSize);
                }
            }
        }

        private string endOfGameMessage()
        {
            string winner = m_Manager.Winner.PlayerID == Player.ePlayerID.Player1 ? k_Player1 : k_Player2;
            string message = string.Format("{0} Won! ({1}/{2}) ({3}/{4}){5}Would you like a another round?",
                winner, m_Manager.Winner.Score, m_Manager.Losser.Score, m_Manager.Player1.RoundsWinner, m_Manager.Player2.RoundsWinner, Environment.NewLine);

            return message;
        }
    }
}
