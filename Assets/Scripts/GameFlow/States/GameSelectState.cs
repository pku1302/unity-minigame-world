using MiniGameWorld.Game;
using MiniGameWorld.UI;
using System.Collections;
using UnityEngine;

namespace MiniGameWorld.Core
{
    public class GameSelectState : AbstractState
    {
        private readonly UIPresenter m_UIPresenter; // 추후 리팩터링 고려
        private readonly GameRecordManager m_RecordManager;

        public GameSelectState(UIPresenter uiPresenter, GameRecordManager gameRecordManager)
        {
            m_UIPresenter = uiPresenter;
            m_RecordManager = gameRecordManager;
        }

        public override void Enter()
        {
            base.Enter();

            m_UIPresenter.ShowView(m_UIPresenter.GameSelectView);

            GameRecord flowerRecord = m_RecordManager.GetRecord(GameType.Flower);
            Debug.Log(flowerRecord.BestScore.ToString());
            m_UIPresenter.GameSelectView.SetGameInfo(
                GameType.Flower,
                flowerRecord.BestScore);
        }

        public override IEnumerator Execute()
        {
            while (true)
                yield return null;
        }

        public override void Exit()
        {
        }
    }
}

