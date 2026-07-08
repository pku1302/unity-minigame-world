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
        public ResultState(UIPresenter uIPresenter, Func<MiniGameResult> resultGetter)
        {
            m_UI = uIPresenter;
            m_ResultGetter = resultGetter;
        }
        public override void Enter()
        {
            base.Enter();

            m_UI.ShowView(m_UI.ResultView);

            MiniGameResult result = m_ResultGetter();

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
