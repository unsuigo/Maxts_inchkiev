using UniRx;
using UnityEngine;

namespace Game
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private string _starTag = "Star";
        [SerializeField] private Camera _camera;


        private void Awake()
        {
            //_____Update_____
            Observable.EveryUpdate()
                .Subscribe(x => Inputs())
                .AddTo(this);
        }

        void Inputs()
        {
#if UNITY_EDITOR
#else
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                var ray = _camera.ScreenPointToRay(Input.touches[0].position);
                RaycastHit hit;
                if (Physics.Raycast(ray,out hit))
                {
                     
                    var selection = hit.transform;
                    if (selection.CompareTag(_starTag))
                    {
                        var selectionBihaviour = selection.GetComponent<StarBehaviour>();
                        if (selection != null)
                        {
                            selectionBihaviour.StarSelected();
                        }
                    }
                }
                      
            }
#endif
        }
    }

}
