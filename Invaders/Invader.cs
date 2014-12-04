using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Invaders
{
    class Invader
    {
        public Point Location { get; private set; }

        public Rectangle Area { get; }

        internal void Move(Direction direction)
        {
            throw new NotImplementedException();
        }

        internal void Draw(System.Drawing.Graphics g, int animationCell)
        {
            //throw new NotImplementedException();
        }
    }
}
