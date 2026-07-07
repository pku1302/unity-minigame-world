using System;
using UnityEngine;

namespace MiniGameWorld.Game
{
    public abstract class MiniGame : MonoBehaviour
    {
        public event Action<MiniGameResult> Finished;
        public abstract void Initialize();
        public abstract void StartGame();
        public abstract void FinishGame();
        public abstract void Dispose();
    }
}