using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    public class Paddle : GameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D texture;
        public const int width = 50;
        public int height = 150;
        public Rectangle rectangle;
        public Vector2 position;
        const float initialSpeedFactor = 100;
        float speedFactor;
        Rectangle field;
        bool isEnemy = false;
        public int maxHeight = 50;

        public Paddle(Game game) : base(game)
        {
            rectangle = new Rectangle(0, 0, width, height);
        }

        public Paddle(Game game, bool _isEnemy) : base(game)
        {
            isEnemy = _isEnemy;
            rectangle = new Rectangle(0, 0, width, height);
        }

        public override void Initialize()
        {
            speedFactor = initialSpeedFactor;
            maxHeight = Game.Window.ClientBounds.Height / 2;
            field = Game.Window.ClientBounds;
            field.Location = new Point(0, 0);
            ResetPosition();
            base.Initialize();

        }

        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            texture = Game.Content.Load<Texture2D>("paddle");
        }

        public override void Update(GameTime gameTime)
        {
            rectangle.Location = new Point(Convert.ToInt32(position.X - width / 2.0f), Convert.ToInt32(position.Y - height / 2.0f));
            if (!rectangle.Intersects(field))
            {
                ResetPosition();
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
            if (isEnemy)
            {
                position.X = field.Width - 75f;
            }
            else
            {
                position.X = 75f;
            }
            position.Y = field.Height / 2.0f;
        }

        public void MoveUp(GameTime gameTime)
        {
            position.Y -= speedFactor * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (position.Y < Game.Window.ClientBounds.Top + rectangle.Height / 2)
            {
                position.Y = Game.Window.ClientBounds.Top+rectangle.Height/2;
            }
        }

        public void MoveDown(GameTime gameTime)
        {
            position.Y += speedFactor * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (position.Y > Game.Window.ClientBounds.Bottom - rectangle.Height / 2)
            {
                position.Y = Game.Window.ClientBounds.Bottom - rectangle.Height / 2;
            }
        }

        public void UnloadContent()
        {

        }

        public void SetPaddleHeight(Int32 value)
        {
            if (value > maxHeight)
            {
                value = maxHeight;
            }
            height = value;
            rectangle.Height = value;
        }
    }
}
