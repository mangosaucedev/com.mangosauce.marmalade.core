using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade
{
    public class Resources : MonoBehaviour
    {
        private static Dictionary<Type, IResource> servicesByType = new Dictionary<Type, IResource>();
        private static Dictionary<Guid, IResource> servicesByGuid = new Dictionary<Guid, IResource>();

        public static T Get<T>() where T : IResource
        {
            try
            {
                return (T)servicesByType[typeof(T)];
            }
            catch
            {
                throw new ResourceNotFoundException(typeof(T));
            }
        }

        public static T Get<T>(Guid guid) where T : IResource
        {
            try
            {
                return (T)servicesByGuid[guid];
            }
            catch
            {
                throw new ResourceNotFoundException(guid, typeof(T));
            }
        }

        public static bool TryGet<T>(out T resource) where T : IResource
        {
            resource = default(T);
            if (servicesByType.TryGetValue(typeof(T), out IResource r))
            {
                resource = (T) r;
                return true;
            }
            return false;
        }

        public static bool TryGet<T>(Guid guid, out T resource) where T : IResource
        {
            resource = default(T);
            if (servicesByGuid.TryGetValue(guid, out IResource r))
            {
                resource = (T) r;
                return true;
            }               
            return false;
        }

        public static Guid Register(IResource resource)
        {
            servicesByGuid[resource.Guid] = resource;
            return resource.Guid;
        }

        public static Guid Register<T>(IResource resource)
        {
            servicesByType[typeof(T)] = resource;
            return Register(resource);
        }

        public static Guid Unregister(IResource resource)
        {
            servicesByGuid.Remove(resource.Guid);
            return resource.Guid;
        }

        public static Guid Unregister<T>(IResource resource)
        {
            servicesByType.Remove(typeof(T));
            return Unregister(resource);
        }
    }
}