using System;
using UnityEngine;

namespace MiniGameWorld.Core
{
    public class Achievement 
    {
        public AchievementData Data { get; }
        public AchievementType Id => Data.Id;
        public string Title => Data.Title;
        public string Description => Data.Description;
        public Sprite Icon => Data.Icon;

        public bool IsUnlocked { get; private set; }
        public Achievement(AchievementData data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public void Unlock()
        {
            if (IsUnlocked)
                return;

            IsUnlocked = true;
        }
        public void Load(bool unlocked)
        {
            IsUnlocked = unlocked;
        }
    }
}