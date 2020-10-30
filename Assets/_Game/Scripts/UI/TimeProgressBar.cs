using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    
    public class TimeProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _progressImage;
        [SerializeField] private TextMeshProUGUI _secondsLeftText;
        [SerializeField] private float _disabledAlpha;
        [SerializeField] private string _secondsPrefix = "s";

        private bool _lockedByLevel = false;
    
        public float Progress { get; private set; }


        public void SetProgress(float progress, int secondsLeft)
        {
            _progressImage.fillAmount = 
                Progress = progress;

            var inProgress = float.Epsilon < progress;
        
            // SetProgressEnabled(inProgress);
            // SetImageFade(inProgress);
            SetSecondsLeft(secondsLeft);
        }

        public void SetProgressEnabled(bool isEnabled)
        {
            _secondsLeftText.gameObject.SetActive(isEnabled);
        }
    
        private void SetImageFade(bool isDisabled)
        {
            _progressImage.DOFade(isDisabled
                    ? _disabledAlpha
                    : 1f, 
                0f);
        }
    
        private void SetSecondsLeft(int secondsLeft)
        {
            _secondsLeftText.text = $"{secondsLeft}{_secondsPrefix}";
        }
    }
   
}