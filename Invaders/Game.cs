using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Invaders
{
    class Game
    {
        /// <summary>
        /// Fires when the game is over.
        /// </summary>
        public event EventHandler GameOver;

        private System.Drawing.Rectangle ClientRectangle;

        public Game(System.Drawing.Rectangle ClientRectangle)
        {
            // TODO: Complete member initialization
            this.ClientRectangle = ClientRectangle;
        }
        internal void FireShot()
        {
            throw new NotImplementedException();
        }

        internal void Go()
        {
            throw new NotImplementedException();
        }

        internal void MovePlayer(Direction direction)
        {
            throw new NotImplementedException();
        }
    }
}
