using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UnrivaledPractise
{
    public class Sprite
    {
        #region Declarations
        public Input Input;

        protected KeyboardState currentKeyboardState;
        protected KeyboardState previousKeyboardState;

        protected AnimationManager _animationManager;
        protected Texture2D Texture;
        protected bool hasJumped;
        public bool isFacingLeft;

        public int Health;
        public int Lives;
        public int PlayerNum;
        public Vector2 Position;
        public Vector2 HPPosition;
        public Vector2 LivesPosition;
        public Vector2 Velocity;
        public Vector2 Gravity = new Vector2(0, 20);

        public float LifeSpan = 0f;
        public bool IsRemoved = false;

        protected Dictionary<string, Animation> _animations;


        public float Speed = 5f;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int) Position.X, (int) Position.Y, 50, 90);
            }
        }

        public Vector2 AnimPosition
        {
            get { return Position; }
            set
            {
                Position = value;

                if (_animationManager != null)
                    _animationManager.Position = Position;
            }
        }
        #endregion

        #region Constructors
        public Sprite(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
        }
        public Sprite(Texture2D texture)
        {
            Texture = texture;
        }
        
        #endregion

        #region Update, Move and Draw
        public virtual void Update(GameTime gameTime, List<Rectangle> Tiles, List<Sprite> sprites)
        {

        }

        
        public virtual void Draw(SpriteBatch spriteBatch)
        {
           // if (Texture != null)
                spriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, 50, 90), Color.White);
            //else if (_animationManager != null)
               // _animationManager.Draw(spriteBatch);
        }
        #endregion

        #region Sprite Collision
        protected bool IsTouchingLeft(Sprite sprite)
        {
            return this.Rectangle.Right + this.Velocity.X > sprite.Rectangle.Left + 10 &&
                this.Rectangle.Left < sprite.Rectangle.Left - 10 &&
                this.Rectangle.Bottom > sprite.Rectangle.Top + 9 &&
                this.Rectangle.Top < sprite.Rectangle.Bottom - 9;
        }
        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.Rectangle.Left + this.Velocity.X < sprite.Rectangle.Right - 10 &&
                this.Rectangle.Right > sprite.Rectangle.Right + 10 &&
                this.Rectangle.Bottom > sprite.Rectangle.Top + 9&&
                this.Rectangle.Top < sprite.Rectangle.Bottom - 9;
        }
        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.Rectangle.Bottom + this.Velocity.Y > sprite.Rectangle.Top + 9 &&
                this.Rectangle.Top < sprite.Rectangle.Top - 9 &&
                this.Rectangle.Right > sprite.Rectangle.Left + 10 &&
                this.Rectangle.Left < sprite.Rectangle.Right - 10;
        }
        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.Rectangle.Top + this.Velocity.Y < sprite.Rectangle.Bottom - 9 &&
                this.Rectangle.Bottom > sprite.Rectangle.Bottom  + 9 &&
                this.Rectangle.Right > sprite.Rectangle.Left + 10 &&
                this.Rectangle.Left < sprite.Rectangle.Right - 10;
        }
        #endregion

        #region Tile Collision
        public static bool IsTouchingTileLeft(Sprite sprite, Rectangle tile)
        {
            return sprite.Rectangle.Right - 7 + sprite.Velocity.X > tile.Left &&
                sprite.Rectangle.Left < tile.Left &&
                sprite.Rectangle.Bottom > tile.Top &&
                sprite.Rectangle.Top + 10 < tile.Bottom;
        }
        public static bool IsTouchingTileRight(Sprite sprite, Rectangle tile)
        {
            return sprite.Rectangle.Left + 7 + sprite.Velocity.X < tile.Right &&
                sprite.Rectangle.Right > tile.Right &&
                sprite.Rectangle.Bottom > tile.Top &&
                sprite.Rectangle.Top + 10 < tile.Bottom;
        }

        public static bool IsTouchingTileBottom(Sprite sprite, Rectangle tile)
        {
            return sprite.Rectangle.Top + 10 + sprite.Velocity.Y < tile.Bottom &&
                sprite.Rectangle.Bottom > tile.Bottom &&
                sprite.Rectangle.Right - 7 > tile.Left &&
                sprite.Rectangle.Left + 7 < tile.Right;
        }
        public static bool IsTouchingTileTop(Sprite sprite, Rectangle tile)
        {
            return sprite.Rectangle.Bottom + sprite.Velocity.Y > tile.Top &&
                sprite.Rectangle.Top < tile.Top &&
                sprite.Rectangle.Right - 7 > tile.Left &&
                sprite.Rectangle.Left + 7 < tile.Right;
        }
        #endregion

        #region Respawn
        public void Respawn(Sprite sprite)
        {
            sprite.Health = 100;
            if (sprite.PlayerNum == 1)
            {
                sprite.Position = new Vector2(1537, 8);
                sprite.Lives--;
            }
            if (sprite.PlayerNum == 2)
            {
                sprite.Position = new Vector2(5, 5);
                sprite.Lives--;
            }
        }
        #endregion
        #region endscreendraw
        public void EndDraw()
        {


        }



        #endregion
    }
}
