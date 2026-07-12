using UnityEngine;
using MiniGameWorld.FlowerGame;

namespace MiniGameWorld.Game
{ 
    public class CollectFlowerGame : MiniGame
    {
        [SerializeField] Board m_Board;
        [SerializeField] Player m_Player;
        [SerializeField] LaserSystem m_LaserSystem;

        private Vector2Int m_StartPosition = new Vector2Int(2, 2);
        public int Score => m_Score;

        void Awake()
        {
            m_Player.Moved += OnPlayerMoved;
            m_Player.Hit += OnPlayerHit;
            m_Board.FlowerSpawned += OnFlowerSpawned;
        }
        public override void Initialize()
        {
            base.Initialize();
            m_Player.Initialize(m_Board, m_StartPosition);
        }

        public override void ResetGame()
        {
            base.ResetGame();
            RaiseScoreChanged(m_Score);
            m_Player.ResetPlayer(m_StartPosition);
            m_Board.ResetBoard();
        }

        public override void StartGame()
        {
            base.StartGame();
            m_Board.StartSpawn();
            m_LaserSystem.StartSystem();
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
        private void OnPlayerHit()
        {
            ReduceTime(10f);
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
            AddTime(3f);
        }
        public override void FinishGame()
        {
            m_Board.StopSpawn();
            m_LaserSystem.StopSystem();

            gameObject.SetActive(false);

            RaiseFinished();
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