using UnityEngine;

namespace Game.Utils
{
    public class SingletonT<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
	            if (_instance != null)
	            {
		            return _instance;
	            }
	            
	            _instance = FindObjectOfType<T>();
	            return _instance != null 
		            ? _instance 
		            : null;
            }
        }
    }
}