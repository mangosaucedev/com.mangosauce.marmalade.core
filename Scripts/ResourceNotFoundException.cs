using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(Type type) : 
            base(message: $"Resource of type {type.Name} not found.")
        {

        }

        public ResourceNotFoundException(Guid guid, Type type) : 
            base(message: $"Resource of type {type.Name} not found with Guid {guid}.")
        {

        }
    }
}
