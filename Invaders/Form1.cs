using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invaders
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// All the keys the player is currently pressing. The most current key pressed is at the top of the list.
        /// </summary>
        private List<Keys> keysPressed = new List<Keys>();

        /// <summary>
        /// Whether the game is over.
        /// </summary>
        private bool gameOver;
        private Game game;
        private int animationCell; //What animation cell the invaders are on.
        private bool descendingAnimation; 
        private Random random;

        public Form1()
        {
            InitializeComponent();

            random = new Random();
            game = new Game(ClientRectangle, random); //Tells the game object how big the game's window is.
            game.OnGameOver += Game_GameOver;
        }

        /// <summary>
        /// Controls the animation for the invaders and the game's background.
        /// </summary>
        private void timerAnimation_Tick(object sender, EventArgs e)
        {
            //If the animation is descending back through the cells, decrement the animation cell that is currently displayed.
            //Else the animation is ascending through the cells, increase the animation cell that is currently displayed.
            if (descendingAnimation)
            {
                animationCell--;

                //If the animation cell is less than the minimum cell, reset it and begin ascending.
                if (animationCell < 0)
                {
                    animationCell = 1;
                    descendingAnimation = false;
                }
            }
            else
            {
                animationCell++;

                //If the animation cells is greater than the maximum cell, reset it and begin descending.
                if (animationCell < 3)
                {
                    animationCell = 2;
                    descendingAnimation = true;
                }
            }
            game.Twinkle();
            Refresh();
        }

        /// <summary>
        /// Advances the game by one frame.
        /// </summary>
        private void timerGame_Tick(object sender, EventArgs e)
        {
            game.Go();

            //Move the player left or right if the player wants to.
            foreach (Keys key in keysPressed)
                if (key == Keys.Left)
                {
                    game.MovePlayer(Direction.Left);
                    return;
                }
                else if (key == Keys.Right)
                {
                    game.MovePlayer(Direction.Right);
                    return;
                }
        }

        /// <summary>
        /// Tracks what key the player just pressed.
        /// </summary>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //If the player wants to quit,
            //  quit.
            if (e.KeyCode == Keys.Q)
                Application.Exit();

            //If the game is over and the player wants to reset the game,
            //  reset the game.
            if (gameOver && e.KeyCode == Keys.S)
            {
                //Reset the game code. *********************************************************************
                return;
            }

            //If the player wants to fire a shot,
            //  fire a shot.
            if (e.KeyCode == Keys.Space)
                game.FireShot();

            //If the key has already been added to the list,
            //  remove it so that it can be placed at the top so that the most current key pressed in the list is the top.
            if (keysPressed.Contains(e.KeyCode))
                keysPressed.Remove(e.KeyCode);
            keysPressed.Add(e.KeyCode);
        }

        /// <summary>
        /// Removes from the list what key the player just released.
        /// </summary>
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (keysPressed.Contains(e.KeyCode))
                keysPressed.Remove(e.KeyCode);
        }

        /// <summary>
        /// Stops the game from continuing to run.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Game_GameOver(object sender, EventArgs e)
        {
            timerGame.Stop();
            gameOver = true;
            Invalidate();
        }

        /// <summary>
        /// Draws the current frame.
        /// </summary>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            //Draw the frame.
            game.Draw(g, animationCell);
            
            //Draw that the game is over and instructions.
            if (gameOver)
            {
                string endScreen = "Game Over!", instructions = "s: Start Game, q: Quit Game";
                Font fontEndScreen = new Font(FontFamily.Families[0], 100f, FontStyle.Bold);
                Font fontInstructions = new Font(FontFamily.Families[0], 50f, FontStyle.Bold);
                SizeF sizeEndScreen = e.Graphics.MeasureString(endScreen, fontEndScreen);
                SizeF sizeInstructions = e.Graphics.MeasureString(instructions, fontInstructions);

                g.DrawString(endScreen, fontEndScreen, Brushes.Yellow, (Width - sizeEndScreen.Width) / 2, (Height - sizeEndScreen.Height) / 2);
                g.DrawString(instructions, fontInstructions, Brushes.Yellow, (Width - sizeInstructions.Width) / 2, (Height +
                    sizeEndScreen.Height - sizeInstructions.Height) / 2);
            }
        }
    }
}