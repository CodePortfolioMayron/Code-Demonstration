using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UnrivaledPractise.Gui;
using UnrivaledPractise.States;

namespace UnrivaledPractise
{
    public class Player : Sprite 
    {
        #region Constructor
        public Player(/*Dictionary<string, Animation> animations*/ Texture2D texture)
            : base(/*animations*/ texture)
        {
            hasJumped = true;
        }

        #endregion
        gui guinfo = new gui();
        #region Update (Collisions and Movement)
        public override void Update(GameTime gameTime, List<Rectangle> Tiles, List<Sprite> sprites)
        {
            //SetAnimations();
            //_animationManager.Update(gameTime);
            
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            //Player Movement
            Position += Velocity;

            if (Input == null)
            {
                return;
            }

            //Move Left/Right
            if (currentKeyboardState.IsKeyDown(Input.Left) ) 
            {
                isFacingLeft = true;
                Velocity.X = -Speed;
            }

            else if (currentKeyboardState.IsKeyDown(Input.Right))
            {
                Velocity.X = Speed;
                isFacingLeft = false;
            }
                
            else
                Velocity.X = 0;

            //Jump
            if (currentKeyboardState.IsKeyDown(Input.Up) && hasJumped == false)
            {
                Velocity.Y = -7.3f;
                hasJumped = true;
            }

            if (hasJumped == true)
            {
                float i = 1;
                Velocity.Y += 0.15f * i;
            }
            if (hasJumped == false)
            {
                Velocity.Y = 0f;
            }

            //Sprite Collision
            foreach (var sprite in sprites)
            {
                if (sprite == this)
                    continue;

                //Touching Left/Right Sprite
                if (this.Velocity.X > 0 && this.IsTouchingLeft(sprite) ||
                    this.Velocity.X < 0 && this.IsTouchingRight(sprite))
                {
                    this.Velocity.X = 0;
                }
                //Touching Up/Down Sprite
                if (this.Velocity.Y > 0 && this.IsTouchingTop(sprite) ||
                    this.Velocity.Y < 0 && this.IsTouchingBottom(sprite))
                {
                    this.Velocity.Y = 0;
                    hasJumped = false;
                }
                else
                    hasJumped = true;
            }
          

            //Tile Collision
            foreach (var tile in Tiles)
            {
                foreach (var sprite in sprites)
                {
                    //Touching Up/Down Tile
                    if (sprite.Velocity.Y < 0 && Sprite.IsTouchingTileBottom(sprite, tile))
                    {
                        sprite.Velocity.Y = 0;
                    }
                    if (sprite.Velocity.Y > 0 && Sprite.IsTouchingTileTop(sprite, tile))
                    {
                        sprite.Velocity.Y = 0;
                        hasJumped = false;
                    }

                    //Touching Left/Right Tile
                    if (sprite.Velocity.X > 0 && Sprite.IsTouchingTileLeft(sprite, tile) ||
                    sprite.Velocity.X < 0 && Sprite.IsTouchingTileRight(sprite, tile))
                    {
                        sprite.Velocity.X = 0;
                    }

                    //Left Boundary (Smaller = Left, Bigger = Right)
                    if (sprite.Position.X <= 7)
                    {
                        sprite.Position.X = 7;
                    }

                    //Right Boundary (Smaller = Left, Bigger = Right)
                    if (sprite.Position.X >= 1550)
                    {
                        sprite.Position.X = 1550;
                    }

                    //Up Boundary (Smaller = Up, Bigger = Down)
                    if (sprite.Position.Y <= 6)
                    {
                        sprite.Position.Y = 6;
                        hasJumped = true;
                    }

                    //Down Boundary (Smaller = Up, Bigger = Down)
                    if (sprite.Position.Y >= 800)
                    {
                        sprite.Position.Y = 800;
                        hasJumped = false;
                    }
                }
            }

            //Respawn
            foreach (var sprite in sprites)
            {
                if (sprite.Health <= 0  )
                {
                    if (sprite.PlayerNum == 1 && Flag.isPickedUpP1)
                    {
                        Flag.isPickedUpP1 = false;
                    }
                    if (sprite.PlayerNum != 1 && Flag.isPickedUpP2)
                    {
                        Flag.isPickedUpP2 = false;
                    }
                    Respawn(sprite);
                }
            }

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }
        #endregion

        protected void SetAnimations()
        {
            if (Velocity.X > 0)
                _animationManager.Play(_animations["WalkRight"]);
            else if (Velocity.X < 0)
                _animationManager.Play(_animations["WalkLeft"]);
            /*else if (Velocity.Y < 0)
                _animationManager.Play(_animations["Jump"]);
            else if (Velocity.Y > 0)
                _animationManager.Play(_animations["WalkDown"]);*/
        }
        }
    }