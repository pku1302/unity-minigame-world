using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


namespace MiniGameWorld.Core
{
    public interface IState
    {
        void Enter();
        IEnumerator Execute();
        void Exit();
        void AddLink(ILink link);
        void RemoveLink(ILink link);
        bool ValidateLinks(out IState nextState);
    }
}

