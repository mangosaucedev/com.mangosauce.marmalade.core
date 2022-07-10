using System;
using System.Collections;
using System.Collections.Generic;

namespace Marmalade
{
    public interface IHeapItem<T> : IComparable<T>
    {
        int HeapIndex { get; set; }
    }
}
