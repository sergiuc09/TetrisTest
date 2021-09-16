using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Temp3Script : MonoBehaviour { }

namespace StatePatternV1
{
    public abstract class State
    {
        protected GameMenuSystem GameMenuSystem;

        public State(GameMenuSystem gameMenuSystem)
        {
            GameMenuSystem = gameMenuSystem;
        }

        public virtual void Start() { }
    }

    public abstract class StateMachine
    {
        protected State _state;

        public void SetState(State state)
        {
            _state = state;
        }
    }

    //
    public class GamePlay : State
    {
        public GamePlay(GameMenuSystem gameMenuSystem) : base(gameMenuSystem)
        {

        }

        public override void Start()
        {
            Time.timeScale = 1;
            GameMenuSystem.GamePlay?.Invoke();
        }
    }

    public class GamePause : State
    {
        public GamePause(GameMenuSystem gameMenuSystem) : base(gameMenuSystem)
        {

        }

        public override void Start()
        {
            Time.timeScale = 0;
            GameMenuSystem.GamePlay?.Invoke();
        }
    }

    public class GameOverState : State
    {
        public GameOverState(GameMenuSystem gameMenuSystem)
            : base(gameMenuSystem)
        {

        }

        public override void Start()
        {
            GameMenuSystem.GameOver?.Invoke();
        }
    }

    //
    public class GameMenuSystem : StateMachine
    {
        public Action GamePlay;
        public Action GamePaused;
        public Action GameOver;


    }
}

//---------------------------------------------------------------//
namespace MenuStatePattern
{

    public class StateMachine : MonoBehaviour
    {
        private BaseState currentState;
        [SerializeField] private UIRoot ui;
        public UIRoot UI => ui;

        private void Start()
        {
            ChangeState(new MenuState());
        }

        private void Update()
        {
            if (currentState != null)
                currentState.UpdateState();
        }

        public void ChangeState(BaseState newState)
        {
            if (currentState != null)
                currentState.DestroyState();

            currentState = newState;

            if(currentState != null)
            {
                currentState.owner = this;
                currentState.PrepareState();
            }
        }

    }

    public abstract class BaseState
    {
        public StateMachine owner;

        public virtual void PrepareState() { }

        public virtual void UpdateState() { }

        public virtual void DestroyState() { }
    }

    public class MenuState : BaseState
    {
        public override void PrepareState()
        {
            // Attach functions to view events
            owner.UI.MenuView.OnStartClicked += StartClicked;
            owner.UI.MenuView.OnQuitClicked += QuitClicked;

            // Show menu view
            owner.UI.MenuView.ShowView();
        }

        public override void DestroyState()
        {
            // Hide menu view
            owner.UI.MenuView.HideView();

            // Detach functions from view events
            owner.UI.MenuView.OnStartClicked -= StartClicked;
            owner.UI.MenuView.OnQuitClicked -= QuitClicked;

            base.DestroyState();
        }

        /// <summary>
        /// Function called when Start button is clicked in Menu view.
        /// </summary>
        private void StartClicked()
        {
            owner.ChangeState(new GameState());
        }

        /// <summary>
        /// Function called when Quit button is clicked in Menu view.
        /// </summary>
        private void QuitClicked()
        {
            Application.Quit();
        }
    }

    public class GameState : BaseState
    {
        // Variables used for loading and destroying game content
        public bool loadGameContent = true;
        public bool destroyGameContent = true;

        // Used when player decide to go to menu from pause state
        public bool skipToFinish = false;

        public override void PrepareState()
        {
            base.PrepareState();

            // Skip to finish game
            if (skipToFinish)
            {
                owner.ChangeState(new GameOverState { gameResult = GameResult.GetRandomResult() });
                return;
            }

            // Attach functions to view events
            owner.UI.GameView.OnPauseClicked += PauseClicked;
            owner.UI.GameView.OnFinishClicked += FinishClicked;

            // Show game view
            owner.UI.GameView.ShowView();

            if (loadGameContent)
            {
                // here we would load game content
            }
        }

        public override void DestroyState()
        {
            if (destroyGameContent)
            {
                // here we would destroy loaded game content
            }

            // Hide game view
            owner.UI.GameView.HideView();

            // Detach functions from view events
            owner.UI.GameView.OnPauseClicked -= PauseClicked;
            owner.UI.GameView.OnFinishClicked -= FinishClicked;

            base.DestroyState();
        }

        /// <summary>
        /// Function called when Pause button is clicked in Game view.
        /// </summary>
        private void PauseClicked()
        {
            destroyGameContent = false;

            owner.ChangeState(new PauseState());
        }

        /// <summary>
        /// Function called when Finish Game button is clicked in Game view.
        /// </summary>
        private void FinishClicked()
        {
            owner.ChangeState(new GameOverState { gameResult = GameResult.GetRandomResult() });
        }
    }

    public class PauseState : BaseState
    {
        public override void PrepareState()
        {
            base.PrepareState();

            // Stop time in game
            Time.timeScale = 0;

            // Attach functions to view events
            owner.UI.PauseView.OnMenuClicked += MenuClicked;
            owner.UI.PauseView.OnResumeClicked += ResumeClicked;

            // Show pause view
            owner.UI.PauseView.ShowView();
        }

        public override void DestroyState()
        {
            // Hide pause view
            owner.UI.PauseView.HideView();

            // Detach functions from view events
            owner.UI.PauseView.OnMenuClicked -= MenuClicked;
            owner.UI.PauseView.OnResumeClicked -= ResumeClicked;

            // Resume time in game
            Time.timeScale = 1;

            base.DestroyState();
        }

        /// <summary>
        /// Function called when Menu button is clicked in Pause view.
        /// </summary>
        private void MenuClicked()
        {
            // we are using skipToFinish variable to have finishing code in one place - game state
            owner.ChangeState(new GameState { skipToFinish = true });
        }

        /// <summary>
        /// Function called when Resume button is clicked in Pause view.
        /// </summary>
        private void ResumeClicked()
        {
            // we are disabling game content loading as game is already loaded and prepared
            owner.ChangeState(new GameState { loadGameContent = false });
        }

    }

    public class GameOverState : BaseState
    {
        // Data with information about game result
        public GameResult gameResult;

        public override void PrepareState()
        {
            base.PrepareState();

            // Attach functions to view events
            owner.UI.GameOverView.OnReplayClicked += ReplayClicked;
            owner.UI.GameOverView.OnMenuClicked += MenuClicked;

            // Pass data to display it in UI
            owner.UI.GameOverView.data = gameResult;

            // Show summary view
            owner.UI.GameOverView.ShowView();
        }

        public override void DestroyState()
        {
            // Hide summary view
            owner.UI.GameOverView.HideView();

            // Detach functions from view events
            owner.UI.GameOverView.OnReplayClicked -= ReplayClicked;
            owner.UI.GameOverView.OnMenuClicked -= MenuClicked;

            base.DestroyState();
        }

        /// <summary>
        /// Function called when Replay button is clicked in Game Over / Summary view.
        /// </summary>
        private void ReplayClicked()
        {
            owner.ChangeState(new GameState());
        }

        /// <summary>
        /// Function called when Menu button is clicked in Game Over / Summary view.
        /// </summary>
        private void MenuClicked()
        {
            owner.ChangeState(new MenuState());
        }

    }

} // end namespace
