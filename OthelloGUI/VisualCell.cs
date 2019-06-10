using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OthelloLogic;

namespace OthelloGUI
{
    class VisualCell
    {
        private Image m_ImagePlayer1 = Properties.Resources.CoinRed;
        private Image m_ImagePlayer2 = Properties.Resources.CoinYellow;
        private Cell m_LogicCell;
        private PictureBox m_Image;
        private bool m_IsEmpty;

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

        public bool IsEmpty
        {
            get
            {
                return m_IsEmpty;
            }
        }

        public VisualCell(Cell i_Cell)
        {
            m_LogicCell = i_Cell;
        }

        public PictureBox SetImage(Cell i_Cell, int i_Height, int i_Width)
        {
            m_IsEmpty = false;
            Image currentPlayerImage = i_Cell.CellType == Cell.eType.Player1 ? m_ImagePlayer1 : m_ImagePlayer2;
            PictureBox cellPicture = new PictureBox();
            cellPicture.Image = currentPlayerImage;
            cellPicture.Height = i_Height;
            cellPicture.Width = i_Width;
            cellPicture.SizeMode = PictureBoxSizeMode.StretchImage;

            return cellPicture;
        }

        public PictureBox SetEmptyCell(MouseEventHandler i_OnClick)
        {
            m_IsEmpty = true;
            PictureBox cellEmpty = new PictureBox();
            cellEmpty.BackColor = Color.Green;
            cellEmpty.Cursor = Cursors.Hand;
            cellEmpty.MouseClick += new MouseEventHandler(i_OnClick);
            return cellEmpty;
        }

        public void ChangeCellType(Cell.eType i_NewType)
        {
            if (m_Image==null)
            {
                m_Image = new PictureBox();

            }
            m_Image.Image = i_NewType == Cell.eType.Player1 ? m_ImagePlayer1 : m_ImagePlayer2;
        }
    }
}
