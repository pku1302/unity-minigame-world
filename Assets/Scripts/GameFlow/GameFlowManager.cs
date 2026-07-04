using UnityEngine;
using MiniGameWorld.Core;
using UnityEngine.InputSystem;

namespace MiniGameWorld
{
    public class GameFlowManager : MonoBehaviour
    {
        StateMachine m_StateMachine = new StateMachine();

        IState m_TitleState;
        IState m_MainMenuState;

        private void Awake()
        {
            Utilities.Coroutines.Initialize(this);
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            SetStates();
            AddLinks();
            RunStateMachine();
        }

        private void SetStates()
        {
            m_TitleState = new TitleState { Name = "Title" };
            m_MainMenuState = new MainMenuState { Name = "MainMenu" };
        }

        private void AddLinks()
        {
            m_TitleState.AddLink(
                new ConditionLink( 
                    () => Keyboard.current.anyKey.wasPressedThisFrame,
                    m_MainMenuState));
        }

        private void RunStateMachine()
        {
            m_StateMachine.Run(m_TitleState);
        }
    }
}
