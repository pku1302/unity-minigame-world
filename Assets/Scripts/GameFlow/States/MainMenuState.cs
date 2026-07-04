using UnityEngine;
using System.Collections;

namespace MiniGameWorld.Core
{
    public class MainMenuState : AbstractState
    {
        public override void Enter()
        {
            base.Enter();

            Debug.Log("MainMenu Enter");
        }

        public override IEnumerator Execute()
        {
            while (true)
                yield return null;
        }

        public override void Exit()
        {
            Debug.Log("MainMenu Exit");
        }
    }
}
