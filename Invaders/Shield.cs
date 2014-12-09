using System;
using System.Drawing;

namespace Invaders
{
    /// <summary>
    /// A shield that can be placed to protect the player from invader shots. Player shots can't go through it, but they don't hurt it. Only invader
    /// shots can destroy it.
    /// </summary>
    class Shield
    {
        /// <summary>
        /// HITS: How many hits the shield can take before it is destroyed.
        /// WIDTH: The width of the rectangle that represents the shield.
        /// HEIGHT: The height of the rectangle that represents the shield.
        /// </summary>
        private const int HITS = 5;
        public static int WIDTH { get {return 80; }}
        public static int HEIGHT { get { return 20; } }

        public Point Location { get; private set; } //The location of the shield.
        private int destroyedShieldHeight = HEIGHT; //The height of the shield as it is being destroyed.
        private int hit; //The number of shots that have hit the shield.

        /// <summary>
        /// The area that the shield takes up.
        /// </summary>
        public Rectangle Area { get; private set; }

        public event EventHandler OnShieldDestroyed;

        public Shield(Point location)
        {
            Location = location;
            Area = new Rectangle(Location, new Size(WIDTH, HEIGHT));
        }

        /// <summary>
        /// Hits the shield with an invader shot.
        /// </summary>
        public void Hit()
        {
            hit++;
        }

        /// <summary>
        /// Draw the shield.
        /// </summary>
        /// <param name="g">The play area to draw to.</param>
        public void Draw(Graphics g)
        {
            //If the shield has not been destroyed, draw it like normal.
            //Else the shield has been destroyed, so animate it being destroyed.
            if (hit < HITS)
                g.FillRectangle(Brushes.White, Location.X, Location.Y, WIDTH, HEIGHT);
            else
            {
                if (destroyedShieldHeight > 0)
                {
                    destroyedShieldHeight--;
                    g.FillRectangle(Brushes.White, Location.X, Location.Y + HEIGHT - destroyedShieldHeight, WIDTH, destroyedShieldHeight);
                }
                else
                    OnShieldDestroyed(this, new EventArgs());
            }
        }
    }
}
