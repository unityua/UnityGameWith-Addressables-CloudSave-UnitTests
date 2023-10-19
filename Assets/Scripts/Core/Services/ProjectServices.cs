using UnityEngine;

namespace PesPatron.Core
{
    public class ProjectServices : ServiceLocator
    {
        private static ProjectServices _instance;

        protected override bool InitializeLocator()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this);
                return true;
            }
            else
            {
                Destroy(gameObject);
                return false;
            }
        }

        public static T GetService<T>()
        {
            return _instance.Get<T>();
        }
    }
}