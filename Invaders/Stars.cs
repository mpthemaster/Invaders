using System;
using System.Collections.Generic;
using System.Drawing;

namespace Invaders
{
    class Stars
    {
        /// <summary>
        /// A star, made up of a location and a color.
        /// </summary>
        private struct Star
        {
            public Point location;
            public Pen color;

            /// <summary>
            /// Create a star.
            /// </summary>
            /// <param name="location">The location of the star.</param>
            /// <param name="color">The color of the star.</param>
            public Star(Point location, Pen color)
            {
                this.location = location;
                this.color = color;
            }
        }

        private List<Star> stars; //300 stars.
        private Rectangle boundaries;
        private Random random;

        /// <summary>
        /// Create stars.
        /// </summary>
        public Stars(Rectangle boundaries, Random random)
        {
            this.boundaries = boundaries;
            this.random = random;
            stars = new List<Star>(300);
            CreateStars(300);
        }

        /// <summary>
        /// Creates 300 stars.
        /// </summary>
        private void CreateStars(int numberOfStars)
        {
            for (int i = 0; i < numberOfStars; i++)
            {
                Point newLocation = new Point(random.Next(boundaries.Right), random.Next(boundaries.Bottom));
                stars.Add(new Star(newLocation, RandomPen()));
            }
        }

        /// <summary>
        /// Generates a pen with a random color.
        /// </summary>
        /// <returns>A pen with a random color.</returns>
        private Pen RandomPen()
        {
            switch(random.Next(4))
            {
                case 0: return Pens.AliceBlue;

                case 1: return Pens.AntiqueWhite;

                case 2: return Pens.Aqua;

                case 3: return Pens.Aquamarine;

                default: return Pens.Azure;
            }
        }

        /// <summary>
        /// Draw 300 stars.
        /// </summary>
        /// <param name="g">The play area to draw onto.</param>
        public void Draw(Graphics g)
        {
            foreach (Star star in stars)
                g.DrawRectangle(star.color, star.location.X, star.location.Y, 1, 1);
        }

        /// <summary>
        /// Remove 5 stars and add 5 new stars.
        /// </summary>
        public void Twinkle()
        {
            for (int i = 0; i < 5; i++)
                stars.RemoveAt(random.Next(300 - i));
            CreateStars(5);
        }
    }
}
