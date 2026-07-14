using UnityEngine;
using System.Collections;
using MiniGameWorld.UI;
using MiniGameWorld.Game;
using System;

namespace MiniGameWorld.Core
{
    public class ResultState : AbstractState
    {
        private readonly UIPresenter m_UI;
        private readonly Func<MiniGameResult> m_ResultGetter;
        private readonly GameRecordManager m_RecordManager;

        public ResultState(UIPresenter uIPresenter, Func<MiniGameResult> resultGetter, GameRecordManager gameRecordManager)
        {
            m_UI = uIPresenter;
            m_ResultGetter = resultGetter;
            m_RecordManager = gameRecordManager;
        }
        public override void Enter()
        {
            base.Enter();

            m_UI.ShowView(m_UI.ResultView);

            MiniGameResult result = m_ResultGetter();

            m_RecordManager.UpdateRecord(result);

            m_UI.ResultView.SetResult(result);
        }

        public override IEnumerator Execute()
        {
            while (true)
                yield return null;
        }

        public override void Exit()
        {
            Debug.Log("Result Exit");
        }
    }
}
