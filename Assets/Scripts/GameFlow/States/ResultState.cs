using UnityEngine;
using System.Collections;
using MiniGameWorld.UI;

namespace MiniGameWorld.Core
{
    public class ResultState : AbstractState
    {
        private readonly UIPresenter m_UI;
        public ResultState(UIPresenter uIPresenter)
        {
            m_UI = uIPresenter;
        }

        public override void Enter()
        {
            base.Enter();

            m_UI.ShowView(m_UI.ResultView);
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
