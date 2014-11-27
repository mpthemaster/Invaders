using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Invaders
{
    class Game
    {
        private int score = 0, livesLeft = 2, wave = 0, framesSkipped = 0;

        private Rectangle boundaries;
        private Random random;

        private Direction invaderDirection;
        private List<Invader> invaders;

        private PlayerShip playerShip;
        private List<Shot> playerShots, invaderShots;

        private Stars stars;


        /// <summary>
        /// Fires when the game is over.
        /// </summary>
        public event EventHandler GameOver;

        public Game(Rectangle boundaries)
        {
            // TODO: Complete member initialization
            this.boundaries = boundaries;
            stars = new Stars();
            invaders = new List<Invader>();
            playerShip = new PlayerShip();
            playerShots = new List<Shot>();
            invaderShots = new List<Shot>();
        }
        public void FireShot()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates a frame.
        /// </summary>
        public void Go()
        {
            //If the player died, do nothing.
            //Else calculate the frame.
            if (!playerShip.Alive)
                return;
            else
            {
                //Foreach shot fired by the player, move it.
                //  If the shot moved off screen, remove it from the game.
                foreach (Shot shot in playerShots)
                    if (!shot.Move())
                        playerShots.Remove(shot);

                //Foreach shot fired by the invaders, move it.
                //  If the shot moved off screen, remove it from the game.
                foreach (Shot shot in invaderShots)
                    if (!shot.Move())
                        invaderShots.Remove(shot);

                //Move the invaders.*************************************************************************************
                foreach (Invader invader in invaders)
                    invader.Move(Direction.Left);

                //Check if time to return fire and fire.***********************************************************************

                //Collision detection.******************************************************************
            }
        }

        public void MovePlayer(Direction direction)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Draws everything in the frame onto the form.
        /// </summary>
        /// <param name="g">The form's graphics.</param>
        /// <param name="animationCell">The animation cell to draw the invaders at.</param>
        public void Draw(Graphics g, int animationCell)
        {
            //Draw the outer space background.
            g.FillRectangle(Brushes.Black, boundaries);
            stars.Draw(g);

            //Draw everything else.
            foreach (Invader invader in invaders)
                invader.Draw(g, animationCell);

            playerShip.Draw(g);

            foreach (Shot shot in playerShots)
                shot.Draw(g);

            foreach (Shot shot in invaderShots)
                shot.Draw(g);

            //Draw score.
            g.DrawString(score.ToString(), new Font(FontFamily.Families[0], 45f), Brushes.GreenYellow, 0, -10);

            //Draw player's lives.
            Image player = Properties.Resources.player;
            for (int i = 0; i < livesLeft; i++)
            {
                int x = (player.Width + 10) * i;
                g.DrawImageUnscaled(player, boundaries.Width - player.Width - 10 - x, 10);
            }
        }

        public void Twinkle()
        {

        }
    }
}
