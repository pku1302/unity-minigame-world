using UnityEngine;

namespace MiniGameWorld.Core
{
    public abstract class AchievementChecker
    {
        protected readonly AchievementManager AchievementManager;
        protected AchievementChecker(AchievementManager achievementManager)
        {
            AchievementManager = achievementManager;
        }
    }

}