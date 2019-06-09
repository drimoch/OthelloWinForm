using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloLogic
{
    public class Cell
    {
        // Members
        private Location m_CellLocation;
        private eType m_CellType;

        public Location CellLocation
        {
            get
            {
                return m_CellLocation;
            }

            set
            {
                m_CellLocation = value;
            }
        }

        public enum eType
        {
            Empty,
            Player1,
            Player2 
        }

        public eType CellType
        {
            get
            {
                return m_CellType;
            }

            set
            {
                m_CellType = value;
            }
        }

        public struct Location
        {
            private int x;
            private int y;

            public int X
            {
                get
                {
                    return x;
                }

                set
                {
                    x = value;
                }
            }

            public int Y
            {
                get
                {
                    return y;
                }

                set
                {
                    y = value;
                }
            }
        }

        // Methods
        public Cell(int i_X, int i_Y, eType i_Type = eType.Empty)
        {
            m_CellLocation = new Location();
            m_CellType = i_Type;
            m_CellLocation.X = i_X;
            m_CellLocation.Y = i_Y;
        }
    }
}
