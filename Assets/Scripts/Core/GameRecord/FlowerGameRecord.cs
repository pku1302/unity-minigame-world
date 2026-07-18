using MiniGameWorld.Game;
using UnityEngine;

namespace MiniGameWorld.Core
{
    public class FlowerGameRecord : GameRecord
    {
        public int FlowerCount { get; private set; }

        public FlowerGameRecord() : base(GameType.Flower) { }

        public override void UpdateRecord(MiniGameResult result)
        {
            FlowerGameResult flowerResult = (FlowerGameResult)result;

            UpdateScore(flowerResult.Score);

        }


        public override void Load(SaveManager saveManager)
        {
            base.Load(saveManager);
            FlowerCount = saveManager.LoadInt($"{GameType}_FlowerCount");
        }

        public override void Save(SaveManager saveManager)
        {
            base.Save(saveManager);

            saveManager.SaveInt($"{GameType}_FlowerCount", FlowerCount);
        }
    }
}
