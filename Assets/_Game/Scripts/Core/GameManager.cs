using UnityEngine;

namespace KingdomOfCloud.Core
{
    public enum GameRunState
    {
        Boot,
        Playing,
        Paused,
    }

    /// <summary>
    /// 全局运行状态：暂停、时间缩放、退出。挂在常驻物体（如 GameSystems）上即可。
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private KeyCode pauseKey = KeyCode.Escape;
        [SerializeField] private bool pauseOnStart;

        public GameRunState State { get; private set; } = GameRunState.Boot;

        public bool IsPaused => State == GameRunState.Paused;

        protected override void Awake()
        {
            base.Awake();
            if (Instance != this)
                return;

            State = pauseOnStart ? GameRunState.Paused : GameRunState.Playing;
            ApplyTimeScale();
        }

        private void Update()
        {
            if (Input.GetKeyDown(pauseKey))
                TogglePause();
        }

        public void SetPlaying()
        {
            State = GameRunState.Playing;
            ApplyTimeScale();
        }

        public void SetPaused()
        {
            State = GameRunState.Paused;
            ApplyTimeScale();
        }

        public void TogglePause()
        {
            if (State == GameRunState.Paused)
                SetPlaying();
            else if (State == GameRunState.Playing)
                SetPaused();
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void ApplyTimeScale()
        {
            Time.timeScale = IsPaused ? 0f : 1f;
        }
    }
}
