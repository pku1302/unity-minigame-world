using UnityEngine;
using MiniGameWorld.Core;
using MiniGameWorld.Game;
using UnityEngine.InputSystem;
using MiniGameWorld.UI;

namespace MiniGameWorld
{
    public class GameFlowManager : MonoBehaviour
    {
        [SerializeField]
        UIPresenter m_UIPresenter;
        [SerializeField]
        private CollectFlowerGame m_CollectFlowerGame;
        StateMachine m_StateMachine = new StateMachine();

        MiniGameResult m_CurrentResult;

        IState m_TitleState;
        IState m_MainMenuState;
        IState m_GameSelectState;
        IState m_GameState;
        IState m_ResultState;

        bool m_IsStartRequested;
        bool m_IsMainMenuRequested;
        bool m_IsFinishRequested;

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

            m_StateMachine.StateChanged += OnStateChanged;

            m_UIPresenter.StartRequested += OnStartRequested;
            m_UIPresenter.SettingsRequested += OnSettingsRequested;
            m_UIPresenter.QuitRequested += OnQuitRequested;
            m_UIPresenter.MainMenuRequested += OnMainMenuRequested;
            m_UIPresenter.FinishRequested += OnFinishRequested;
        }

        private void OnStateChanged(IState previous, IState current)
        {
            ResetRequests();
        }

        private void ResetRequests()
        {
            m_IsStartRequested = false;
            m_IsMainMenuRequested = false;
            m_IsFinishRequested = false;
        }

        private void OnFinishRequested()
        {
            m_IsFinishRequested = true;
        }

        private void OnStartRequested()
        {
            m_IsStartRequested = true;
        }

        private void OnMainMenuRequested()
        {
            m_IsMainMenuRequested = true;
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
            m_MainMenuState = new MainMenuState (m_UIPresenter){ Name = "MainMenu" };
            m_GameSelectState = new GameSelectState (m_UIPresenter) { Name = "GameSelect" };
            m_ResultState = new ResultState (m_UIPresenter, () => m_CurrentResult) { Name = "Result" };
            m_GameState = new GameState(m_UIPresenter, m_CollectFlowerGame, OnGameFinished) { Name = "Game" };
        }
        private void OnGameFinished(MiniGameResult result)
        {
            m_CurrentResult = result;
        }

        private void AddLinks()
        {
            m_TitleState.AddLink(
                new ConditionLink( 
                    () => Keyboard.current.anyKey.wasPressedThisFrame,
                    m_MainMenuState));

            m_MainMenuState.AddLink(
                new ConditionLink(
                    () => m_IsStartRequested,
                    m_GameSelectState));

            m_GameSelectState.AddLink(
                new ConditionLink(
                    () => m_IsStartRequested,
                    m_GameState));

            m_GameSelectState.AddLink(
                new ConditionLink(
                    () => m_IsMainMenuRequested,
                    m_MainMenuState));

            m_GameState.AddLink(
                new ConditionLink(
                    () => m_IsFinishRequested,
                    m_ResultState));

            m_ResultState.AddLink(
                new ConditionLink(
                    () => m_IsStartRequested,
                    m_GameState));

            m_ResultState.AddLink(
                new ConditionLink(
                    () => m_IsMainMenuRequested,
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
            m_UIPresenter.MainMenuRequested -= OnMainMenuRequested;
            m_UIPresenter.FinishRequested -= OnFinishRequested;

            m_StateMachine.StateChanged -= OnStateChanged;

        }
    }
}
