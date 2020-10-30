using Dreamteck.Splines;
using Game.Utils;
using UnityEngine;

namespace Game
{
    public class SquareDimention : MonoBehaviour
    {
        [SerializeField] private SplineFollower _starFollower;
        [SerializeField] private float _starStartPosition;
        [SerializeField] private Spline.Direction _starStartDirection;

        private StarBehaviour _starBehavoir;
        private float _lastSpeed;
        private void Start()
        {
            // StarBehaviour.StarSelectedAction += OnStarSelected;
            _starFollower.followSpeed = 0;
            SetSplinePosition(_starStartPosition);
            SetDirection(_starStartDirection);
            
            _starBehavoir = _starFollower.GetComponent<StarBehaviour>();
            // StarAppear();
        }

        public  void StarAppear()
        {
            _starBehavoir.AppearAct();
            SetSplinePosition(RandomFloat(0, 6f));
            SetRandomDirection();
            SetSpeed(GetRandomeSpeed());
            
            if (GameManager.Instance.GameState == GameState.Standby)
            {
                SessionPaused();
            }
        }

        void StarDissappear()
        {
            _starBehavoir.Dissappear();
        }

        float RandomFloat(float a, float b)
        {
            return Random.Range(a, b);
        }

        float GetRandomeSpeed()
        {
            var speed = RandomFloat(0.01f, 0.2f);
            _lastSpeed = speed;
            return speed;
        }

        bool RandomBool()
        {
             return (Random.Range(0, 2) == 1); 
        }
        
        void SetSpeed(float speed)
        {
            _starFollower.followSpeed = speed;
        }
        
        void SetSplinePosition(float position)
        {
            _starFollower.SetDistance(position);
        }


        void SetRandomDirection()
        {
            if (RandomBool())
            {
                SetDirection(Spline.Direction.Backward);
            }
            else
            {
                SetDirection(Spline.Direction.Forward);
            }
        }
        
        void SetDirection(Spline.Direction direction)
        {
            _starFollower.direction = direction;
        }

        public void StopGameSession()
        {
            StarDissappear();
            SetSpeed(0);
        }

        public void SessionPaused()
        {
            SetSpeed(0);
        }

        public void ResumeSession()
        {
            SetSpeed(_lastSpeed);
        }
        public async void OnStarSelected()
        {
            SetSpeed(0);
            GameManager.Instance.StarCollected();
            await UniTaskUtils.Delay(2);
            if (GameManager.Instance.GameState == GameState.None)
            {
               return;
            }
            StarAppear();
        }
    }
}
