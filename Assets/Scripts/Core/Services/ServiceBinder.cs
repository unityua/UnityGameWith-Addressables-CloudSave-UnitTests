using UnityEngine;

namespace PesPatron.Core
{
    public abstract class ServiceBinder : MonoBehaviour
    {
        public abstract void BindServices(ServiceLocator serviceLocator);
    }
}