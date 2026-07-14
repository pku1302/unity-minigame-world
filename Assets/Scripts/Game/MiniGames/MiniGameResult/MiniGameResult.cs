using UnityEngine;

namespace MiniGameWorld.Game
{
    public abstract class MiniGameResult
    {
        public int Score { get; }
        public GameType GameType { get; }

        protected MiniGameResult(int score, GameType gameType)
        {
            Score = score;
            GameType = gameType;
        }
    }

}