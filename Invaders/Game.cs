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

        /// <summary>
        /// Creates the game.
        /// </summary>
        /// <param name="boundaries">The boundaries of the battlefield.</param>
        /// <param name="random">A random number generator.</param>
        public Game(Rectangle boundaries, Random random)
        {
            this.boundaries = boundaries;
            this.random = random;
            stars = new Stars();
            playerShip = new PlayerShip();
            playerShots = new List<Shot>();
            invaderShots = new List<Shot>();
            NextWave();
        }

        /// <summary>
        /// Fires a shot from the player's ship.
        /// </summary>
        public void FireShot()
        {
            //If there are less than 2 shots on the screen, the ship fires another shot.
            if (playerShots.Count < 2)
                playerShots.Add(new Shot());
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
                    invader.Move(invaderDirection);

                //Check if time to return fire and fire.***********************************************************************

                //Collision detection.******************************************************************
            }
        }

        /// <summary>
        /// Moves the player's ship.
        /// </summary>
        /// <param name="direction">The direction the player wants to move in.</param>
        public void MovePlayer(Direction direction)
        {
            //If the player is still alive, move the player's ship.
            if (playerShip.Alive)
                playerShip.Move(direction);
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

        /// <summary>
        /// Make the stars twinkle.
        /// </summary>
        public void Twinkle()
        {
            stars.Twinkle(random);
        }

        /// <summary>
        /// Creates a new wave of invaders. *********************************Make more complicated.
        /// </summary>
        private void NextWave()
        {
            wave++;
            List<Invader> newInvaders = new List<Invader>(30);

            //Creates 6 columns with 5 rows of invaders (30 invaders total).***********************************************
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    newInvaders.Add(new Invader());
                }
            }
            
            invaderDirection = Direction.Right;
            framesSkipped = 0;
        }

        /// <summary>
        /// Checks if an invader's shot collided with the player.
        /// </summary>
        /// <returns>Returns true if a shot collided with the player.</returns>
        private bool CheckForPlayerCollisions()
        {
            return false;//***************************************************************+
        }

        /// <summary>
        /// Checks if a player's shot collided with an invader.
        /// </summary>
        /// <returns>Returns true if a shot collided with an invader.</returns>
        private bool CheckFoInvaderCollisions()
        {
            return false;//**********************************************************************************
        }

        //private void MoveInvaders()
        //{

        //}

        /// <summary>
        /// Invaders fire back at the player.
        /// </summary>
        private void ReturnFire()
        {

        }
    }
}
