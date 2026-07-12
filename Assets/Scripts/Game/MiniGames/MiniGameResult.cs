using UnityEngine;

namespace MiniGameWorld.Game
{
    public class MiniGameResult
    {
        public int Score { get; init; }
        public GameType GameType { get; init; }

        public MiniGameResult(int score, GameType gameType)
        {
            Score = score;
            GameType = gameType;
        }
    }

}