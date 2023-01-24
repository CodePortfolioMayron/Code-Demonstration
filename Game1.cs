using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UnrivaledPractise.States;
using UnrivaledPractise.Gui;
using System.Diagnostics;


namespace UnrivaledPractise
{
    public class Game1 : Game
    {
        #region Declarations
        //GUI
        Texture2D playerGui, Endscreen;
        enum gamestates { Start, Play, End };
        gamestates currentstate = gamestates.Start;


        //states
        private State _currentState;
        private State _nextState;
        public void ChangeState(State state)
        {
            _nextState = state;
        }

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public const int stage = 1;

        Texture2D LevelOne, Frame, TileSheet, RedPlayer, BluePlayer, bulletTextureLeft, bulletTextureRight, flag, MainMenu;
        private SpriteFont HealthFont;
        private List<Sprite> Sprites;

        BulletManager Bullets = new BulletManager();
        private List<Rectangle> tilesInGame = Tile.GetTileList();
        #endregion

        #region Constructor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        #endregion

        #region Initialize
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            IsMouseVisible = true;
            graphics.ApplyChanges();
        
            base.Initialize();
        }
        #endregion

        #region Load Content
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //States
            MainMenu = Content.Load<Texture2D>("Background/MainMenu");
            Endscreen = Content.Load<Texture2D>("Background/Endscreen");

            _currentState = new MenuState(this, graphics.GraphicsDevice, Content);

            //Levels & Tiles
            TileSheet = Content.Load<Texture2D>("Sheets\\TileSheet20x20");
            LevelOne = Content.Load<Texture2D>("Background\\Lv1");
            Frame = Content.Load<Texture2D>("Background\\Frame");

            //Players
            RedPlayer = Content.Load<Texture2D>("Sprites\\RedSprite");
            BluePlayer = Content.Load<Texture2D>("Sprites\\BlueSprite");

            // Lives
            playerGui = Content.Load<Texture2D>("guibuttons/heart");

            //Fonts
            HealthFont = Content.Load<SpriteFont>("Fonts\\Health");

            //Bullet
            bulletTextureRight = Content.Load<Texture2D>("Bullet\\BulletRight");
            bulletTextureLeft = Content.Load<Texture2D>("Bullet\\BulletLeft");

            //Flag
            flag = Content.Load<Texture2D>("Sprites\\Flag");

