using MiniGameWorld.FlowerGame;
using UnityEngine;

namespace MiniGameWorld.FlowerGame
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] SpriteRenderer m_SpriteRenderer;
        public Vector2Int Position { get; private set; }

        public Flower Flower { get; private set; }

        public bool HasFlower => Flower != null;

        public void Initialize(Vector2Int position)
        {
            Position = position;
        }

        public void SetColor(Color color)
        {
            m_SpriteRenderer.color = color;
        }

        public void SetFlower(Flower flower)
        {
            Flower = flower;
        }

        public void RemoveFlower()
        {
            Flower = null;
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }

}