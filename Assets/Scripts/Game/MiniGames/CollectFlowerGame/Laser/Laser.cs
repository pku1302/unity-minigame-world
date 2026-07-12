using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace MiniGameWorld.FlowerGame
{
    public class Laser : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer m_Renderer;

        [SerializeField]
        private float m_Thickness = 0.6f;

        public void Initialize(float length, LaserDirection direction)
        {
            transform.localScale = new Vector3(length, m_Thickness, 1f);

            Vector3 offset = direction switch
            {
                LaserDirection.Up => Vector3.up,
                LaserDirection.Down => Vector3.down,
                LaserDirection.Left => Vector3.left,
                LaserDirection.Right => Vector3.right,
                _ => Vector3.zero
            };

            transform.position += offset * (length * 0.5f + 0.5f);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent<Player>(out var player))
                return;

            player.RaiseHit();
        }
    }
}
