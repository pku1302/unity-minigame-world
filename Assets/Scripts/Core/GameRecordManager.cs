using System.Collections.Generic;
using UnityEngine;
using MiniGameWorld.Game;
using System;

namespace MiniGameWorld.Core
{
    public abstract class GameRecord
    {
        protected GameType GameType { get; }

        protected GameRecord(GameType gameType)
        {
            GameType = gameType;
        }

        public int BestScore { get; protected set; }
        public int PlayCount { get; protected set; }

        public abstract void UpdateRecord(MiniGameResult result);
        public void UpdateScore(int score)
        {
            if (BestScore > score)
                BestScore = score;
        }

        public virtual void Load(SaveManager saveManager)
        {
            BestScore = saveManager.LoadInt($"{GameType}_BestScore");
            PlayCount = saveManager.LoadInt($"{GameType}_PlayCount");
        }
        public virtual void Save(SaveManager saveManager)
        {
            saveManager.SaveInt($"{GameType}_BestScore", BestScore);
            saveManager.SaveInt($"{GameType}_PlayCount", PlayCount);
        }
        
    }

    public class GameRecordManager
    {
        private readonly Dictionary<GameType, GameRecord> m_Records = new();
        private readonly SaveManager m_SaveManager;

        public event Action<GameType, GameRecord> RecordUpdated;

        public GameRecordManager(SaveManager saveManager)
        {
            m_SaveManager = saveManager;

            foreach (GameType gameType in System.Enum.GetValues(typeof(GameType)))
            {
                m_Records.Add(gameType, CreateRecord(gameType));
            }
        }
        private GameRecord CreateRecord(GameType gameType)
        {
            return gameType switch
            {
                GameType.Flower => new FlowerGameRecord(),

                _ => throw new ArgumentOutOfRangeException(nameof(gameType), gameType, null)
            };
        }

        public void Load()
        {
            foreach (GameRecord record in m_Records.Values)
            {
                record.Load(m_SaveManager);
            }
        }
        public void Save(GameType gameType)
        {
            m_Records[gameType].Save(m_SaveManager);
            m_SaveManager.Save();
        }

        public GameRecord GetRecord(GameType gameType)
        {
            return m_Records[gameType];
        }
        public void UpdateRecord(MiniGameResult result)
        {
            GameRecord record = m_Records[result.GameType];

            record.UpdateRecord(result);

            RecordUpdated?.Invoke(result.GameType, record);

            Save(result.GameType);
        }
    }
}
