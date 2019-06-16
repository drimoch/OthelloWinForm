using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OthelloLogic;

namespace OthelloGUI
{
    internal class VisualCell
    {
        private Image m_ImagePlayer1 = Properties.Resources.CoinRed;
        private Image m_ImagePlayer2 = Properties.Resources.CoinYellow;
        private Cell m_LogicCell;
        private PictureBox m_Image;

        public Cell LogicCell
        {
            get
            {
                return m_LogicCell;
            }
        }

        public PictureBox Image
        {
            get
            {
                return m_Image;
            }
        }

        public VisualCell(Cell i_Cell)
        {
            m_LogicCell = i_Cell;
            m_Image = new PictureBox();
        }

        public void SetImage(Cell i_Cell, int i_Height, int i_Width)
        {
            Image currentPlayerImage = i_Cell.CellType == Cell.eType.Player1 ? m_ImagePlayer1 : m_ImagePlayer2;
            m_Image.Image = currentPlayerImage;
            m_Image.Height = i_Height;
            m_Image.Width = i_Width;
            m_Image.BackColor = Color.Transparent;
            m_Image.Cursor = Cursors.Arrow;
            m_Image.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void SetEmptyCell(MouseEventHandler i_OnClick)
        {
            m_Image.BackColor = Color.Green;
            m_Image.Cursor = Cursors.Hand;
            m_Image.MouseClick -= i_OnClick; // remove old event if exists
            m_Image.MouseClick += i_OnClick;
        }
    }
}
