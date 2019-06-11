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
        private const string k_Player1 = "Red";
        private const string k_Player2 = "Yellow";
        private readonly int r_BoardSize;
        private readonly int r_NumOfPlayers;
        private GameManager m_Manager;
        private List<VisualCell> m_VisualCells;
        private bool m_UserEndGame = false;

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
            m_Manager = new GameManager();
            startNewGame();
        }

        private void startNewGame()
        {
            tableLayoutPanelGame.Visible = false;
            tableLayoutPanelGame.Controls.Clear();
            m_Manager.InitiateGame(r_NumOfPlayers, r_BoardSize);
            m_VisualCells.Clear();
            setTable();
            paintInitialTable();
            tableLayoutPanelGame.Visible = true;
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

        private void paintInitialTable()
        {
            List<Cell> validCells = m_Manager.GetValidCells();
            foreach (Cell cell in m_Manager.Board)
            {
                VisualCell newCell = new VisualCell(cell);
                m_VisualCells.Add(newCell);
                if (cell.CellType != Cell.eType.Empty)
                {
                    newCell.SetImage(cell, tableLayoutPanelGame.Size.Height / r_BoardSize, tableLayoutPanelGame.Size.Width / r_BoardSize);
                    tableLayoutPanelGame.Controls.Add(newCell.Image, cell.CellLocation.X, cell.CellLocation.Y);
                }
                else
                {
                    if (validCells.Contains(cell))
                    {
                        newCell.SetEmptyCell(pictureBox_Click);
                        tableLayoutPanelGame.Controls.Add(newCell.Image, cell.CellLocation.X, cell.CellLocation.Y);
                    }
                }
            }

            Text = string.Format("Othello - {0}'s Turn", m_Manager.CurrentPlayer == m_Manager.Player1 ? k_Player1 : k_Player2);
        }

        private void refreshBoard(GameManager.eResponseCode i_Response = GameManager.eResponseCode.OK)
        {
            if (i_Response != GameManager.eResponseCode.OK)
            {
                getErrorMessage(i_Response);
            }
            if (!m_UserEndGame)
            {
            printBoard();
            GameManager.eResponseCode response = isBoardValid();
            if (response != GameManager.eResponseCode.OK)
            {
                getErrorMessage(response);
            }

            Text = string.Format("Othello - {0}'s Turn", m_Manager.CurrentPlayer == m_Manager.Player1 ? k_Player1 : k_Player2);
            }

        }

        private void printBoard()
        {
            List<Cell> validCells = m_Manager.GetValidCells();
            tableLayoutPanelGame.Visible = false;

            foreach (VisualCell visualCell in m_VisualCells)
            {
                if (visualCell.LogicCell.CellType == Cell.eType.Empty)
                {
                    if (validCells.Contains(visualCell.LogicCell))
                    {
                        tableLayoutPanelGame.Controls.Remove(visualCell.Image);
                        visualCell.SetEmptyCell(pictureBox_Click);
                        tableLayoutPanelGame.Controls.Add(visualCell.Image, visualCell.LogicCell.CellLocation.X, visualCell.LogicCell.CellLocation.Y);
                    }
                    else if (visualCell.Image != null)
                    {
                        visualCell.Image.MouseClick -= pictureBox_Click;
                        tableLayoutPanelGame.Controls.Remove(visualCell.Image);
                    }
                }
                else
                {
                    visualCell.Image.MouseClick -= pictureBox_Click;
                    visualCell.SetImage(visualCell.LogicCell, tableLayoutPanelGame.Size.Height / r_BoardSize, tableLayoutPanelGame.Size.Width / r_BoardSize);
                    tableLayoutPanelGame.Controls.Add(visualCell.Image, visualCell.LogicCell.CellLocation.X, visualCell.LogicCell.CellLocation.Y);
                }
            }

            tableLayoutPanelGame.Visible = true;
        }

        private bool isBoardFull()
        {
            bool isFull = true;
            foreach(Cell cell in m_Manager.Board)
            {
                if(cell.CellType == Cell.eType.Empty)
                {
                    isFull = false;
                    break;
                }
            }

            return isFull;
        }

        private GameManager.eResponseCode isBoardValid()
        {
            return m_Manager.CheckValidCellsForBothPlayers();
        }

        private void pictureBox_Click(object sender, MouseEventArgs e)
        {
            PictureBox picBox = sender as PictureBox;
            int x = tableLayoutPanelGame.GetRow(picBox);
            int y = tableLayoutPanelGame.GetColumn(picBox);
            GameManager.eResponseCode reponse = m_Manager.PlayTurn(x, y);
            refreshBoard(reponse);
        }

        private void getErrorMessage(GameManager.eResponseCode i_Error)
        {
            string message;
            bool endOfGame = false;

            switch (i_Error)
            {
                case GameManager.eResponseCode.NoValidCellsForPlayer:
                    message = "You have no valid cells. The turn will be moved to the second player";
                    break;
                case GameManager.eResponseCode.NoValidCellsForBothPlayers:
                    message = endOfGameMessage();
                    endOfGame = true;
                    break;
                default:
                    message = string.Empty;
                    break;
            }

            if (message != string.Empty&&m_Manager.CurrentPlayer.PlayerID!= Player.ePlayerID.Computer)
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
                MessageBox.Show(i_Message, caption, buttons, MessageBoxIcon.Warning);
                refreshBoard();
            }
            else
            {
                buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(i_Message, caption, buttons, MessageBoxIcon.Information);
                if (result == DialogResult.No)
                {
                    Close();
                    m_UserEndGame = true;
                }
                else
                {
                    startNewGame();
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
