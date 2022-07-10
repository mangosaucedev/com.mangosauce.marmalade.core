using System.Collections;
using System.Collections.Generic;

namespace Marmalade
{
    public class GridEnumerator<T> : IEnumerator<T>
    {
        private GenericGrid<T> grid;
        private int x;
        private int y;

        private uint Width => grid.width;

        private uint Height => grid.height;

        public T Current => grid[(uint)x, (uint)y];

        object IEnumerator.Current => Current;

        public GridEnumerator(GenericGrid<T> grid)
        {
            this.grid = grid;
            Reset();
        }

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            x++;
            if (x == Width)
            {
                x = 0;
                y++;
            }
            if (y == Height)
                return false;
            return true;
        }

        public void Reset()
        {
            x = -1;
            y = 0;
        }
    }
}