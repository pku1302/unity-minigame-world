using System;
using System.Collections;
using UnityEngine;

namespace MiniGameWorld.FlowerGame
{
    [RequireComponent(typeof(PlayerInput), typeof(PlayerMovement))]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private float m_InvincibleTime = 1.5f;
        [SerializeField]
        private SpriteRenderer m_Renderer;

        private bool m_IsInvincible;

        PlayerInput m_PlayerInput;
        PlayerMovement m_PlayerMovement;

        public event Action<Vector2Int> Moved;
        public event Action Hit;
        public Vector2Int Position => m_PlayerMovement.Position;

        void Awake()
        {
            m_Renderer = GetComponent<SpriteRenderer>();
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
            if (m_IsInvincible)
                return;

            Hit?.Invoke();

            if (!gameObject.activeInHierarchy)
                return;

            StartCoroutine(InvincibleRoutine());
        }
        private IEnumerator InvincibleRoutine()
        {
            m_IsInvincible = true;

            float elapsed = 0f;

            while (elapsed < m_InvincibleTime)
            {
                m_Renderer.enabled = !m_Renderer.enabled;

                yield return new WaitForSeconds(0.1f);

                elapsed += 0.1f;
            }

            m_Renderer.enabled = true;
            m_IsInvincible = false;
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