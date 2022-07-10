using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Marmalade
{
    public class GenericGrid<T> : IEnumerable<T>
    {
        public uint width;
        public uint height;
        public T[,] cells;

        public T this[uint x, uint y]
        {
            get => Get(x, y);
            set => Set(x, y, value);
        }

        public GenericGrid(uint width, uint height)
        {
            this.width = width;
            this.height = height;
            cells = new T[width, height];
        }

        public bool IsWithinBounds(uint x, uint y) => x < width && y < height;

        public T Get(uint x, uint y)
        {
            if (IsWithinBounds(x, y))
                return cells[x, y];
            Debug.WriteLine($"Could not get grid position {x},{y}; out of bounds!");
            return default;
        }

        public void Set(uint x, uint y, T value)
        {
            if (IsWithinBounds(x, y))
                cells[x, y] = value;
            else
                Debug.WriteLine($"Could not set grid position {x},{y}; out of bounds!");
        }

        public IEnumerator<T> GetEnumerator() => new GridEnumerator<T>(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void CopyTo(GenericGrid<T> grid)
        {
            uint width = (uint)Math.Min((int)this.width, (int)grid.width);
            uint height = (uint)Math.Min((int)this.height, (int)grid.height);

            for (uint x = 0; x < width; x++)
                for (uint y = 0; y < height; y++)
                    grid[x, y] = Get(x, y);
        }
    }
}