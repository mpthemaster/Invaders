using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Invaders
{
    class PlayerShip
    {
        public bool Alive { get; set; }

        public Point Location { get; private set; }

        public Rectangle Area { get; }

        internal void Draw(System.Drawing.Graphics g)
        {
            //throw new NotImplementedException();
        }

        internal void Move(Direction direction)
        {
            throw new NotImplementedException();
        }
    }
}
