using System.Drawing;

namespace Invaders
{
    class Invader
    {
        //The number of pixels to move horizontally and/or vertically.
        private const int HORIZONTALINTERVAL = 10;
        private const int VERTICALINTERVAL = 40;

        private Bitmap image; //The image of the invader.

        /// <summary>
        /// Where the invader is currently located.
        /// </summary>
        public Point Location { get; private set; }

        /// <summary>
        /// The type of ship which determines how many points to give the player on its destruction.
        /// </summary>
        public ShipType InvaderType { get; private set; }

        public Rectangle Area
        {
            get
            {
                return new Rectangle(Location, image.Size);
            }
        }

        /// <summary>
        /// The number of points destroying this invader gets the player.
        /// </summary>
        public int Score { get; private set; }

        /// <summary>
        /// Creates an invader.
        /// </summary>
        /// <param name="shipType">The type of ship the invader is.</param>
        /// <param name="location">The location to spawn the invader at.</param>
        /// <param name="score">The number of points this invader is worth.</param>
        public Invader(ShipType shipType, Point location)
        {
            InvaderType = shipType;
            Location = location;
            DetermineScore();
            image = InvaderImage(0);
        }

        /// <summary>
        /// Determines how many points this invader is worth based on its ship type.
        /// </summary>
        private void DetermineScore()
        {
            switch (InvaderType)
            {
                case ShipType.Bug: Score = 40; break;
                case ShipType.Satellite: Score = 50; break;
                case ShipType.Saucer: Score = 30; break;
                case ShipType.Spaceship: Score = 20; break;
                case ShipType.Star: Score = 10; break;
            }
        }

        /// <summary>
        /// Gets the current image of the invader.
        /// </summary>
        /// <param name="animationCell">The specific image to use.</param>
        /// <returns></returns>
        private Bitmap InvaderImage(int animationCell)
        {
            switch (animationCell)
            {
                case 0:
                    switch(InvaderType)
                    {
                        case ShipType.Bug:
                            return Properties.Resources.bug1;

                        case ShipType.Satellite:
                            return Properties.Resources.satellite1;

                        case ShipType.Saucer:
                            return Properties.Resources.flyingsaucer1;

                        case ShipType.Spaceship:
                            return Properties.Resources.spaceship1;

                        case ShipType.Star:
                            return Properties.Resources.star1;
                    }
                    break;

                case 1:
                    switch (InvaderType)
                    {
                        case ShipType.Bug:
                            return Properties.Resources.bug2;

                        case ShipType.Satellite:
                            return Properties.Resources.satellite2;

                        case ShipType.Saucer:
                            return Properties.Resources.flyingsaucer2;

                        case ShipType.Spaceship:
                            return Properties.Resources.spaceship2;

                        case ShipType.Star:
                            return Properties.Resources.star2;
                    }
                    break;

                case 2:
                    switch (InvaderType)
                    {
                        case ShipType.Bug:
                            return Properties.Resources.bug3;

                        case ShipType.Satellite:
                            return Properties.Resources.satellite3;

                        case ShipType.Saucer:
                            return Properties.Resources.flyingsaucer3;

                        case ShipType.Spaceship:
                            return Properties.Resources.spaceship3;

                        case ShipType.Star:
                            return Properties.Resources.star3;
                    }
                    break;

                case 3:
                    switch (InvaderType)
                    {
                        case ShipType.Bug:
                            return Properties.Resources.bug4;

                        case ShipType.Satellite:
                            return Properties.Resources.satellite4;

                        case ShipType.Saucer:
                            return Properties.Resources.flyingsaucer4;

                        case ShipType.Spaceship:
                            return Properties.Resources.spaceship4;

                        case ShipType.Star:
                            return Properties.Resources.star4;
                    }
                    break;
            }
            return null;
        }

        /// <summary>
        /// Moves the invader.
        /// </summary>
        /// <param name="direction">The direction to move the invader towards.</param>
        public void Move(Direction direction)
        {
            switch(direction)
            {
                case Direction.Down:
                    Location = new Point(Location.X, Location.Y + VERTICALINTERVAL);
                    break;

                case Direction.Left:
                    Location = new Point(Location.X - HORIZONTALINTERVAL, Location.Y);
                    break;

                case Direction.Right:
                    Location = new Point(Location.X + HORIZONTALINTERVAL, Location.Y);
                    break;

                case Direction.Up:
                    Location = new Point(Location.X, Location.Y + VERTICALINTERVAL);
                    break;
            }
        }

        /// <summary>
        /// Draws the invader.
        /// </summary>
        /// <param name="g">The drawing surface to draw the invader onto.</param>
        /// <param name="animationCell">The specific animation image to use.</param>
        public void Draw(System.Drawing.Graphics g, int animationCell)
        {
            g.DrawImageUnscaled(InvaderImage(animationCell), Location);
        }
    }
}
