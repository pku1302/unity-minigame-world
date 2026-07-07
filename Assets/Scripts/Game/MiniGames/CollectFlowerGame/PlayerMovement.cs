using UnityEngine;

namespace MiniGameWorld.FlowerGame
{
    public class PlayerMovement : MonoBehaviour
    {
        Board m_Board;

        Vector2Int m_Position;
        public Vector2Int Position => m_Position;

        public void Initialize(Board board, Vector2Int startPosition)
        {
            m_Board = board;
            m_Position = startPosition;

            transform.position = m_Board.GridToWorld(m_Position);
        }

        public void Move(Vector2Int direction)
        {
            if (m_Board == null)
                return;

            Vector2Int nextPosition = m_Position + direction;

            if (!m_Board.IsInside(nextPosition))
            {
                return;
            }

            m_Position = nextPosition;
            transform.position = m_Board.GridToWorld(m_Position);
        }
    }
}