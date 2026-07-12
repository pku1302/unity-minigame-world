using System.Collections.Generic;
using UnityEngine;
using MiniGameWorld.Game;

namespace MiniGameWorld.Core
{
    public class GameRecord
    {
        public int BestScore { get; private set; }
        public int PlayCount { get; private set; }

        public void UpdateScore(int score)
        {
            if (score > BestScore)
            {
                BestScore = score;
            }

            PlayCount++;
        }
        public void Load(int bestScore, int playCount)
        {
            BestScore = bestScore;
            PlayCount = playCount;
        }
    }

    public class GameRecordManager
    {
        private readonly Dictionary<GameType, GameRecord> m_Records = new();
        private readonly SaveManager m_SaveManager;

        public GameRecordManager(SaveManager saveManager)
        {
            m_SaveManager = saveManager;

            foreach (GameType gameType in System.Enum.GetValues(typeof(GameType)))
            {
                m_Records.Add(gameType, new GameRecord());
            }
        }
        private string GetBestScoreKey(GameType gameType)
        {
            return $"{gameType}_BestScore";
        }
        private string GetPlayCountKey(GameType gameType)
        {
            return $"{gameType}_PlayCount";
        }
        public void Load()
        {
            foreach (GameType gameType in System.Enum.GetValues(typeof(GameType)))
            {
                GameRecord record = m_Records[gameType];

                record.Load(
                    m_SaveManager.LoadInt(GetBestScoreKey(gameType)),
                    m_SaveManager.LoadInt(GetPlayCountKey(gameType))
                    );
            }
        }
        public void Save(GameType gameType)
        {
            GameRecord record = m_Records[gameType];

            m_SaveManager.SaveInt(
                GetBestScoreKey(gameType),
                record.BestScore);

            m_SaveManager.SaveInt(
                GetPlayCountKey(gameType),
                record.PlayCount);

            m_SaveManager.Save();
        }

        public GameRecord GetRecord(GameType gameType)
        {
            return m_Records[gameType];
        }
        public void UpdateScore(GameType gameType, int score)
        {
            m_Records[gameType].UpdateScore(score);

            Save(gameType);
        }
    }
}
