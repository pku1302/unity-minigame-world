using UnityEngine;

namespace MiniGameWorld.Core
{
    public class CurrencyAchievementChecker : AchievementChecker
    {
        public CurrencyAchievementChecker(AchievementManager achievementManager, CurrencyManager currencyManager) : base(achievementManager)
        {
            currencyManager.CurrencyChanged += OnCurrencyChanged;
        }
        private void OnCurrencyChanged(int amount)
        {
            if (amount >= 100)
            {
                AchievementManager.Unlock(AchievementType.Rich);
            }
        }
    }
}