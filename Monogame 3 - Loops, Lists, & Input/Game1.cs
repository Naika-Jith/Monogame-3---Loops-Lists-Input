using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;

namespace Monogame_3___Loops__Lists____Input
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        KeyboardState keyboardState;
        Rectangle window;
        Texture2D grassTexture;
        Texture2D mowerTexture;
        Rectangle mowerRect;

        SoundEffect mowerSound;
        SoundEffectInstance mowerSoundInstance;

        Vector2 mowerSpeed;

        List<Rectangle> grassTiles;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            window = new Rectangle(0, 0, 600, 500);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            mowerRect = new Rectangle(100, 100, 30, 30);

            grassTiles = new List<Rectangle>();
            for (int x = 0; x < window.Width; x += 5) // we will use x to rep all x coordinates across our window
                for (int y = 0; y < window.Height; y += 5) // we will use y to rep all y coordinates across our window
                    grassTiles.Add(new Rectangle(x, y, 5, 5)); // add a new rectangle to our list of grass tiles    



            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            mowerTexture = Content.Load<Texture2D>("Images/mower");
            grassTexture = Content.Load<Texture2D>("Images/long_grass");
            mowerSound = Content.Load<SoundEffect>("Sounds/mower_sound");
            mowerSoundInstance = mowerSound.CreateInstance();
            mowerSoundInstance.IsLooped = true;

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            keyboardState = Keyboard.GetState();

            mowerSpeed = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.W))
            {
                mowerSpeed.Y = -1;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                mowerSpeed.Y += 1;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                mowerSpeed.X = -1;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                mowerSpeed.X += 1;
            }

            mowerRect.Offset(mowerSpeed);

            for(int i = grassTiles.Count - 1; i >= 0; i--)
            {
                if (mowerRect.Contains(grassTiles[i]))
                {
// We cannot use a foreach loop because
//we are making a change to the List.
                    grassTiles.Remove(grassTiles[i]);
                    i--; // Decrement i to avoid skipping the next tile after removal
                }
 /// When we remove an element from the List, we need to subtract one from our
//counter because all indexes we haven’t inspected yet will decrease by 1.


            }



            if (mowerSpeed == Vector2.Zero)
                mowerSoundInstance.Stop();

            else
                mowerSoundInstance.Play();




            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGreen);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(mowerTexture, mowerRect, Color.White);

            foreach (Rectangle grassTile in grassTiles)
            {
                _spriteBatch.Draw(grassTexture, grassTile, Color.White);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
