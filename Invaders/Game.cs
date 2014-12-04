﻿using System;
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
            {
                Point shotLocation = new Point(playerShip.Location.X + playerShip.Area.Width / 2, playerShip.Location.Y);
                playerShots.Add(new Shot(shotLocation, Direction.Up, boundaries));
            }
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

                MoveInvaders();
                ReturnFire();

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

        /// <summary>
        /// Invaders fire back at the player.
        /// </summary>
        private void ReturnFire()
        {
            //If there are more shots than the current invader wave or if randomly determined, no invaders fire this frame.
            if (invaderShots.Count > wave || random.Next(10) < 10 - wave)
                return;

            //Group invaders by their x locations.
            var invadersByXLocation = from invader in invaders
                                      group invader by invader.Location.X
                                      into invaderGroups
                                      orderby invaderGroups.Key descending
                                      select invaderGroups;

            //Randomly select a group to have the front most invader fire from.
            int invaderGroupToFire = random.Next(invaders.Count);
            Invader invaderToFire;

            //Find out the front most invader to fire from.
            List<Invader> invaderGroup = (List<Invader>) invadersByXLocation.ElementAt(invaderGroupToFire);
            invaderToFire = invaderGroup.First();

            //Emit the shot from the bottom middle of the invader.
            Point shotLocation = new Point(invaderToFire.Location.X + invaderToFire.Area.Width / 2, invaderToFire.Location.Y + invaderToFire.Area.Height);
            invaderShots.Add(new Shot(shotLocation, Direction.Down, boundaries));
        }

        /// <summary>
        /// Moves the invaders.
        /// </summary>
        private void MoveInvaders()
        {
            //Based on the wave, frames are skipped to slow the movement of the invaders.
            //The higher the wave, the less frames are skipped thus causing the invaders to move faster.
            int maxFramesToSkip = 7 - wave;
            
            //If the number of frames skipped is less than or equal to the number of frames to skip, skip this frame.
            if (++framesSkipped <= maxFramesToSkip && framesSkipped > 0)
            {
                //If this is the last frame to skip, reset so that the next frame can occur.
                if (framesSkipped == maxFramesToSkip) 
                    framesSkipped = -1;
                return;
            }

            switch (invaderDirection)
            {
                //Invaders are moving to the right.
                case Direction.Right :

                    //Get a list of any invaders within 100 PX of the right side of the play area.
                    var invadersCloseToEdge = from invader in invaders
                                              where boundaries.Width - invader.Location.X < 100
                                              select invader;

                    //If there are any invaders within 100 PX of the right side of the play area, move the invaders down and change their direction
                    //to the left.
                    //Else move the invaders to the right.
                    if (((List<Invader>)invadersCloseToEdge).Count > 0)
                    {
                        foreach (Invader invader in invaders)
                            invader.Move(Direction.Down);
                        invaderDirection = Direction.Left;
                    }
                    else
                        foreach (Invader invader in invaders)
                            invader.Move(invaderDirection);
                    break;

                //Invaders are moving to the left.
                case Direction.Left :

                    //Get a list of any invaders within 100 PX of the left side of the play area.
                    invadersCloseToEdge = from invader in invaders
                                          where invader.Location.X - boundaries.X < 100
                                          select invader;

                    //If there are any invaders within 100 PX of the left side of the play area, move the invaders down and change their direction
                    //to the right.
                    //Else move the invaders to the left.
                    if (((List<Invader>)invadersCloseToEdge).Count > 0)
                    {
                        foreach (Invader invader in invaders)
                            invader.Move(Direction.Down);
                        invaderDirection = Direction.Right;
                    }
                    else
                        foreach (Invader invader in invaders)
                            invader.Move(invaderDirection);
                    break;
            }
        }
    }
}