            var animations = new Dictionary<string, Animation>()
            {
                { "WalkRight", new Animation(Content.Load<Texture2D>("Sheets\\P1WalkRightSheet"), 5) },
                { "WalkLeft", new Animation(Content.Load<Texture2D>("Sheets\\P1WalkRightSheet"), 5) }
            };
            Sprites = new List<Sprite>()
            {
                new Player(RedPlayer)
                {
                    PlayerNum = 1,
                    AnimPosition = new Vector2(1537, 8),
                    HPPosition = new Vector2(1340, 10),
                    Speed = 3f,
                    Health = 100,
                    Lives = 3,
                    LivesPosition = new Vector2 (1000, 500),
                    Input = new Input()
                    {
                        Up = Keys.W,
                        Down = Keys.S,
                        Left = Keys.A,
                        Right = Keys.D,
                        Shoot = Keys.B
                    }
                },

                new Player(BluePlayer)
                {
                    PlayerNum = 2,
                    AnimPosition = new Vector2(10, 10),
                    HPPosition = new Vector2(12, 10),
                    Speed = 3f,
                    Health = 100,
                    Lives = 3,
                    LivesPosition = new Vector2 (400, 500),
                    Input = new Input()
                    {
                        Up = Keys.Up,
                        Down = Keys.Down,
                        Left = Keys.Left,
                        Right = Keys.Right,
                        Shoot = Keys.K
                    }
                }
            };
            Bullets.Initialize(bulletTextureLeft, bulletTextureRight, GraphicsDevice);
            Flag.Initialize(flag);
            Tile.Initialize(TileSheet);
        }
        #endregion

        #region Update and Draw
        protected override void Update(GameTime gameTime)
        {
            switch (currentstate)
            {

                case gamestates.Start:
                    bool start= MenuState.getclickedStart(); 
                    if (start == false)
                    {
                        foreach (var sprite in Sprites)
                        {
                            sprite.Speed = 0f;
                            
                        }
                    }
                    if (start == true)
                    {
                        currentstate = gamestates.Play;
                        
                    }
                    break;
                case gamestates.Play:
                    foreach (var sprite in Sprites)
                    {
                        sprite.Speed =3f ;

                        if (sprite.PlayerNum == 1)
                        {
                            if (sprite.Lives <= 0)
                            {
                                currentstate = gamestates.End;
                            }
                        }
                        if (sprite.PlayerNum != 1)
                        {
                            if (sprite.Lives <= 0)
                            {
                                currentstate = gamestates.End;
                            }
                        }

                    }
                    break;
                case gamestates.End:

                    break;

            }


            // TODO: Add your update logic here

            if (_nextState != null)
            {
                _currentState = _nextState;

                _nextState = null;
            }

            _currentState.Update(gameTime);

            _currentState.PostUpdate(gameTime);
            // TODO: Add your update logic here
            //Update each sprite
            foreach (var sprite in Sprites)
            {
                if (stage == 1)
                {
                    sprite.Update(gameTime, tilesInGame, Sprites);
                }
            }
            Bullets.UpdateManagerBullet(gameTime, Sprites);
            Tile.Update(gameTime, Sprites);
            Flag.Update(gameTime, Sprites);
            base.Update(gameTime);
        }
                
            
        
    

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if (currentstate == gamestates.Start)
            {
                _currentState.Draw(gameTime, spriteBatch);

            };

            if (currentstate == gamestates.Play)
            {
               
                if (stage == 1)
                {
                    
                    spriteBatch.Draw(LevelOne, new Rectangle(0, 0, 1600, 900), Color.White);
                    Tile.Draw(spriteBatch);
                    Flag.Draw(spriteBatch);
                    Bullets.DrawBullets(spriteBatch);
                    foreach (var sprite in Sprites)
                    {
                        if (sprite.Health > 0)
                        {
                            sprite.Draw(spriteBatch);
                            if (sprite.PlayerNum != 1)
                            {
                                spriteBatch.DrawString(HealthFont, "HP: " + sprite.Health, new Vector2(sprite.HPPosition.X, sprite.HPPosition.Y), Color.Blue);
                            }
                            if (sprite.PlayerNum == 1)
                            {
                                spriteBatch.DrawString(HealthFont, "HP: " + sprite.Health, new Vector2(sprite.HPPosition.X, sprite.HPPosition.Y), Color.Red);
                            }

                        }
                        if (sprite.PlayerNum == 1)
                        {
                            for (int i = 1; i <= sprite.Lives; i++)
                            {
                                spriteBatch.Draw(playerGui, new Rectangle((int)sprite.LivesPosition.X + (i * 50), 30, 30, 30), Color.White);
                            }
                        }
                        if (sprite.PlayerNum != 1)
                        {
                            for (int i = 1; i <= sprite.Lives; i++)
                            {
                                spriteBatch.Draw(playerGui, new Rectangle((int)sprite.LivesPosition.X + (i * 50), 30, 30, 30), Color.White);
                            }
                        }

                    }

                }
            }
            if (currentstate == gamestates.End)
            {
                spriteBatch.Draw(Endscreen, new Vector2(0, 0), Color.White);
                foreach (var sprite in Sprites)
                {
                    sprite.Draw(spriteBatch);
                }
            };


           
            spriteBatch.Draw(Frame, new Rectangle(0, 0, 1600, 900), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion
    }

    
    #region Program Class
    public static class Program
        {
            static void Main()
            {
                using (var game = new Game1())
                    game.Run();
            }
        }
    #endregion
}
