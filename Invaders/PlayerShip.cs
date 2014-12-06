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
        private int speed = 10; //How fast the ship travels.
        private DateTime shipDeath; //The time that the player's ship has been shot. Allows a death animation to occur for 3 seconds.
        private int deadShipHeight; //The ship is animated as being destroyed by shrinking out of existence.

        /// <summary>
        /// Whether or not the player's ship is alive.
        /// </summary>
        public bool Alive 
        { 
            get
            {
                return alive;
            }

            set 
            {
                shipDeath = DateTime.Now; //For keeping track of when the player's ship was hit by an invader's shot.
                alive = value;
            }
        }
        private bool alive;

        /// <summary>
        /// Where the player's ship is located on the drawing surface.
        /// </summary>
        public Point Location { get; private set; }

        /// <summary>
        /// The area that the player's ship takes up.
        /// </summary>
        public Rectangle Area
        {
            get
            {
                return new Rectangle(Location, image.Size);
            }
        }

        /// <summary>
        /// Create a player's ship.
        /// </summary>
        /// <param name="boundaries">The boundaries of the play area.</param>
        public PlayerShip(Rectangle boundaries)
        {
            this.boundaries = boundaries;
            Alive = true;
            image = Properties.Resources.player;
            Location = new Point((boundaries.Width - image.Width) / 2, boundaries.Height - image.Height - 10);
            deadShipHeight = image.Height;
        }

        /// <summary>
        /// Draws the player's ship normal or if it is blowing up.
        /// </summary>
        /// <param name="g">The surface to draw onto.</param>
        public void Draw(Graphics g)
        {
            //If the ship is alive, draw it.
            //Else the ship has been hit, so animate it becoming destroyed by shrinking it.
            if (Alive)
                g.DrawImageUnscaled(Properties.Resources.player, Location);
            else
            {
                //If the height of the player's ship is greater than 0, decrease it's height and draw it further destroyed.
                if (deadShipHeight > 0)
                {
                    //As the height of the ship is decreased, it's y-coordinate is increased to maintain its bottom location.
                    deadShipHeight--;
                    g.DrawImage(image, Location.X, Location.Y + image.Height - deadShipHeight, image.Width, deadShipHeight);
                }

                //If three seconds has passed, the ship is alive again and gameplay can continue.
                if (DateTime.Now - shipDeath >= new TimeSpan(0, 0, 3))
                {
                    Alive = true;
                    deadShipHeight = image.Height;
                }
            }
                
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
