using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src.Entities.Models
{
    public class Coordinate
    {
        private int x;
        private int y;

        public Coordinate()
        {
            x = 0; y = 0;
        }
        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            Coordinate other = (Coordinate)obj;
            if(x == other.x && y == other.y) return true;
            return false;
        }
    }
}
