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
        private int k_AgainstComputer = 1;
        private int k_AgaintFriend = 2;
        private const int k_DefaultBoardSize = 6;
        private int m_BoardSize = k_DefaultBoardSize;

        public GameSettings()
        {
            InitializeComponent();
            buttonAgainstComputer.Click += new EventHandler(buttonAgainstComputer_Click);
            buttonAgainstFriend.Click += new EventHandler(buttonAgainstFriend_Click);
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
            GameBoard gameForm = new GameBoard(m_BoardSize, k_AgainstComputer);
            this.Hide();
            gameForm.ShowDialog();
        }

        private void buttonAgainstFriend_Click(object sender, EventArgs e)
        {
            GameBoard gameForm = new GameBoard(m_BoardSize, k_AgaintFriend);
            this.Hide();
            gameForm.ShowDialog();
        }
    }
}
