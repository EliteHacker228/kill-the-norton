﻿#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kill_the_Norton.Calculations;
using Kill_the_Norton.Entities;
using Kill_the_Norton.Presenters;

namespace Kill_the_Norton
{
    public partial class Form1 : Form
    {
        private Game _game;

        private GamePresenter _gamePresenter;
        //private Bullet? bullet;

        public Form1()
        {
            InitializeComponent();
            
            this.Cursor = Cursors.Cross;

            //this.BackgroundImageLayout = ImageLayout.Tile;
            //this.BackgroundImage = new Bitmap(Resources.AsphaltSprite);

            BackgroundImageLayout = ImageLayout.Center;
            BackgroundImage = new Bitmap(Resources.Back);
            Text = "Kill the Norton";
            Icon = new Icon(Directory.GetCurrentDirectory()+"\\favicon.ico");
            
            _game = new Game();
            _game.Player = new Player();
            _game.Player.Cooridnates = new Point(200, 250);
            _game.Player.Delta = new Point(100, 150);
            //_game.Player.Cooridantes = new Point(0, 0);
            _game.Level = new Level();
            _game.Player.Sprite = Resources.PlayerSprite;
            _game.Level.Map = new[,]
            {
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 4, 4, 4, 4, 4, 4, 3, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 4, 4, 4, 4, 4, 4, 3, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 4, 4, 3, 3, 3, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 4, 4, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 4, 4, 3, 2, 3, 3, 3, 4, 4, 3, 3, 9, 2, 1, 2, 3, 3, 3, 3, 3, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 2, 3, 4, 4, 3, 2, 3, 3, 3, 4, 4, 3, 3, 3, 2, 1, 2, 3, 4, 4, 4, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 3, 3, 3, 3, 3, 3, 1, 1, 2, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 4, 4, 3, 2, 3, 3, 3, 4, 4, 3, 3, 3, 2, 1, 2, 3, 4, 4, 4, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 2, 2, 2, 4, 4, 2, 2, 2, 1, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 8, 3, 4, 4, 3, 2, 3, 3, 3, 4, 4, 3, 3, 3, 2, 1, 2, 3, 3, 4, 4, 3, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 2, 8, 3, 4, 4, 3, 8, 2, 1, 2, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 3, 2, 3, 3, 3, 4, 4, 3, 3, 3, 2, 1, 2, 3, 3, 4, 4, 3, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 2, 3, 3, 4, 4, 3, 3, 2, 1, 2, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 3, 2, 9, 3, 3, 4, 4, 3, 3, 3, 2, 1, 2, 2, 3, 4, 4, 3, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 2, 3, 3, 4, 4, 3, 3, 2, 1, 2, 3, 3, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 9, 3, 3, 3, 3, 2, 2, 2, 3, 4, 4, 3, 2, 2, 2, 1, 1, 2, 3, 4, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 2, 3, 3, 4, 4, 3, 3, 2, 1, 2, 3, 3, 4, 4, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 2, 3, 4, 4, 3, 2, 1, 1, 1, 1, 2, 3, 4, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 2, 9, 3, 4, 4, 3, 3, 2, 1, 2, 3, 3, 4, 4, 3, 3, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 4, 4, 3, 2, 1, 1, 1, 1, 2, 3, 4, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 2, 2, 2, 2, 3, 3, 4, 4, 3, 3, 2, 2, 2, 2, 3, 4, 4, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 4, 4, 3, 2, 2, 2, 2, 2, 2, 3, 4, 4, 3, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1},
                {1, 2, 8, 3, 3, 3, 3, 4, 4, 3, 3, 3, 3, 3, 8, 3, 4, 4, 3, 8, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 8, 3, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 3, 3, 3, 3, 3, 3, 3, 2, 1, 1, 1, 1, 1, 1},
                {1, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 3, 3, 2, 1, 1, 1, 1, 1, 1},
                {1, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 3, 3, 2, 1, 1, 1, 1, 1, 1},
                {1, 2, 8, 3, 3, 3, 3, 4, 4, 3, 3, 3, 3, 3, 3, 3, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 3, 3, 3, 3, 3, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 3, 3, 2, 1, 1, 1, 1, 1, 1},
                {1, 2, 2, 2, 2, 3, 3, 4, 4, 3, 3, 2, 2, 2, 2, 3, 4, 4, 3, 2, 2, 2, 2, 2, 2, 3, 4, 4, 3, 2, 2, 2, 2, 2, 2, 3, 4, 4, 3, 2, 2, 2, 3, 4, 4, 3, 2, 3, 3, 3, 3, 3, 3, 4, 4, 3, 3, 2, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 2, 3, 3, 4, 4, 3, 9, 2, 3, 3, 9, 3, 4, 4, 9, 2, 3, 3, 3, 3, 2, 3, 4, 4, 3, 2, 1, 1, 2, 9, 3, 3, 4, 4, 3, 3, 3, 2, 3, 4, 4, 3, 2, 3, 3, 3, 3, 3, 3, 4, 4, 3, 2, 2, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 3, 4, 4, 4, 4, 4, 3, 2, 3, 4, 4, 3, 2, 3, 4, 4, 3, 2, 1, 1, 2, 3, 3, 3, 4, 4, 3, 3, 3, 2, 3, 4, 4, 3, 2, 3, 3, 3, 3, 3, 3, 4, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 4, 4, 4, 4, 4, 3, 2, 3, 4, 4, 3, 3, 3, 4, 4, 3, 2, 1, 1, 2, 3, 3, 3, 4, 4, 3, 3, 3, 2, 3, 4, 4, 3, 2, 2, 2, 2, 2, 2, 3, 4, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3, 3, 9, 2, 3, 4, 4, 4, 4, 4, 4, 4, 3, 2, 1, 1, 2, 3, 3, 3, 4, 4, 3, 3, 3, 2, 3, 4, 4, 3, 2, 3, 3, 3, 3, 2, 3, 4, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 4, 4, 4, 4, 4, 4, 4, 3, 2, 1, 1, 2, 3, 3, 3, 4, 4, 3, 3, 3, 2, 3, 4, 4, 3, 2, 3, 4, 4, 3, 2, 3, 4, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 4, 4, 3, 3, 3, 4, 4, 3, 2, 1, 1, 2, 3, 3, 3, 4, 4, 3, 3, 3, 2, 3, 4, 4, 3, 2, 3, 4, 4, 3, 3, 3, 4, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 4, 4, 3, 2, 3, 4, 4, 3, 2, 1, 1, 2, 3, 3, 3, 4, 4, 3, 3, 3, 2, 3, 4, 4, 3, 2, 3, 4, 4, 4, 4, 4, 4, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 2, 3, 4, 4, 3, 2, 1, 1, 2, 3, 4, 4, 4, 4, 4, 4, 3, 2, 3, 4, 4, 3, 2, 3, 4, 4, 4, 4, 4, 4, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 3, 4, 4, 3, 2, 1, 1, 2, 3, 4, 4, 4, 4, 4, 4, 3, 2, 3, 4, 4, 3, 2, 3, 4, 4, 3, 3, 3, 4, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 2, 1, 1, 2, 3, 3, 3, 3, 3, 3, 3, 9, 2, 3, 4, 4, 3, 2, 3, 4, 4, 3, 2, 3, 4, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 4, 4, 3, 2, 3, 3, 3, 3, 2, 3, 4, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3, 3, 2, 3, 4, 4, 3, 2, 2, 2, 2, 2, 2, 3, 4, 4, 3, 2, 2, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3, 3, 2, 3, 4, 4, 3, 2, 3, 3, 3, 3, 3, 3, 4, 4, 3, 3, 2, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3, 3, 2, 3, 4, 4, 3, 2, 3, 3, 3, 3, 3, 3, 4, 4, 3, 3, 2, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 2, 3, 4, 4, 3, 2, 3, 3, 3, 3, 3, 3, 4, 4, 3, 3, 2, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3, 2, 3, 3, 3, 3, 3, 3, 2, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 3, 3, 2, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 3, 3, 2, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 3, 3, 2, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3, 2, 3, 3, 3, 3, 3, 3, 2, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 3, 3, 2, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 2, 3, 4, 4, 3, 2, 3, 3, 3, 3, 3, 3, 4, 4, 3, 3, 2, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3, 2, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3, 3, 2, 3, 4, 4, 3, 2, 3, 3, 3, 3, 3, 3, 4, 4, 3, 3, 2, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3, 2, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3, 3, 2, 3, 3, 3, 3, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                
            };

            _game.Level.MapWidth = _game.Level.Map.GetLength(0);
            _game.Level.MapHeight= _game.Level.Map.GetLength(1);

            _gamePresenter = new GamePresenter(_game.Level.Map, this);
            _gamePresenter.Game = _game;
            _gamePresenter.form = this;

            progressBar1.Maximum = 9;
            progressBar1.Minimum = 0;
            progressBar1.Step = 1;
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.ForeColor = Color.Black;
            _gamePresenter.TimeMachine.ProgressBar = progressBar1;


            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            label1.Hide();
            label2.Hide();
            label3.Hide();
            pictureBox1.Hide();

            DoubleBuffered = true;

            timer1.Interval = 10;
            timer1.Tick += _gamePresenter.update;
            timer1.Start();

            KeyDown += _gamePresenter.keyDown;
            KeyUp += _gamePresenter.keyUp;

            KeyPress += _gamePresenter.keyPress;

            MouseMove += _gamePresenter.OnMouseMove;
            MouseClick += _gamePresenter.MouseClickHandler;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        protected override void OnPaint(PaintEventArgs gameScreen)
        {
            Graphics gameGraphic = gameScreen.Graphics;
            _gamePresenter.DrawMap(gameGraphic);

            Bitmap playerBitmap = new Bitmap(_game.Player.Sprite.Width, _game.Player.Sprite.Height);
            Graphics playerGraphics = Graphics.FromImage(playerBitmap);

            playerGraphics.TranslateTransform(playerBitmap.Width / 2, playerBitmap.Height / 2);
            playerGraphics.RotateTransform(_game.Player.Angle);
            playerGraphics.TranslateTransform(-playerBitmap.Width / 2, -playerBitmap.Height / 2);
            playerGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            playerGraphics.DrawImage(_game.Player.Sprite, 0, 0);

            gameScreen.Graphics.DrawImage(playerBitmap, _game.Player.Cooridnates.X - 32,
                _game.Player.Cooridnates.Y - 32);

            /*gameScreen.Graphics.FillEllipse(Brushes.GreenYellow, _game.Player.Cooridantes.X - _game.Player.Delta.X,
                _game.Player.Cooridantes.Y - _game.Player.Delta.Y, 8, 8);
            gameScreen.Graphics.FillEllipse(Brushes.GreenYellow, _game.Player.Cooridantes.X - _game.Player.Delta.X + 64,
                _game.Player.Cooridantes.Y - _game.Player.Delta.Y, 8, 8);
            gameScreen.Graphics.FillEllipse(Brushes.GreenYellow, _game.Player.Cooridantes.X - _game.Player.Delta.X,
                _game.Player.Cooridantes.Y - _game.Player.Delta.Y + 64, 8, 8);
            gameScreen.Graphics.FillEllipse(Brushes.GreenYellow, _game.Player.Cooridantes.X - _game.Player.Delta.X + 64,
                _game.Player.Cooridantes.Y - _game.Player.Delta.Y + 64, 8, 8);*/

            /*gameScreen.Graphics.FillEllipse(Brushes.Red, _game.Player.Cooridantes.X - 32,
                _game.Player.Cooridantes.Y - 32, 8, 8);
            gameScreen.Graphics.FillEllipse(Brushes.Red, _game.Player.Cooridantes.X + 32,
                _game.Player.Cooridantes.Y - 32, 8, 8);
            gameScreen.Graphics.FillEllipse(Brushes.Red, _game.Player.Cooridantes.X - 32,
                _game.Player.Cooridantes.Y + 32, 8, 8);
            gameScreen.Graphics.FillEllipse(Brushes.Red, _game.Player.Cooridantes.X + 32,
                _game.Player.Cooridantes.Y + 32, 8, 8);*/


            if (_gamePresenter.bullets.Count != 0)
            {
                foreach (var bullet in _gamePresenter.bullets)
                {
                    if (bullet.Sender == Sender.Player)
                        gameScreen.Graphics.FillEllipse(Brushes.Yellow, bullet.OwnCoordinates.X,
                            bullet.OwnCoordinates.Y, 16, 16);
                    else 
                        gameScreen.Graphics.FillEllipse(Brushes.GreenYellow, bullet.OwnCoordinates.X,
                            bullet.OwnCoordinates.Y, 16, 16);
                    //gameScreen.Graphics.DrawLine(Pens.Yellow, _game.Player.Cooridantes, bullet.Target);
                }
            }

            if (_gamePresenter.enemies.Count != 0)
            {
                foreach (var enemy in _gamePresenter.enemies)
                {
                    /*gameScreen.Graphics.DrawImage(Resources.EnemySprite, enemy.Cooridantes.X - _game.Player.Delta.X,
                        enemy.Cooridantes.Y - _game.Player.Delta.Y);*/

                    Bitmap enemyBitmap = new Bitmap(Resources.EnemySprite.Width, Resources.EnemySprite.Height);
                    Graphics enemyGraphics = Graphics.FromImage(enemyBitmap);

                    enemyGraphics.TranslateTransform(enemyBitmap.Width / 2, enemyBitmap.Height / 2);
                    enemyGraphics.RotateTransform(enemy.Angle);
                    enemyGraphics.TranslateTransform(-enemyBitmap.Width / 2, -enemyBitmap.Height / 2);
                    enemyGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    enemyGraphics.DrawImage(Resources.EnemySprite, 0, 0);

                    gameScreen.Graphics.DrawImage(enemyBitmap, enemy.Cooridantes.X - _game.Player.Delta.X,
                        enemy.Cooridantes.Y - _game.Player.Delta.Y);

                    /*gameScreen.Graphics.FillEllipse(Brushes.Red, enemy.Cooridantes.X - _game.Player.Delta.X,
                        enemy.Cooridantes.Y - _game.Player.Delta.Y, 8, 8);
                    gameScreen.Graphics.FillEllipse(Brushes.Red, enemy.Cooridantes.X - _game.Player.Delta.X + 64,
                        enemy.Cooridantes.Y - _game.Player.Delta.Y, 8, 8);
                    gameScreen.Graphics.FillEllipse(Brushes.Red, enemy.Cooridantes.X - _game.Player.Delta.X,
                        enemy.Cooridantes.Y - _game.Player.Delta.Y + 64, 8, 8);
                    gameScreen.Graphics.FillEllipse(Brushes.Red, enemy.Cooridantes.X - _game.Player.Delta.X + 64,
                        enemy.Cooridantes.Y - _game.Player.Delta.Y + 64, 8, 8);*/
                }
            }
        }

        /*private void ClickHandler(object sender,  e)
        {
            label2.Text = "Координаты клика: " + 
        }*/

        /*protected override void OnMouseClick(MouseEventArgs e)
        {
            label2.Text = "Координаты клика: " + e.X + ", " + e.Y;
            bullet = new Bullet();
            bullet.Target = new Point(e.X - _game.Player.Delta.X, e.Y - _game.Player.Delta.Y);
            bullet.Coordinates = new Point(_game.Player.PlayerCooridantes.X - _game.Player.Delta.X - 64, _game.Player.PlayerCooridantes.Y - _game.Player.Delta.Y - 64);
            bullet.Speed = 18;
            bullet.Delta = GameMath.GetDelta(bullet);
            _gamePresenter.bullet = bullet;
        }*/
    }
}