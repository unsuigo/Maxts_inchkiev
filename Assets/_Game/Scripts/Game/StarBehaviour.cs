using Game.Utils;
using UnityEngine;

namespace Game
{
    public class StarBehaviour : MonoBehaviour
    {
        // public static event Action StarSelectedAction;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private GameObject _appearFX;
        [SerializeField] private SquareDimention _rootDimention;

        private void Start()
        {
            SetAlpha(0f);
        }

        public async void AppearAct()
        {
            // _appearFX.GetComponent<ParticleSystem>().Play();
            await UniTaskUtils.Delay(0.1f);
            SetAlpha(1.0f);
            GetComponent<SphereCollider>().enabled = true;
        }

        void SetAlpha(float val)
        {
            var color = _renderer.material.color;
            color.a = val;
            _renderer.material.color = color;
            
        }

        public async void Dissappear()
        {
            // _appearFX.GetComponent<ParticleSystem>().Play();
            await UniTaskUtils.Delay(0.1f);
            SetAlpha(0f);
            GetComponent<SphereCollider>().enabled = false;
        }

       

        public void OnMouseDown()
        {
            StarSelected();
        }

        public  void StarSelected()
        {
            if (GameManager.Instance.GameState != GameState.Playing)
            {
                return;
            }
            // _appearFX.GetComponent<ParticleSystem>().Play();
            AudioSystem.Instance.PlayOneShot(AudioClips.HitStar);

            Dissappear();
            _rootDimention.OnStarSelected();
            // StarSelectedAction?.Invoke();
        }

    }
}
