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
        private Rectangle boundaries;
        private int speed = 10;

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

        public PlayerShip(Rectangle boundaries)
        {
            this.boundaries = boundaries;
            Alive = true;
            image = Properties.Resources.player;
            Location = new Point((boundaries.Width - image.Width) / 2, boundaries.Height - image.Height - 10);
        }

        /// <summary>
        /// Draws the player's ship normal or if it is blowing up.
        /// </summary>
        /// <param name="g">The surface to draw onto.</param>
        public void Draw(Graphics g)
        {
            if (Alive)
                g.DrawImageUnscaled(Properties.Resources.player, Location);
                
        }

        /// <summary>
        /// Moves the player's ship left or right.
        /// </summary>
        /// <param name="direction">The direction for the player's ship to move.</param>
        public void Move(Direction direction)
        {
            int newXLocation;
            switch (direction)
            {
                //If the player's new location is within the boundaries of the surface being drawn to, the player can move.
                case Direction.Left:
                    newXLocation = Location.X - speed;
                    if (newXLocation > boundaries.X)
                        Location = new Point(Location.X - speed, Location.Y);
                    break;

                //If the player's new location is within the boundaries of the surface being drawn to, the player can move.
                case Direction.Right:
                    newXLocation = Location.X + speed;
                    if (newXLocation + image.Width < boundaries.Right)
                        Location = new Point(Location.X + speed, Location.Y);
                    break;
            }
        }
    }
}
