using MiniGameWorld.Game;
using UnityEngine;

namespace MiniGameWorld.FlowerGame
{
    public abstract class Collectible : MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {

        }
        public abstract void Collect(ICollectContext miniGame);

    }
}