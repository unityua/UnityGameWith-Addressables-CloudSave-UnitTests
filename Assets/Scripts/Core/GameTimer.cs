using UnityEngine;

namespace PesPatron.Core
{
    public class GameTimer : MonoBehaviour
    {
        private float _passedTime;

        private bool _isWorking = true;

        public float PassedTime => _passedTime; 

        private void Update()
        {
            if (_isWorking == false)
                return;

            _passedTime += Time.deltaTime;
        }

        public void Restart()
        {
            _passedTime = 0f;
            _isWorking = true;
        }

        public void Stop()
        {
            _isWorking = false;
        }

        public void Resume()
        {
            _isWorking = true;
        }
    }
}