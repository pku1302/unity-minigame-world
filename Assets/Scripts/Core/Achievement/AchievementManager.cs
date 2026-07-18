using MiniGameWorld.Game;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGameWorld.Core
{
    public enum AchievementType
    {
        FirstFlower,
        FlowerMaster,
        Rich,
    }
    public class AchievementManager
    {
        public event Action<Achievement> AchievementUnlocked;

        private readonly Dictionary<AchievementType, Achievement> m_Achievements = new Dictionary<AchievementType, Achievement>();
        private readonly SaveManager m_SaveManager;
        private readonly List<AchievementChecker> m_Checkers = new();
        public AchievementManager(AchievementData[] achievementDatas, CurrencyManager currencyManager, GameRecordManager gameRecordManager, SaveManager saveManager)
        {
            m_SaveManager = saveManager;
            m_Checkers.Add(new FlowerAchievementChecker(this, gameRecordManager));
            m_Checkers.Add(new CurrencyAchievementChecker(this, currencyManager));

            RegisterAchievements(achievementDatas);
            Reset();
        }
        private void RegisterAchievements(AchievementData[] datas)
        {
            foreach (AchievementData data in datas)
            {
                Debug.Log($"Register : { data.Id }");
                Add(new Achievement(data));
            }
        }

        // µð¹ö±ë ¿ë
        public void Reset()
        {
            foreach (Achievement achievement in m_Achievements.Values)
            {
                achievement.Load(false);
            }

            Save();
        }

        private void Add(Achievement achievement)
        {
            m_Achievements.Add(achievement.Id, achievement);
        }
        public void Unlock(AchievementType type)
        {
            if (!m_Achievements.TryGetValue(type, out Achievement achievement))
            {
                Debug.Log($"Achievement not found : {type}");
                return;
            }

            if (achievement.IsUnlocked)
            {
                Debug.Log("Unlock");
                return;

            }

            achievement.Unlock();

            Save();
                
            AchievementUnlocked?.Invoke(achievement);
        }
        public bool IsUnlocked(AchievementType id)
        {
            return m_Achievements.TryGetValue(id, out Achievement achievement)
                && achievement.IsUnlocked;
        }
        public void Save()
        {
            foreach (Achievement achievement in m_Achievements.Values)
            {
                m_SaveManager.SaveInt(
                    $"Achievement_{achievement.Id}",
                    achievement.IsUnlocked ? 1 : 0);
            }

            m_SaveManager.Save();
        }
        public void Load()
        {
            foreach (Achievement achievement in m_Achievements.Values)
            {
                bool unlocked =
                    m_SaveManager.LoadInt(
                        $"Achievement_{achievement.Id}") == 1;

                achievement.Load(unlocked);
            }
        }
    }
}