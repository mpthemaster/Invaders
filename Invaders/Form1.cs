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

        public Form1()
        {
            InitializeComponent();

            game = new Game(ClientRectangle); //Tells the game object how big the game's window is.
            game.GameOver += Game_GameOver;
        }

        /// <summary>
        /// Controls the animation for the invaders and the game's background.
        /// </summary>
        private void timerAnimation_Tick(object sender, EventArgs e)
        {

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
            if (gameOver)
            {
                //End game writing********************************************************************************************
            }
            else
            {
                //Draw frame************************************************************************************************
            }
        }
    }
}
