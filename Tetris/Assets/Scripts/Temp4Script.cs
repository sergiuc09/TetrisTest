using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Temp4Script : MonoBehaviour { }
 
namespace MenuStatePattern
{
    public class GameResult
    {
        // Score in "game".
        public int score;

        /// <summary>
        /// Method used to generate random game result.
        /// </summary>
        /// <returns>Random  game result.</returns>
        public static GameResult GetRandomResult()
        {
            return new GameResult { score = Random.Range(0, 100) };
        }
    }

    public class UIRoot : MonoBehaviour
    {
        [SerializeField]
        private MenuView menuView;
        public MenuView MenuView => menuView;

        [SerializeField]
        private GameView gameView;
        public GameView GameView => gameView;

        [SerializeField]
        private PauseView pauseView;
        public PauseView PauseView => pauseView;

        [SerializeField]
        private GameOverView gameOverView;
        public GameOverView GameOverView => gameOverView;
    }

    public class BaseView : MonoBehaviour
    {
        /// <summary>
        /// Method called to show view
        /// </summary>
        public virtual void ShowView()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Method called to hide view
        /// </summary>
        public virtual void HideView()
        {
            gameObject.SetActive(false);
        }
    }

    public class GameOverView : BaseView
    {
        // Reference to label used for displaying score
        [SerializeField]
        private TMP_Text scoreLabel;

        // Events to attach to.
        public Action OnReplayClicked;
        public Action OnMenuClicked;

        // Data to display
        public GameResult data;

        public override void ShowView()
        {
            base.ShowView();

            // If we have data, we should display it!
            scoreLabel.text = "Score : " + (data != null ? data.score : 0);
        }

        public override void HideView()
        {
            // Let GC clean memory
            data = null;

            base.HideView();
        }

        /// <summary>
        /// Method called by Replay Button.
        /// </summary>
        public void ReplayClick()
        {
            OnReplayClicked?.Invoke();
        }

        /// <summary>
        /// Method called by Menu Button.
        /// </summary>
        public void MenuClick()
        {
            OnMenuClicked?.Invoke();
        }
    }

    public class GameView : BaseView
    {
        // Events to attach to.
        public Action OnPauseClicked;
        public Action OnFinishClicked;

        /// <summary>
        /// Method called by Pause Button.
        /// </summary>
        public void PauseClick()
        {
            OnPauseClicked?.Invoke();
        }

        /// <summary>
        /// Method called by Finish Button.
        /// </summary>
        public void FinishClick()
        {
            OnFinishClicked?.Invoke();
        }
    }

    public class MenuView : BaseView
    {
        // Events to attach to.
        public Action OnStartClicked;
        public Action OnQuitClicked;

        /// <summary>
        /// Method called by Start Button.
        /// </summary>
        public void StartClick()
        {
            OnStartClicked?.Invoke();
        }

        /// <summary>
        /// Method called by Quit Button.
        /// </summary>
        public void QuitClick()
        {
            OnQuitClicked?.Invoke();
        }
    }

    public class PauseView : BaseView
    {
        // Events to attach to.
        public Action OnResumeClicked;
        public Action OnMenuClicked;

        /// <summary>
        /// Method called by Resume Button.
        /// </summary>
        public void ResumeClick()
        {
            OnResumeClicked?.Invoke();
        }

        /// <summary>
        /// Method called by Menu Button.
        /// </summary>
        public void MenuClick()
        {
            OnMenuClicked?.Invoke();
        }
    }

} // end namespace
