using System;
using System.Collections;
using System.Collections.Generic;

namespace Marmalade
{
    public class HeapException : Exception 
    {
        public HeapException() : base()
        {

        }

        public HeapException(string message) : base(message)
        {

        }
    }
}
