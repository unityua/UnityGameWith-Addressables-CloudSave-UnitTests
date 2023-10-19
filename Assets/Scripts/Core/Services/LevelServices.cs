using System;

namespace PesPatron.Core
{
    public class LevelServices : ServiceLocator
    {
        private static LevelServices _instance;

        protected override bool InitializeLocator()
        {
            if(_instance == null)
            {
                _instance = this;
                return true;
            }
            else
            {
                throw new Exception("More Then One LevelServices Detected On Scene. Remove One Of Them Or Combine To Single!");
            }
        }

        public static T GetService<T>()
        {
            return _instance.Get<T>();
        }
    }
}