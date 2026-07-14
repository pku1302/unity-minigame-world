using UnityEngine;

namespace MiniGameWorld.Core
{
    public class Achievement
    {
        public AchievementType Id { get; }
        public string Title { get; }
        public string Description { get; }  
        public bool IsUnlocked { get; private set; }

        public Achievement(AchievementType id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
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