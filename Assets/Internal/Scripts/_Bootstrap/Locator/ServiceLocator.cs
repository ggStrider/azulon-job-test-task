using System;
using System.Collections.Generic;
using UnityEngine;

namespace Internal.Scripts._Bootstrap.Locator
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new();

        #region Registration

        public static bool Register<T>(T to) where T : class
        {
            var type = typeof(T);
            var isAdded = _services.TryAdd(type, to);

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (!isAdded)
            {
                Debug.LogWarning($"[{nameof(ServiceLocator)}] Service {typeof(T).Name} already registered! "
                    + "Skipping... If you want to register it - unregister firstly!");
            }
#endif

            return isAdded;
        }

        public static bool Unregister<T>() where T : class
        {
            if (TryGet<T>(out var service))
            {
                var type = typeof(T);
                _services.Remove(type);

                return true;
            }

            return false;
        }

        #endregion

        #region Get

        public static bool TryGet<T>(out T serviceToGet) where T : class
        {
            serviceToGet = default;

            var type = typeof(T);
            if (_services.TryGetValue(type, out var raw))
            {
                serviceToGet = (T)raw;
                return true;
            }

            return false;
        }

        public static T Get<T>() where T : class
        {
            if (TryGet<T>(out var serviceToGet))
            {
                return serviceToGet;
            }

            throw new InvalidOperationException($"[{nameof(ServiceLocator)}] Service " +
                $"{typeof(T).Name} is not registered!");
        }

        #endregion

        #region Other

        public static void Clear()
        {
            _services.Clear();
        }

        #endregion
    }
}