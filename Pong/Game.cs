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
        GameTime lastRecordedGameTime;
        DateTime startTime;

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
            Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().CoreWindow.KeyDown += CoreWindow_KeyDown;
            startTime = DateTime.Now;
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            TimeSpan elapsedTime = DateTime.Now - startTime;
            GameTime gameTime = lastRecordedGameTime; //Using last recorded gametime since there is no easy way to calculate it
            switch (args.VirtualKey)

            {
                case Windows.System.VirtualKey.None:
                    break;
                case Windows.System.VirtualKey.LeftButton:
                    break;
                case Windows.System.VirtualKey.RightButton:
                    break;
                case Windows.System.VirtualKey.Cancel:
                    break;
                case Windows.System.VirtualKey.MiddleButton:
                    break;
                case Windows.System.VirtualKey.XButton1:
                    break;
                case Windows.System.VirtualKey.XButton2:
                    break;
                case Windows.System.VirtualKey.Back:
                    break;
                case Windows.System.VirtualKey.Tab:
                    break;
                case Windows.System.VirtualKey.Clear:
                    break;
                case Windows.System.VirtualKey.Enter:
                    break;
                case Windows.System.VirtualKey.Shift:
                    break;
                case Windows.System.VirtualKey.Control:
                    break;
                case Windows.System.VirtualKey.Menu:
                    break;
                case Windows.System.VirtualKey.Pause:
                    break;
                case Windows.System.VirtualKey.CapitalLock:
                    break;
                case Windows.System.VirtualKey.Kana:
                    break;
                case Windows.System.VirtualKey.Junja:
                    break;
                case Windows.System.VirtualKey.Final:
                    break;
                case Windows.System.VirtualKey.Hanja:
                    break;
                case Windows.System.VirtualKey.Escape:
                    break;
                case Windows.System.VirtualKey.Convert:
                    break;
                case Windows.System.VirtualKey.NonConvert:
                    break;
                case Windows.System.VirtualKey.Accept:
                    break;
                case Windows.System.VirtualKey.ModeChange:
                    break;
                case Windows.System.VirtualKey.Space:
                    break;
                case Windows.System.VirtualKey.PageUp:
                    break;
                case Windows.System.VirtualKey.PageDown:
                    break;
                case Windows.System.VirtualKey.End:
                    break;
                case Windows.System.VirtualKey.Home:
                    break;
                case Windows.System.VirtualKey.Left:
                    break;
                case Windows.System.VirtualKey.Up:
                    paddle.MoveUp(gameTime);
                    break;
                case Windows.System.VirtualKey.Right:
                    break;
                case Windows.System.VirtualKey.Down:
                    paddle.MoveDown(gameTime);
                    break;
                case Windows.System.VirtualKey.Select:
                    break;
                case Windows.System.VirtualKey.Print:
                    break;
                case Windows.System.VirtualKey.Execute:
                    break;
                case Windows.System.VirtualKey.Snapshot:
                    break;
                case Windows.System.VirtualKey.Insert:
                    break;
                case Windows.System.VirtualKey.Delete:
                    break;
                case Windows.System.VirtualKey.Help:
                    break;
                case Windows.System.VirtualKey.Number0:
                    break;
                case Windows.System.VirtualKey.Number1:
                    break;
                case Windows.System.VirtualKey.Number2:
                    break;
                case Windows.System.VirtualKey.Number3:
                    break;
                case Windows.System.VirtualKey.Number4:
                    break;
                case Windows.System.VirtualKey.Number5:
                    break;
                case Windows.System.VirtualKey.Number6:
                    break;
                case Windows.System.VirtualKey.Number7:
                    break;
                case Windows.System.VirtualKey.Number8:
                    break;
                case Windows.System.VirtualKey.Number9:
                    break;
                case Windows.System.VirtualKey.A:
                    break;
                case Windows.System.VirtualKey.B:
                    break;
                case Windows.System.VirtualKey.C:
                    break;
                case Windows.System.VirtualKey.D:
                    break;
                case Windows.System.VirtualKey.E:
                    break;
                case Windows.System.VirtualKey.F:
                    break;
                case Windows.System.VirtualKey.G:
                    break;
                case Windows.System.VirtualKey.H:
                    break;
                case Windows.System.VirtualKey.I:
                    break;
                case Windows.System.VirtualKey.J:
                    break;
                case Windows.System.VirtualKey.K:
                    break;
                case Windows.System.VirtualKey.L:
                    break;
                case Windows.System.VirtualKey.M:
                    break;
                case Windows.System.VirtualKey.N:
                    break;
                case Windows.System.VirtualKey.O:
                    break;
                case Windows.System.VirtualKey.P:
                    break;
                case Windows.System.VirtualKey.Q:
                    break;
                case Windows.System.VirtualKey.R:
                    break;
                case Windows.System.VirtualKey.S:
                    break;
                case Windows.System.VirtualKey.T:
                    break;
                case Windows.System.VirtualKey.U:
                    break;
                case Windows.System.VirtualKey.V:
                    break;
                case Windows.System.VirtualKey.W:
                    break;
                case Windows.System.VirtualKey.X:
                    break;
                case Windows.System.VirtualKey.Y:
                    break;
                case Windows.System.VirtualKey.Z:
                    break;
                case Windows.System.VirtualKey.LeftWindows:
                    break;
                case Windows.System.VirtualKey.RightWindows:
                    break;
                case Windows.System.VirtualKey.Application:
                    break;
                case Windows.System.VirtualKey.Sleep:
                    break;
                case Windows.System.VirtualKey.NumberPad0:
                    break;
                case Windows.System.VirtualKey.NumberPad1:
                    break;
                case Windows.System.VirtualKey.NumberPad2:
                    break;
                case Windows.System.VirtualKey.NumberPad3:
                    break;
                case Windows.System.VirtualKey.NumberPad4:
                    break;
                case Windows.System.VirtualKey.NumberPad5:
                    break;
                case Windows.System.VirtualKey.NumberPad6:
                    break;
                case Windows.System.VirtualKey.NumberPad7:
                    break;
                case Windows.System.VirtualKey.NumberPad8:
                    break;
                case Windows.System.VirtualKey.NumberPad9:
                    break;
                case Windows.System.VirtualKey.Multiply:
                    break;
                case Windows.System.VirtualKey.Add:
                    break;
                case Windows.System.VirtualKey.Separator:
                    break;
                case Windows.System.VirtualKey.Subtract:
                    break;
                case Windows.System.VirtualKey.Decimal:
                    break;
                case Windows.System.VirtualKey.Divide:
                    break;
                case Windows.System.VirtualKey.F1:
                    break;
                case Windows.System.VirtualKey.F2:
                    break;
                case Windows.System.VirtualKey.F3:
                    break;
                case Windows.System.VirtualKey.F4:
                    break;
                case Windows.System.VirtualKey.F5:
                    break;
                case Windows.System.VirtualKey.F6:
                    break;
                case Windows.System.VirtualKey.F7:
                    break;
                case Windows.System.VirtualKey.F8:
                    break;
                case Windows.System.VirtualKey.F9:
                    break;
                case Windows.System.VirtualKey.F10:
                    break;
                case Windows.System.VirtualKey.F11:
                    break;
                case Windows.System.VirtualKey.F12:
                    break;
                case Windows.System.VirtualKey.F13:
                    break;
                case Windows.System.VirtualKey.F14:
                    break;
                case Windows.System.VirtualKey.F15:
                    break;
                case Windows.System.VirtualKey.F16:
                    break;
                case Windows.System.VirtualKey.F17:
                    break;
                case Windows.System.VirtualKey.F18:
                    break;
                case Windows.System.VirtualKey.F19:
                    break;
                case Windows.System.VirtualKey.F20:
                    break;
                case Windows.System.VirtualKey.F21:
                    break;
                case Windows.System.VirtualKey.F22:
                    break;
                case Windows.System.VirtualKey.F23:
                    break;
                case Windows.System.VirtualKey.F24:
                    break;
                case Windows.System.VirtualKey.NavigationView:
                    break;
                case Windows.System.VirtualKey.NavigationMenu:
                    break;
                case Windows.System.VirtualKey.NavigationUp:
                    break;
                case Windows.System.VirtualKey.NavigationDown:
                    break;
                case Windows.System.VirtualKey.NavigationLeft:
                    break;
                case Windows.System.VirtualKey.NavigationRight:
                    break;
                case Windows.System.VirtualKey.NavigationAccept:
                    break;
                case Windows.System.VirtualKey.NavigationCancel:
                    break;
                case Windows.System.VirtualKey.NumberKeyLock:
                    break;
                case Windows.System.VirtualKey.Scroll:
                    break;
                case Windows.System.VirtualKey.LeftShift:
                    break;
                case Windows.System.VirtualKey.RightShift:
                    break;
                case Windows.System.VirtualKey.LeftControl:
                    break;
                case Windows.System.VirtualKey.RightControl:
                    break;
                case Windows.System.VirtualKey.LeftMenu:
                    break;
                case Windows.System.VirtualKey.RightMenu:
                    break;
                case Windows.System.VirtualKey.GoBack:
                    break;
                case Windows.System.VirtualKey.GoForward:
                    break;
                case Windows.System.VirtualKey.Refresh:
                    break;
                case Windows.System.VirtualKey.Stop:
                    break;
                case Windows.System.VirtualKey.Search:
                    break;
                case Windows.System.VirtualKey.Favorites:
                    break;
                case Windows.System.VirtualKey.GoHome:
                    break;
                case Windows.System.VirtualKey.GamepadA:
                    break;
                case Windows.System.VirtualKey.GamepadB:
                    break;
                case Windows.System.VirtualKey.GamepadX:
                    break;
                case Windows.System.VirtualKey.GamepadY:
                    break;
                case Windows.System.VirtualKey.GamepadRightShoulder:
                    break;
                case Windows.System.VirtualKey.GamepadLeftShoulder:
                    break;
                case Windows.System.VirtualKey.GamepadLeftTrigger:
                    break;
                case Windows.System.VirtualKey.GamepadRightTrigger:
                    break;
                case Windows.System.VirtualKey.GamepadDPadUp:
                    paddle.MoveUp(gameTime);
                    break;
                case Windows.System.VirtualKey.GamepadDPadDown:
                    paddle.MoveDown(gameTime);
                    break;
                case Windows.System.VirtualKey.GamepadDPadLeft:
                    break;
                case Windows.System.VirtualKey.GamepadDPadRight:
                    break;
                case Windows.System.VirtualKey.GamepadMenu:
                    break;
                case Windows.System.VirtualKey.GamepadView:
                    break;
                case Windows.System.VirtualKey.GamepadLeftThumbstickButton:
                    break;
                case Windows.System.VirtualKey.GamepadRightThumbstickButton:
                    break;
                case Windows.System.VirtualKey.GamepadLeftThumbstickUp:
                    paddle.MoveUp(gameTime);
                    break;
                case Windows.System.VirtualKey.GamepadLeftThumbstickDown:
                    paddle.MoveDown(gameTime);
                    break;
                case Windows.System.VirtualKey.GamepadLeftThumbstickRight:
                    break;
                case Windows.System.VirtualKey.GamepadLeftThumbstickLeft:
                    break;
                case Windows.System.VirtualKey.GamepadRightThumbstickUp:
                    break;
                case Windows.System.VirtualKey.GamepadRightThumbstickDown:
                    break;
                case Windows.System.VirtualKey.GamepadRightThumbstickRight:
                    break;
                case Windows.System.VirtualKey.GamepadRightThumbstickLeft:
                    break;
                default:
                    break;
            }
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
            lastRecordedGameTime = gameTime;
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
            //if (Keyboard.GetState().IsKeyDown(Keys.Up))
            //{
            //    paddle.MoveUp(gameTime);
            //}

            //if (Keyboard.GetState().IsKeyDown(Keys.Down))
            //{
            //    paddle.MoveDown(gameTime);
            //}

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
