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
        public event EventHandler OnGameOver;

        /// <summary>
        /// Creates the game.
        /// </summary>
        /// <param name="boundaries">The boundaries of the battlefield.</param>
        /// <param name="random">A random number generator.</param>
        public Game(Rectangle boundaries, Random random)
        {
            this.boundaries = boundaries;
            this.random = random;
            stars = new Stars(boundaries, random);
            playerShip = new PlayerShip(boundaries);
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
                for (int i = 0; i < playerShots.Count; i++)
                    if (!playerShots[i].Move())
                        playerShots.Remove(playerShots[i]);

                //Foreach shot fired by the invaders, move it.
                //  If the shot moved off screen, remove it from the game.
                for (int i = 0; i < invaderShots.Count; i++)
                    if (!invaderShots[i].Move())
                        invaderShots.Remove(invaderShots[i]);

                MoveInvaders();
                ReturnFire();

                //Collision detection.
                //If an invader has reached the end of the screen, fire the game over event.
                if (CheckFoInvaderCollisions() && OnGameOver != null)
                    OnGameOver(this, new EventArgs());

                //If the player was hit by an invader shot,
                //  If the player is out of lives, end the game.
                //  Else take away a life.
                if (CheckForPlayerCollisions())
                {
                    playerShip.Alive = false;
                    if (livesLeft == 0)
                        OnGameOver(this, new EventArgs());
                    else
                        livesLeft--;
                }
               
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
            stars.Twinkle();
        }

        /// <summary>
        /// Creates a new wave of invaders. *********************************Make more complicated.
        /// </summary>
        private void NextWave()
        {
            wave++;
            List<Invader> newInvaders = new List<Invader>(30);

            //Creates 6 columns with 5 rows of invaders (30 invaders total).
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Point location = new Point(100 + 75 * i, 75 + 75 * j);
                    newInvaders.Add(new Invader(ShipType.Star, location, 10));
                }
            }

            invaders = newInvaders;
            invaderDirection = Direction.Right;
            framesSkipped = 0;
        }

        /// <summary>
        /// Checks if an invader's shot collided with the player.
        /// </summary>
        /// <returns>Returns true if a shot collided with the player.</returns>
        private bool CheckForPlayerCollisions()
        {
            for (int i = 0; i < invaderShots.Count(); i++)
                if (playerShip.Area.Contains(invaderShots[i].Location))
                {
                    invaderShots.Remove(invaderShots[i]);
                    return true;
                }
            return false;
        }

        /// <summary>
        /// Checks if a player's shot collided with an invader and whether the invaders reached the bottom of the screen.
        /// </summary>
        /// <returns>Returns true if an invader reached the bottom of the screen.</returns>
        private bool CheckFoInvaderCollisions()
        {
            bool endGame = false;

            //Check if each shot has hit any invaders.
            //If a shot has hit an invader, remove the invader and the shot and add to the player's score.
            for (int i = 0; i < invaders.Count(); i++)
            {
                //If an invader has reached the bottom of the screen, end the game.
                if (invaders[i].Location.Y + invaders[i].Area.Height >= boundaries.Height)
                    endGame = true;

                for (int j = 0; j < playerShots.Count(); j++)
                    if (invaders[i].Area.Contains(playerShots[j].Location))
                    {
                        playerShots.Remove(playerShots[j]);
                        score += invaders[i].Score;
                        invaders.Remove(invaders[i]);
                    }
            }
            return endGame;
        }

        /// <summary>
        /// Invaders fire back at the player.
        /// </summary>
        private void ReturnFire()
        {
            //If there are more shots than the current invader wave or if randomly determined, no invaders fire this frame.
            if (invaderShots.Count > wave || random.Next(40) < 40 - wave) //Add a multiplier to wave if difficulty isn't increasing by enough.
                return;

            //Group invaders by their x locations.
            var invadersByXLocation = from invader in invaders
                                      group invader by invader.Location.X
                                      into invaderGroups
                                      orderby invaderGroups.Key descending
                                      select invaderGroups;

            //Randomly select a group to have the front most invader fire from.
            int invaderGroupToFire = random.Next(invadersByXLocation.Count());
            Invader invaderToFire = null;

            //Find out the front most invader to fire from.
            int i = 0;
            foreach (var invaderGroup in invadersByXLocation)
            {
                if (i == invaderGroupToFire)
                {
                    invaderToFire = invaderGroup.Last();
                    break;
                }
                i++;
            }

            //If there are any invaders, emit the shot from the bottom middle of the invader.
            if (invaders.Count > 0)
            {
                Point shotLocation = new Point(invaderToFire.Location.X + invaderToFire.Area.Width / 2, invaderToFire.Location.Y + invaderToFire.Area.Height);
                invaderShots.Add(new Shot(shotLocation, Direction.Down, boundaries));
            }
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
                    if (invadersCloseToEdge.Count() > 0)
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
                    if (invadersCloseToEdge.Count() > 0)
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
