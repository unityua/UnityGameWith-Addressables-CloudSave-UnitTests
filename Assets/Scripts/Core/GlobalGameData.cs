using System.Collections.Generic;

namespace PesPatron.Core
{
    public class GlobalGameData
    {
        private bool _webServicesInitialized;
        private string _playerId;

        public bool WebServicesInitialized => _webServicesInitialized;
        public string PlayerId => _playerId;

        public void SetWebData(string playerId)
        {
            _playerId = playerId;
            _webServicesInitialized = true;
        }
    }
}