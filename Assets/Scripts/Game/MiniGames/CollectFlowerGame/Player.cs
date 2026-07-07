using UnityEngine;

namespace MiniGameWorld.FlowerGame
{
    [RequireComponent(typeof(PlayerInput), typeof(PlayerMovement))]
    public class Player : MonoBehaviour
    {
        PlayerInput m_PlayerInput;
        PlayerMovement m_PlayerMovement;

        void Awake()
        {
            m_PlayerInput = GetComponent<PlayerInput>();
            m_PlayerMovement = GetComponent<PlayerMovement>();
        }

        public void Initialize(Board board, Vector2Int startPosition)
        {
            m_PlayerMovement.Initialize(board, startPosition);
        }

        void Update()
        {
            Vector2Int direction = m_PlayerInput.Direction;

            if (direction != Vector2Int.zero)
                m_PlayerMovement.Move(direction);
        }
    }

}