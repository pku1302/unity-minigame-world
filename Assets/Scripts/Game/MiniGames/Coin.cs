using MiniGameWorld.FlowerGame;
using UnityEngine;

namespace MiniGameWorld.Game
{
    public class Coin : Collectible
    {
        public override void Collect(ICollectContext miniGame)
        {
            miniGame.AddScore(5);
            miniGame.AddTime(3f);
            miniGame.AddCoin(1);
        }
    }
}
