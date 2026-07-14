using UnityEngine;

namespace MiniGameWorld.Game
{
    public class FlowerGameResult : MiniGameResult
    {
        public int FlowerCount { get; }

        public FlowerGameResult(int score, int flowerCount) : base(score, GameType.Flower)
        {
            FlowerCount = flowerCount;
        }
    }
}