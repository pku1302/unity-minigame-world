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
        public AchievementManager(CurrencyManager currencyManager, GameRecordManager gameRecordManager, SaveManager saveManager, CollectFlowerGame flowerGame)
        {
            m_SaveManager = saveManager;

            m_Checkers.Add(new FlowerAchievementChecker(this, gameRecordManager));

            currencyManager.CurrencyChanged += OnCurrencyChanged;

            RegisterAchievements();
        }
        private void OnCurrencyChanged(int amount)
        {
            if (amount >= 100)
            {
                Unlock(AchievementType.Rich);
            }
        }
        private void RegisterAchievements()
        {
            Add(new Achievement(
                AchievementType.FirstFlower,
                "Ã¹ ²É",
                "²ÉÀ» Ã³À½ Å‰µæÇß´Ù"
                ));

            Add(new Achievement(
                AchievementType.Rich,
                "ºÎÀÚ",
                "ÄÚÀÎ 100°³¸¦ ¸ð¾Ò´Ù"
                ));
        }

        private void Add(Achievement achievement)
        {
            m_Achievements.Add(achievement.Id, achievement);
        }
        public void Unlock(AchievementType type)
        {
            Achievement achievement = m_Achievements[type];

            if (achievement.IsUnlocked)
                return;

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