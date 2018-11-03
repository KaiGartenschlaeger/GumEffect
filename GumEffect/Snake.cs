using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GumEffect
{
    public class Snake
    {
        private SnakeBodyElement[] _elements;

        private class SnakeBodyElement
        {
            public Texture2D Texture;
            public Vector2 Origin;
            public float Rotation;
            public Vector2 Pos;
            public Vector2 Velocity;
            public int? TargetElementIndex;
        }

        public Snake(Texture2D headTexture, Texture2D bodyTexture, Vector2 headPos, int bodyElemensCount = 10)
        {
            if (headTexture == null)
                throw new ArgumentNullException(nameof(headTexture));
            if (bodyTexture == null)
                throw new ArgumentNullException(nameof(bodyTexture));

            _elements = new SnakeBodyElement[bodyElemensCount];
            for (int i = 0; i < _elements.Length; i++)
            {
                _elements[i] = new SnakeBodyElement();
                _elements[i].Texture = i == 0 ? headTexture : bodyTexture;
                _elements[i].Origin = new Vector2(_elements[i].Texture.Width / 2, _elements[i].Texture.Height / 2);
                _elements[i].Pos = headPos;
                _elements[i].Rotation = 0f;
                _elements[i].Velocity = Vector2.Zero;
                if (i > 0) _elements[i].TargetElementIndex = i - 1;
            }
        }

        public void Update(GameTime time)
        {
            //velocityX = (target.x - source.x) * power
            //velocityY = (target.y - source.y) * power

            var speed = 7.5f;
            foreach (var element in _elements)
            {
                var targetPos = element.TargetElementIndex.HasValue ?
                    _elements[element.TargetElementIndex.Value].Pos
                    :
                    TargetPos;

                var velocity = new Vector2(
                    (targetPos.X - element.Pos.X) * speed,
                    (targetPos.Y - element.Pos.Y) * speed);

                element.Pos += velocity * (float)time.ElapsedGameTime.TotalSeconds;
                element.Rotation = Helper.RadianBetween(element.Pos, targetPos);
            }
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Begin();
            foreach (var element in _elements)
            {
                batch.Draw(element.Texture,
                    position: element.Pos,
                    sourceRectangle: null,
                    color: Color.White,
                    rotation: element.Rotation,
                    origin: element.Origin,
                    scale: 1f,
                    effects: SpriteEffects.None,
                    layerDepth: 0f);
            }
            batch.End();
        }

        public Vector2 TargetPos { get; set; }
    }
}