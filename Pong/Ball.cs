using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    public class Ball : GameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D texture;
        public const int size = 50;
        Rectangle rectangle;
        public Vector2 position;
        Vector2 velocity;
        const float initialSpeedFactor = 100;
        float speedFactor;
        Rectangle field;
        float speedIncrementer = 20;

        public Ball(Game game) : base(game)
        {
            rectangle = new Rectangle(Game.Window.ClientBounds.X / 2, Game.Window.ClientBounds.Y / 2, size, size);
        }


        public override void Initialize()
        {
            speedFactor = initialSpeedFactor;
            field = Game.Window.ClientBounds;
            field.Location = new Point(0, 0);
            ResetPosition();
            base.Initialize();

        }

        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            texture = Game.Content.Load<Texture2D>("ball");
        }

        public override void Update(GameTime gameTime)
        {
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            rectangle.Location = new Point((int)(position.X - size / 2.0f), (int)(position.Y - size / 2.0f));
            if (!rectangle.Intersects(field))
            {
                //ResetPosition();
            }
            base.Update(gameTime);
        }

        

        public void Draw(GameTime gameTime)
        {
            if (texture != null)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                spriteBatch.Draw(texture, rectangle, Color.White);
                spriteBatch.End();
            }
        }

        public void ResetPosition()

        {
            speedFactor = initialSpeedFactor;
            position.X = (float)field.Width / 2.0f;
            position.Y = (float)field.Height / 2.0f;
            double angle = 45 / (double)180.0 * Math.PI;
            velocity = new Vector2(speedFactor * ((float)Math.Cos(angle)), speedFactor * ((float)Math.Sin(angle)));
        }

        public bool CheckHorizontalHit(Rectangle collisionRectangle)
        {
            bool result = false;
            if (collisionRectangle.Intersects(rectangle))
            {
                velocity.X *= -1;
                if (collisionRectangle.Center.X > rectangle.Center.X)
                {
                    position.X = collisionRectangle.X - rectangle.Width / 2;
                }
                else
                {
                    position.X = collisionRectangle.X + collisionRectangle.Width + rectangle.Width / 2;
                }
                speedFactor += speedIncrementer;
                result = true;
            }
            if (rectangle.Left > 0 && rectangle.Right < field.Right)
            {
                if (rectangle.Top <= 0)
                {
                    velocity.Y = 95;
                }
                if (rectangle.Bottom >= field.Bottom-rectangle.Height/2)
                {
                    velocity.Y = -95;
                }
            }
            return result;
        }

        public void UnloadContent()
        {

        }
    }
}
