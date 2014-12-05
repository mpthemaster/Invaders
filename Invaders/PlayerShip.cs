using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Invaders
{
    class PlayerShip
    {
        private Bitmap image; //The image of the player's ship.

        //Whether or not the player's ship is alive.
        public bool Alive { get; set; }

        public Point Location { get; private set; }

        public Rectangle Area
        {
            get
            {
                return new Rectangle(Location, image.Size);
            }
        }

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
