using System;
using Game.Utils;
using Mumi;
using UniRx.Async;
using UnityEngine;

namespace Game
{
    public class TimeManager : SingletonT<TimeManager>
    {
        [SerializeField] private TimeProgressBar _progressBar;
        [Range(1f, 10f)] [SerializeField] private float _updateRateSeconds = 1f;
        [SerializeField] private float _gameDurationSeconds = 30f;
        [SerializeField] private float _regressionProgress;
        private bool _isPlaying = false;

        private float _secLeftPaused;

        // private async void Start()
        // {
        //     await UpdateTimeProgress();
        // }

        private async UniTask UpdateTimeProgress()
        {
            while (_isPlaying)
            {
                CalculateTimerProgress();
                await UniTaskUtils.Delay(_updateRateSeconds);
            }
        }

        private void CalculateTimerProgress()
        {
            double secondsLeft = new TimeSpan(LocalSettings.AvailableTimeToPlay.Ticks - DateTime.UtcNow.Ticks)
                .TotalSeconds;

            _secLeftPaused = (float)secondsLeft;

            if (secondsLeft <= 0)
            {
                _isPlaying = false;

                GameManager.Instance.GameSessionDone();
                return;
            }
            
            _progressBar.SetProgress(
                GetTimerProgress((float) secondsLeft), (int) secondsLeft);

        }

        private float GetTimerProgress(float secundesLeft)
        {
            var secLeft = secundesLeft / _gameDurationSeconds - _regressionProgress;
            return secLeft;
        }

        public async void ResumeTimer()
        {
            
            LocalSettings.AvailableTimeToPlay = DateTime.UtcNow.AddSeconds(_secLeftPaused);
            _isPlaying = true;

            await UpdateTimeProgress();
            // CalculateTimerProgress();
        }

        public void TimerOnPause()
        {
            _isPlaying = false;
        }
        
        public async void StartPlay()
        {
            Debug.Log("time StartPlay");
            _isPlaying = true;

            LocalSettings.AvailableTimeToPlay = DateTime.UtcNow.AddSeconds(_gameDurationSeconds);
            await UpdateTimeProgress();
            // CalculateTimerProgress();
        }
    }
}