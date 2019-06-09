using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloLogic
{
    public class Board
    {
        // Memebers
        private Cell[,] m_MatrixBoard; // matrix for game's board

        public Cell[,] Matrix
        {
            get
            {
                return m_MatrixBoard;
            }

            set
            {
                m_MatrixBoard = value;
            }
        }

        // Methods
        public Board(int i_BoardSize)
        {
            m_MatrixBoard = new Cell[i_BoardSize, i_BoardSize];
            createCells();
        }

        private void createCells()
        {
            for (int i = 0; i < m_MatrixBoard.GetLength(0); i++)
            {
                for (int j = 0; j < m_MatrixBoard.GetLength(1); j++)
                {
                    m_MatrixBoard[i, j] = new Cell(i, j);
                }
            }
        }

        public void InitializeBoard()
        {
            cleanBoard();
            int middle = m_MatrixBoard.GetLength(0) / 2;
            Cell c = m_MatrixBoard[middle, middle];

            m_MatrixBoard[middle - 1, middle - 1].CellType = Cell.eType.Player1;
            m_MatrixBoard[middle - 1, middle].CellType = Cell.eType.Player2;
            m_MatrixBoard[middle, middle - 1].CellType = Cell.eType.Player2;
            m_MatrixBoard[middle, middle].CellType = Cell.eType.Player1;
        }

        private void cleanBoard()
        {
            foreach (Cell cell in m_MatrixBoard)
            {
                cell.CellType = Cell.eType.Empty;
            }
        }
    }
}
