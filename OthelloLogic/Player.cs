using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloLogic
{
    public class Player
    {
        // Members
        private ePlayerID m_playerID;
        private int m_Score;
        private int m_RoundsWinner;

        public enum ePlayerID
        {
            Computer,
            Player1,
            Player2           
        }

        public int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }

        public ePlayerID PlayerID
        {
            get
            {
                return m_playerID;
            }

            set
            {
                m_playerID = value;
            }
        }

        public int RoundsWinner
        {
            get
            {
                return m_RoundsWinner;
            }

            set
            {
                m_RoundsWinner = value;
            }
        }

        // Methods
        public Player(ePlayerID i_PlayerID)
        {
            m_playerID = i_PlayerID;
            m_Score = 2; // 2 coins for each player when game starts
            m_RoundsWinner = 0;
        }

        public void ClearPlayer()
        {
            m_Score = 2;
        }
    }
}
