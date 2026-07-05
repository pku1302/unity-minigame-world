using UnityEngine;
using MiniGameWorld.Core;
using UnityEngine.InputSystem;
using MiniGameWorld.UI;

namespace MiniGameWorld
{
    public class GameFlowManager : MonoBehaviour
    {
        [SerializeField]
        private UIPresenter m_UIPresenter;
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

            m_UIPresenter.StartRequested += OnStartRequested;
            m_UIPresenter.SettingsRequested += OnSettingsRequested;
            m_UIPresenter.QuitRequested += OnQuitRequested;
        }
        private void OnStartRequested()
        {
            Debug.Log("Game Start Requested");
        }

        private void OnSettingsRequested()
        {
            Debug.Log("Settings Requested");
        }

        private void OnQuitRequested()
        {
            Debug.Log("Quit Requested");
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

        private void OnDestroy()
        {
            if (m_UIPresenter == null)
                return;

            m_UIPresenter.StartRequested -= OnStartRequested;
            m_UIPresenter.SettingsRequested -= OnSettingsRequested;
            m_UIPresenter.QuitRequested -= OnQuitRequested;
        }
    }
}
