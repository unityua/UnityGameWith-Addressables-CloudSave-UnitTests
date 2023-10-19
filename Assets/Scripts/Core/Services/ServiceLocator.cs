using System;
using System.Collections.Generic;
using UnityEngine;

namespace PesPatron.Core
{
    public abstract class ServiceLocator : MonoBehaviour
    {
        [SerializeField] private ServiceBinder[] _serviceBinders;

        protected readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        private void Awake()
        {
            bool initialized = InitializeLocator();

            if (initialized)
                BindAllServices();
        }

        protected abstract bool InitializeLocator();

        public void BindAs<T, I>(T service) where T : I
        {
            _services.Add(typeof(I), service);
        }

        public void Bind<T>(T service)
        {
            _services.Add(typeof(T), service);
        }

        public T Get<T>()
        {
            return (T)_services[typeof(T)];
        }

        private void BindAllServices()
        {
            foreach (var serviceBinder in _serviceBinders)
            {
                serviceBinder.BindServices(this);
            }
        }
    }
}