using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Invaders
{
    class Shot
    {
        public Point Location { get; private set; }

        public Shot(Point location, Direction direction, Rectangle boundaries)
        {

        }

        public bool Move()
        {
            //throw new NotImplementedException();
            return false;
        }

        internal void Draw(System.Drawing.Graphics g)
        {
            //throw new NotImplementedException();
        }
    }
}
