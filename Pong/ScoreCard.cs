using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    public class ScoreCard : GameComponent
    {
        SpriteFont font;
        SpriteBatch spriteBatch;
        public int score1;
        public int score2;

        public ScoreCard(Game game) : base(game)
        {
            Reset();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            font = Game.Content.Load<SpriteFont>("ScoreFont");
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, score1.ToString(), new Vector2(100, 20), Color.LightGreen, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(font, score2.ToString(), new Vector2(Game.Window.ClientBounds.Width - 100, 20), Color.LightGreen, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.End();
            base.Update(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void UnloadContent()
        {

        }

        public void Reset()
        {
            score1 = score2 = 0;
        }
    }
}
