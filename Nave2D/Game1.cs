using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using Nave2D.Scripts;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Nave2D
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D baseImage;
        Point window;

        float deltaTime, timeToShoot, nextShot;


        //Player
        Player player;
        List<Bullet> playerBulletPool = new List<Bullet>();

        //Enemy

        //Non-interactive
        Background[] backgrounds = new Background[2] { new Background(), new Background() };

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 600;
            window = new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            baseImage = Content.Load<Texture2D>("Images\\Background");
            for (int i = 0; i < backgrounds.Length; i++)
            {
                backgrounds[i].speed = 60;
                backgrounds[i].image = baseImage;
                backgrounds[i].position = i > 0 ? new Vector2(0, -backgrounds[i].image.Height) : new Vector2(0, 0);
            }

            baseImage = Content.Load<Texture2D>("Images\\nave-2d");
            player = new Player
            (
                RandomColor(),
                new Vector2(50, 50),
                baseImage,
                new Input(),
                graphics,
                position: new Vector2(window.X / 2.1f, (window.Y * 1.5f) - baseImage.Height)
            );

            //playerBulletPool.Add(NewBullet());
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);
            for (int i = 0; i < backgrounds.Length; i++)
            {
                backgrounds[i].Move(deltaTime);
                if (backgrounds[i].position.Y >= backgrounds[i].image.Height)
                    backgrounds[i].position.Y = -backgrounds[i].image.Height;
            }
            for (int i = 0; i < playerBulletPool.Count; i++)
                playerBulletPool[i].Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(player.Input.Shoot))
                Shoot();


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();


            for (int i = 0; i < backgrounds.Length; i++)
                backgrounds[i].Draw(spriteBatch);
            for (int i = 0; i < playerBulletPool.Count; i++)
                playerBulletPool[i].Draw(spriteBatch);

            player.Draw(spriteBatch);


            spriteBatch.End();
            base.Draw(gameTime);
        }

        Bullet Shoot()
        {
            timeToShoot += deltaTime;
            if (timeToShoot >= nextShot)
            {
                nextShot += 0.05f;
                foreach (Bullet bullet in playerBulletPool)
                {
                    if (bullet.Status == Status.Inactive)
                    {
                        bullet.Position.X = player.Position.X + (player.Size.X / 2.5f);
                        bullet.Position.Y = player.Position.Y;
                        //bullet.Color = RandomColor();
                        bullet.Status = Status.Active;
                        return bullet;
                    }
                }
                playerBulletPool.Add(NewBullet());
                return playerBulletPool.LastOrDefault();
            }
            return null;
        }

        Bullet NewBullet()
        {
            baseImage = Content.Load<Texture2D>("Images\\Bullet");
            return new Bullet()
            {
                Speed = 10,
                Image = baseImage,
                Graphics = graphics,
                Color = Color.White,
                Status = Status.Active,
                Size = new Vector2(15, 20),
                Direction = new Vector2(0, -3),
                inactivePosition = new Vector2(window.X / 2, window.Y + 100),
                Position = new Vector2(player.Position.X + (player.Size.X / 2.5f), player.Position.Y),
                Transform = new Rectangle((int)(player.Position.X + (player.Size.X / 2.5f)), (int)player.Position.Y, 15, 20)
            };
        }

        public Color RandomColor(bool hasRed = true, bool hasBlue = true, bool hasGreen = true)
        {
            Random r = new Random();
            return new Color(
                hasRed ? r.Next(100, 256) : 0,
                hasBlue ? r.Next(100, 256) : 0,
                hasGreen ? r.Next(100, 256) : 0);
        }
    }
}
