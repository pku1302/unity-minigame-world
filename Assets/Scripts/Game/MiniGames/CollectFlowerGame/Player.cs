using System;
using UnityEngine;

namespace MiniGameWorld.FlowerGame
{
    [RequireComponent(typeof(PlayerInput), typeof(PlayerMovement))]
    public class Player : MonoBehaviour
    {
        PlayerInput m_PlayerInput;
        PlayerMovement m_PlayerMovement;

        public event Action<Vector2Int> Moved;
        public event Action Hit;
        public Vector2Int Position => m_PlayerMovement.Position;

        void Awake()
        {
            m_PlayerInput = GetComponent<PlayerInput>();
            m_PlayerMovement = GetComponent<PlayerMovement>();
            m_PlayerMovement.Moved += OnMoved;
        }

        public void ResetPlayer(Vector2Int startPosition)
        {
            m_PlayerMovement.ResetPosition(startPosition);
        }

        public void RaiseHit()
        {
            Hit?.Invoke();
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

        private void OnDestroy()
        {
            m_PlayerMovement.Moved -= OnMoved;
        }
        private void OnMoved(Vector2Int postiion)
        {
            Moved?.Invoke(postiion);
        }
    }
}