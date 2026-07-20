using MiniGameWorld.Game;
using UnityEngine;

namespace MiniGameWorld.Core
{
    public class FlowerAchievementChecker : AchievementChecker
    {
        public FlowerAchievementChecker(
            AchievementManager achievementManager,
            GameRecordManager recordManager) : base(achievementManager)
        {
            recordManager.ResultUpdated += OnResultUpdated;
        }

        private void OnResultUpdated(GameType gameType, MiniGameResult result)
        {
            if (result is not FlowerGameResult flowerResult)
                return;
            Debug.Log($"Flower Count : {flowerResult.FlowerCount}");

            if (flowerResult.FlowerCount >= 1)
            {
                AchievementManager.Unlock(AchievementType.FirstFlower);
            }

            if (flowerResult.FlowerCount >= 100)
            {
                AchievementManager.Unlock(AchievementType.FlowerMaster);
            }
        }
    }
}
