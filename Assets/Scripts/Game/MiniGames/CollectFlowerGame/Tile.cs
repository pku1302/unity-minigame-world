using MiniGameWorld.FlowerGame;
using MiniGameWorld.Game;
using UnityEngine;

namespace MiniGameWorld.FlowerGame
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] SpriteRenderer m_SpriteRenderer;
        public Vector2Int Position { get; private set; }

        public Collectible Collectible { get; private set; }

        public bool HasCollectible => Collectible != null;

        public void Initialize(Vector2Int position)
        {
            Position = position;
        }

        public void SetColor(Color color)
        {
            m_SpriteRenderer.color = color;
        }

        public void SetCollectible(Collectible collectible)
        {
            Collectible = collectible;
        }

        public void RemoveCollectible()
        {
            Collectible = null;
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }

}