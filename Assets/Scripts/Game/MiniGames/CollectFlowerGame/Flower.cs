using MiniGameWorld.Game;
using UnityEngine;

namespace MiniGameWorld.FlowerGame
{
    public class Flower : Collectible
    {
        public override void Collect(ICollectContext miniGame)
        {
            miniGame.AddTime(3f);
            miniGame.AddScore(5);
        }
    }
}
