using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Invaders
{
    class Shot
    {
        private const int MOVEINTERVAL = 20, WIDTH = 5, HEIGHT = 15;

        /// <summary>
        /// The location of the shot.
        /// </summary>
        public Point Location { get; private set; }

        private Direction direction; //The direction the shot was fired toward.
        private Rectangle boundaries; //The boundaries of the play area.

        /// <summary>
        /// Create a shot.
        /// </summary>
        /// <param name="location">The location the shot is fired from.</param>
        /// <param name="direction">The direction the shot is fired in.</param>
        /// <param name="boundaries">The boundaries of the play area.</param>
        public Shot(Point location, Direction direction, Rectangle boundaries)
        {
            this.Location = location;
            this.direction = direction;
            this.boundaries = boundaries;
        }

        /// <summary>
        /// Move the shot.
        /// </summary>
        /// <returns>Returns true if the shot didn't move out of bounds.</returns>
        public bool Move()
        {
            switch(direction)
            {
                //The shot is moving up, move it up and then check if it moved outside of the boundaries.
                case Direction.Up:
                    Location = new Point(Location.X, Location.Y - MOVEINTERVAL);
                    if (Location.Y + HEIGHT < boundaries.Y)
                        return false;
                    else
                        return true;

                //The shot is moving down, move it down and then check if it moved outside of the boundaries.
                case Direction.Down:
                    Location = new Point(Location.X, Location.Y + MOVEINTERVAL);
                    if (Location.Y > boundaries.Bottom)
                        return false;
                    else
                        return true;

                default: return true;
            }
        }

        /// <summary>
        /// Draw the shot.
        /// </summary>
        /// <param name="g">The play area to draw to.</param>
        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.Yellow, Location.X, Location.Y, WIDTH, HEIGHT);
        }
    }
}
