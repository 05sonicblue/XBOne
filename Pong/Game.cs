using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    class PongGame : Game
    {
        GraphicsDeviceManager graphics;
        Ball ball;
        Paddle paddle;
        Paddle enemyPaddle;
        bool automatedEnemy = true;
        SoundEffect hitSound;
        SoundEffect bgSound;
        SoundEffectInstance bgSoundEffectInstance;
        float bgVolume = 0.04f;
        float hitVolume = 0.05f;
        ScoreCard scoreCard;

        public PongGame()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            ball = new Ball(this);
            paddle = new Paddle(this);
            enemyPaddle = new Paddle(this, true);
            scoreCard = new ScoreCard(this);
        }

        protected override void Initialize()
        {
            ball.Initialize();
            paddle.Initialize();
            enemyPaddle.Initialize();
            scoreCard.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            ball.LoadContent();
            paddle.LoadContent();
            enemyPaddle.LoadContent();
            scoreCard.LoadContent();
            hitSound = Content.Load<SoundEffect>("hit");
            bgSound = Content.Load<SoundEffect>("bg");
            bgSoundEffectInstance = bgSound.CreateInstance();
            bgSoundEffectInstance.IsLooped = true;
            bgSoundEffectInstance.Volume = bgVolume;
            bgSoundEffectInstance.Play();
        }

        protected override void UnloadContent()
        {
            if (bgSoundEffectInstance.State == SoundState.Playing)
            {
                bgSoundEffectInstance.Stop();
            }
            
            ball.UnloadContent();
            paddle.UnloadContent();
            enemyPaddle.UnloadContent();
            scoreCard.UnloadContent();
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (automatedEnemy)
            {
                float ballYCenter = ball.position.Y - Ball.size/2;
                float paddleYCenter = enemyPaddle.position.Y - enemyPaddle.height / 2;
                float precisionPercent = ballYCenter / paddleYCenter;
                if (ballYCenter < paddleYCenter && (paddleYCenter-ballYCenter>25))
                {
                    enemyPaddle.MoveUp(gameTime);
                }
                if (ballYCenter > paddleYCenter && (ballYCenter-paddleYCenter>25))
                {
                    enemyPaddle.MoveDown(gameTime);
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                paddle.MoveUp(gameTime);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                paddle.MoveDown(gameTime);
            }
            if (ball.position.X < 0)
            {
                scoreCard.score2 += 1;
                ball.ResetPosition();
            }
            if (ball.position.X > Window.ClientBounds.Width)
            {
                scoreCard.score1 += 1;
                ball.ResetPosition();
            }
            if (ball.CheckHorizontalHit(paddle.rectangle))
            {
                hitSound.Play(hitVolume, 0.0f, 0.0f);
                paddle.SetPaddleHeight(enemyPaddle.height += 25);
            }
            if (ball.CheckHorizontalHit(enemyPaddle.rectangle))
            {
                hitSound.Play(hitVolume, 0.0f, 0.0f);
                enemyPaddle.SetPaddleHeight(enemyPaddle.height += 25);
            }
            ball.Update(gameTime);
            paddle.Update(gameTime);
            enemyPaddle.Update(gameTime);
            scoreCard.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            ball.Draw(gameTime);
            paddle.Draw(gameTime);
            enemyPaddle.Draw(gameTime);
            scoreCard.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
