using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI
{
   
    public class PlayButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image _sourceImage;
        [SerializeField] private Sprite _playImage;
        [SerializeField] private Sprite _pauseImage;
        // private bool isPlaying = false;

        private void Start()
        {
            GameManager.sessionDoneAction += OnSessionDone;
            _sourceImage.sprite = _playImage;
        }

        private void OnSessionDone()
        {
            _sourceImage.sprite = _playImage;
        }

        void ButtonClick()
        {
            AudioSystem.Instance.PlayOneShot(AudioClips.ButtonClick);
            if( GameManager.Instance.GameState == GameState.Playing)
            {
                GameManager.Instance.GameState = GameState.Standby;
                _sourceImage.sprite = _playImage;

                GameManager.Instance.GameSessionPaused();
            } 
            else if( GameManager.Instance.GameState == GameState.Standby)
            {
                GameManager.Instance.GameState = GameState.Playing;
                _sourceImage.sprite = _pauseImage;

                GameManager.Instance.GameSessionResumed();
            }
            else
            {
                GameManager.Instance.GameState = GameState.Playing;
                _sourceImage.sprite = _pauseImage;

                GameManager.Instance.StartGameSession();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ButtonClick();
        }
    }

}