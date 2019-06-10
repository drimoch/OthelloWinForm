using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OthelloGUI
{
    public partial class GameSettings : Form
    {
        private const int k_AgainstComputer = 1;
        private const int k_AgainstFriend = 2;
        private const int k_DefaultBoardSize = 6;
        private int m_BoardSize = k_DefaultBoardSize;

        public GameSettings()
        {
            InitializeComponent();
            m_BoardSize = k_DefaultBoardSize;
        }

        private void buttonBoardSize_Click(object sender, EventArgs e)
        {
            string buttonText;

            if (m_BoardSize == 12)
            {
                m_BoardSize = k_DefaultBoardSize;
            }
            else
            {
                m_BoardSize += 2;
            }

            if (m_BoardSize == 12)
            {
                buttonText = string.Format("Board Size: {0}x{0} (click to decrease)", m_BoardSize);
            }
            else
            {
                buttonText = string.Format("Board Size: {0}x{0} (click to increase)", m_BoardSize);
            }

            buttonBoardSize.Text = buttonText;
        }

        private void buttonAgainstComputer_Click(object sender, EventArgs e)
        {
            startGameForm(k_AgainstComputer);
        }

        private void buttonAgainstFriend_Click(object sender, EventArgs e)
        {
            startGameForm(k_AgainstFriend);
        }

        private void startGameForm(int i_NumOfPlayers)
        {
            GameBoard gameForm = new GameBoard(m_BoardSize, i_NumOfPlayers);
            this.Hide();
            gameForm.ShowDialog();
        }
    }
}
