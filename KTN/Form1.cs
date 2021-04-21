using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KTN
{
    public partial class Form1 : Form
    {
        private bool goLeft, goRight, goUp, goDown;
        private int playerSpeed = 10;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void MainGameTimer(object sender, EventArgs e)
        {
            player.Refresh();

            if (goLeft)
            {
                player.Left -= playerSpeed;
            }

            if (goRight)
            {
                player.Left += playerSpeed;
            }
            
            if (goUp)
            {
                player.Top -= playerSpeed;
            }
            
            if (goDown)
            {
                player.Top += playerSpeed;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
           // base.OnKeyDown(e);
           KeyIsDown(null, e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            //base.OnKeyUp(e);
            KeyIsUp(null, e);
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
            }
            
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
        }
    }
}