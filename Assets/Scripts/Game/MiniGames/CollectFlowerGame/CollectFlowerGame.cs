using UnityEngine;
using MiniGameWorld.FlowerGame;

namespace MiniGameWorld.Game
{ 
    public class CollectFlowerGame : MiniGame
    {
        [SerializeField] Board m_Board;
        [SerializeField] Player m_Player;

        private Vector2Int m_StartPosition = new Vector2Int(2, 2);

        public int Score => m_Score;

        void Awake()
        {
            m_Player.Moved += OnPlayerMoved;
            m_Board.FlowerSpawned += OnFlowerSpawned;
        }
        public override void Initialize()
        {
            m_Player.Initialize(m_Board, m_StartPosition);
        }

        public override void ResetGame()
        {
            m_Score = 0;
            RaiseScoreChanged(m_Score);
            m_Player.ResetPlayer(m_StartPosition);
            m_Board.ResetBoard();
        }

        public override void StartGame()
        {
            m_Board.StartSpawn();
        }

        private void AddScore(int amount)
        {
            m_Score += amount;
            RaiseScoreChanged(m_Score);
        }
        private void TryCollectFlower(Vector2Int position)
        {
            Tile tile = m_Board.GetTile(m_Player.Position);

            if (!tile.HasFlower)
                return;

            CollectFlower(tile);
        }

        private void OnPlayerMoved(Vector2Int position)
        {
            TryCollectFlower(position);
        }
        private void OnFlowerSpawned(Vector2Int position)
        {
            TryCollectFlower(position);
        }

        private void CollectFlower(Tile tile)
        {
            Destroy(tile.Flower.gameObject);
            tile.RemoveFlower();

            AddScore(5);
            Debug.Log($"Score : {m_Score}");
        }
        public override void FinishGame()
        {
            gameObject.SetActive(false);

            m_Board.StopSpawn();
        }
        void OnDestroy()
        {
            m_Player.Moved -= OnPlayerMoved;   
        }
        public override void Dispose()
        {
        }
    }
}