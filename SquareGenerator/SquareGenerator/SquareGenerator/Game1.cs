using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SquareGenerator
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public GraphicsDevice graphicsDevice;

        Generator generate = new Generator();

        Camera2D camera2D = new Camera2D();

        public Texture2D dot;
        public Texture2D textureTest;
        int lastScrollWheelValue = Mouse.GetState().ScrollWheelValue;
        Vector2 lastMousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        
        

        public Game1()      
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            this.graphics.PreferredBackBufferWidth = 1920;
            this.graphics.PreferredBackBufferHeight = 1080;
            this.IsMouseVisible = true;
            this.graphics.IsFullScreen = true;
            this.graphics.ApplyChanges();

            generate.InitializeSquares(graphics);

            camera2D.Position = new Vector2(graphics.PreferredBackBufferWidth / 3, -graphics.PreferredBackBufferHeight / 3);
            
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textureTest = Content.Load<Texture2D>("texture");
            dot = new Texture2D(GraphicsDevice, 1, 1, true, SurfaceFormat.Color);
            dot.SetData(new[] { Color.White }); 
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            UpdateInput();

            // TODO: Add your update logic here
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin(SpriteSortMode.BackToFront,BlendState.AlphaBlend,null,null,null,null,camera2D.get_transformation(graphics.GraphicsDevice));
            generate.DrawLine(spriteBatch, dot, new Vector2(-10000, 0), new Vector2(10000, 0));
            generate.DrawLine(spriteBatch, dot, new Vector2(0, -10000), new Vector2(0, 10000));
            generate.DrawSquares(spriteBatch,dot);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void UpdateInput()
        {
            #region Keyboard
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            #endregion

            #region Mouse
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Released)
            {
                lastMousePosition = new Vector2(mouseState.X, mouseState.Y);
            }
            if (mouseState.LeftButton == ButtonState.Pressed)
            {

                camera2D.Move(lastMousePosition - new Vector2(mouseState.X, mouseState.Y));
                lastMousePosition = new Vector2(mouseState.X, mouseState.Y);
            }
            if (lastScrollWheelValue > mouseState.ScrollWheelValue)
            {
                camera2D.Zoom -= 0.1f;
                lastScrollWheelValue = mouseState.ScrollWheelValue;
            }
            if (lastScrollWheelValue < mouseState.ScrollWheelValue)
            {
                camera2D.Zoom += 0.1f;
                lastScrollWheelValue = mouseState.ScrollWheelValue;
            }
            #endregion

            
            
        }
    }
}
