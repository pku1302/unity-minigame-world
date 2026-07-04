using UnityEngine;

namespace MiniGameWorld.Core
{
    public interface ILink
    {
        bool Validate(out IState nextState);
    }
}

