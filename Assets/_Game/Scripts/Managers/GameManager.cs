using System;
using Game.Utils;
using TMPro;
using UnityEngine;


namespace Game
{
    public class GameManager : SingletonT<GameManager>
    {
        public static event Action sessionDoneAction;
        [SerializeField] private SquareDimention[] _dimentionArray;
        [SerializeField] private GameObject _timeerPanel;
        [SerializeField] private TextMeshProUGUI _score;

        private int _starsCollectedQty = 0;

        public GameState GameState{ get; set;}

        void Start()
        {
            GameState = GameState.None;
            _timeerPanel.SetActive(false);
            AudioSystem.Instance.PlayMusic(AudioClips.MainTrack);
        }

        public void StartGameSession()
        {
            _starsCollectedQty = 0;
            SetScoreText(0);
            _timeerPanel.SetActive(true);
            GameState = GameState.Playing;
            foreach (var dimention in _dimentionArray)
            {
                dimention.StarAppear();

            }
            TimeManager.Instance.StartPlay();
        }
        
        public void GameSessionDone()
        {
            AudioSystem.Instance.PlayOneShot(AudioClips.GameEnd);

            _timeerPanel.SetActive(false);
            foreach (var dimention in _dimentionArray)
            {
                dimention.StopGameSession();

            }
            GameState = GameState.None;
            Debug.Log("time finished");
            LocalSettings.StarsGotQty = _starsCollectedQty;
            sessionDoneAction?.Invoke();
        }
        
        public void StarCollected()
        {
            _starsCollectedQty++;
            SetScoreText(_starsCollectedQty);
        }

        void SetScoreText(int score)
        {
            _score.text = $"{score}";
        }

        public void GameSessionPaused()
        {
            TimeManager.Instance.TimerOnPause();
            foreach (var dimention in _dimentionArray)
            {
                dimention.SessionPaused();
            }
        }

        public void GameSessionResumed()
        {
            TimeManager.Instance.ResumeTimer();
            foreach (var dimention in _dimentionArray)
            {
                dimention.ResumeSession();
            }
        }
    }
    

    public enum GameState
    {
        Playing,
        Standby,
        None
    }
}