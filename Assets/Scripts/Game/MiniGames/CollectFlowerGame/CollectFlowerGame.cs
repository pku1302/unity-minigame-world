using UnityEngine;
using MiniGameWorld.FlowerGame;

namespace MiniGameWorld.Game
{ 
    public class CollectFlowerGame : MiniGame
    {
        [SerializeField] Board m_Board;
        [SerializeField] Player m_Player;

        public override void Initialize()
        {
        }

        public override void StartGame()
        {
            m_Player.Initialize(m_Board, new Vector2Int(2, 2));
        }

        public override void FinishGame()
        {
            gameObject.SetActive(false);
        }

        public override void Dispose()
        {
        }
    }
}