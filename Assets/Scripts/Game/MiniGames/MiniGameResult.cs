using UnityEngine;

namespace MiniGameWorld.Game
{
    public class MiniGameResult
    {
        public int Score { get; set; }

        public MiniGameResult(int score)
        {
            Score = score;
        }
    }

}