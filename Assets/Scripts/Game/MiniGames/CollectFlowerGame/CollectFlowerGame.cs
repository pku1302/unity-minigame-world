using UnityEngine;
using MiniGameWorld.FlowerGame;
using System;
using MiniGameWorld.Core;

namespace MiniGameWorld.Game
{
    public interface ICollectContext
    {
        void AddScore(int amount);
        void AddTime(float amount);
        void AddCoin(int amount);
        void NotifyFlowerCollected();
    }
    public class CollectFlowerGame : MiniGame, ICollectContext
    {
        [SerializeField] Board m_Board;
        [SerializeField] Player m_Player;
        [SerializeField] LaserSystem m_LaserSystem;

        public event Action FlowerCollected;

        private int m_FlowerCount;
        private Vector2Int m_StartPosition = new Vector2Int(2, 2);
        public int Score => m_Score;
        public int FlowerCount => m_FlowerCount;

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
        public void NotifyFlowerCollected()
        {
            FlowerCollected?.Invoke();
        }
        public override void AddScore(int amount)
        {
            base.AddScore(amount);
            m_FlowerCount++;
        }

        public override void Pause()
        {
            Time.timeScale = 0f;
            m_Player.enabled = false;
        }

        public override void Resume()
        {
            Time.timeScale = 1f;
            m_Player.enabled = true;
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

        private void TryCollectObject(Vector2Int position)
        {
            Tile tile = m_Board.GetTile(m_Player.Position);

            if (!tile.HasCollectible)
                return;

            CollectObject(tile);
        }

        private void OnPlayerMoved(Vector2Int position)
        {
            TryCollectObject(position);
        }
        private void OnPlayerHit()
        {
            ReduceTime(10f);
        }
        private void OnFlowerSpawned(Vector2Int position)
        {
            TryCollectObject(position);
        }

        private void CollectObject(Tile tile)
        {
            Collectible collectible = tile.Collectible;

            collectible.Collect(this);

            tile.RemoveCollectible();
            Destroy(collectible.gameObject);
        }
        public override void FinishGame()
        {
            Cleanup();

            RaiseFinished();
        }

        public override void Cleanup()
        {
            m_Board.StopSpawn();
            m_LaserSystem.StopSystem();

            gameObject.SetActive(false);
        }

        public override MiniGameResult GetResult()
        {
            return new FlowerGameResult(
                m_Score,
                m_FlowerCount);
        }

        void OnDestroy()
        {
            m_Player.Moved -= OnPlayerMoved;   
            m_Player.Hit -= OnPlayerHit;
            m_Board.FlowerSpawned -= OnFlowerSpawned;
        }
        public override void Dispose()
        {
        }
    }
}