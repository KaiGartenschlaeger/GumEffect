using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GumEffect
{
    public class GameApp : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Snake _snake;

        public GameApp()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 768;

            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        private Texture2D CreateTexture(int width, int height, Color color)
        {
            var data = new Color[width * height];
            for (int i = 0; i < data.Length; i++)
                data[i] = color;

            var texture = new Texture2D(GraphicsDevice, width, height);
            texture.SetData(data);

            return texture;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _snake = new Snake(
                CreateTexture(30, 30, Color.Orange),
                CreateTexture(15, 15, Color.Yellow),
                new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2));
        }

        protected override void Update(GameTime time)
        {
            _snake.Update(time);
            _snake.TargetPos = Mouse.GetState().Position.ToVector2();

            base.Update(time);
        }

        protected override void Draw(GameTime time)
        {
            GraphicsDevice.Clear(Color.Black);

            _snake.Draw(_spriteBatch);

            base.Draw(time);
        }
    }
}