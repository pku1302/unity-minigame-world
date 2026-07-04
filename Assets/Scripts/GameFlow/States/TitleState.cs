using UnityEngine;
using System.Collections;


namespace MiniGameWorld.Core
{
    public class TitleState : AbstractState
    {
        public override void Enter()
        {
            base.Enter();

            Debug.Log("Title Enter");
        }

        public override IEnumerator Execute()
        {
            while (true)
                yield return null;
        }

        public override void Exit()
        {
            Debug.Log("Title Exit");
        }
    }
}
