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
            recordManager.RecordUpdated += OnRecordUpdated;
        }

        private void OnRecordUpdated(GameType gameType, GameRecord record)
        {
            if (record is not FlowerGameRecord flowerRecord)
                return;

            if (flowerRecord.FlowerCount >= 1)
            {
                AchievementManager.Unlock(AchievementType.FirstFlower);
            }

            if (flowerRecord.FlowerCount >= 100)
            {
                AchievementManager.Unlock(AchievementType.FlowerMaster);
            }
        }
    }
}
