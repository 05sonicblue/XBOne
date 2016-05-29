using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missiles
{
    public class MissileGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;
        Texture2D backgroundTexture;
        Texture2D foregroundTexture;
        PlayerData[] players;
        Texture2D carriageTexture;
        Texture2D cannonTexture;
        int playerCount = 4;
        int screenWidth;
        int screenHeight;
        float playerScaling;
        int currentPlayer = 0;
        SpriteFont font;
        Texture2D missileTexture;
        bool missileFlying = false;
        Vector2 missilePosition;
        Vector2 missileDirection;
        float missileAngle;
        float missileScaling = 0.1f;
        Texture2D smokeTexture;
        List<Vector2> smokeList = new List<Vector2>();
        Random randomizer = new Random();
        int[] terrainCountor;
        Texture2D groundTexture;
        Color[,] missileColorArray;
        Color[,] foregroundColorArray;
        Color[,] carriageColorArray;
        Color[,] cannonColorArray;
        Texture2D explosionTexture;
        List<ParticleData> particleList = new List<ParticleData>();
        Color[,] explosionColorArray;
        SoundEffect launchSoundEffect;
        SoundEffect terrainHitSoundEffect;
        SoundEffect carriageHitSoundEffect;
        const bool resultionIndependent = false;
        Vector2 baseScreenSize = new Vector2(800, 600);

        public MissileGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.ApplyChanges();
            Window.Title = "Missiles";
            Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().CoreWindow.KeyDown += CoreWindow_KeyDown;
            base.Initialize();
        }

        private void Launch()
        {
            missileFlying = true;
            missilePosition = players[currentPlayer].Position;
            missilePosition.X += 20;
            missilePosition.Y -= 10;
            missileAngle = players[currentPlayer].Angle;
            Vector2 up = new Vector2(0, -1);
            Matrix rotationalMatrix = Matrix.CreateRotationZ(missileAngle);
            missileDirection = Vector2.Transform(up, rotationalMatrix);
            missileDirection *= players[currentPlayer].Power / 50.0f;
            launchSoundEffect.Play();
        }

        private void PowerUp(bool turbo)
        {
            if (turbo)
            {
                players[currentPlayer].Power += 10;
            }
            else
            {
                players[currentPlayer].Power += 1;
            }
            if (players[currentPlayer].Power > 1000)
                players[currentPlayer].Power = 1000;
            if (players[currentPlayer].Power < 0)
                players[currentPlayer].Power = 0;
        }

        private void PowerDown(bool turbo)
        {
            if (turbo)
            {
                players[currentPlayer].Power -= 10;
            }
            else
            {
                players[currentPlayer].Power -= 1;
            }
            if (players[currentPlayer].Power > 1000)
                players[currentPlayer].Power = 1000;
            if (players[currentPlayer].Power < 0)
                players[currentPlayer].Power = 0;
        }

        private void MoveLeft()
        {
            players[currentPlayer].Angle -= 0.01f;
            if (players[currentPlayer].Angle > MathHelper.PiOver2)
                players[currentPlayer].Angle = -MathHelper.PiOver2;
            if (players[currentPlayer].Angle < -MathHelper.PiOver2)
                players[currentPlayer].Angle = MathHelper.PiOver2;
        }

        private void MoveRight()
        {
            players[currentPlayer].Angle += 0.01f;
            if (players[currentPlayer].Angle > MathHelper.PiOver2)
                players[currentPlayer].Angle = -MathHelper.PiOver2;
            if (players[currentPlayer].Angle < -MathHelper.PiOver2)
                players[currentPlayer].Angle = MathHelper.PiOver2;
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if (args.VirtualKey == Windows.System.VirtualKey.Escape)
            {
                this.Exit();
            }
            if (args.VirtualKey == Windows.System.VirtualKey.Space || args.VirtualKey == Windows.System.VirtualKey.GamepadA)
            {
                Launch();
            }
            if (args.VirtualKey == Windows.System.VirtualKey.Up || args.VirtualKey == Windows.System.VirtualKey.GamepadDPadUp || args.VirtualKey == Windows.System.VirtualKey.GamepadLeftThumbstickUp)
            {
                PowerUp(false);
            }
            if (args.VirtualKey == Windows.System.VirtualKey.Down || args.VirtualKey == Windows.System.VirtualKey.GamepadDPadDown || args.VirtualKey == Windows.System.VirtualKey.GamepadLeftThumbstickDown)
            {
                PowerDown(false);
            }
            if (args.VirtualKey == Windows.System.VirtualKey.PageUp || args.VirtualKey == Windows.System.VirtualKey.GamepadRightThumbstickUp)
            {
                PowerUp(true);
            }
            if (args.VirtualKey == Windows.System.VirtualKey.PageDown || args.VirtualKey == Windows.System.VirtualKey.GamepadRightThumbstickDown)
            {
                PowerDown(true);
            }
            if (args.VirtualKey == Windows.System.VirtualKey.Left || args.VirtualKey == Windows.System.VirtualKey.GamepadDPadLeft || args.VirtualKey == Windows.System.VirtualKey.GamepadLeftThumbstickLeft)
            {
                MoveLeft();
            }
            if (args.VirtualKey == Windows.System.VirtualKey.Right || args.VirtualKey == Windows.System.VirtualKey.GamepadDPadRight || args.VirtualKey == Windows.System.VirtualKey.GamepadLeftThumbstickRight)
            {
                MoveRight();
            }
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;
            backgroundTexture = Content.Load<Texture2D>("background");
            carriageTexture = Content.Load<Texture2D>("carriage");
            cannonTexture = Content.Load<Texture2D>("cannon");
            font = Content.Load<SpriteFont>("font");
            missileTexture = Content.Load<Texture2D>("missile");
            smokeTexture = Content.Load<Texture2D>("smoke");
            groundTexture = Content.Load<Texture2D>("ground");
            explosionTexture = Content.Load<Texture2D>("explosion");
            launchSoundEffect = Content.Load<SoundEffect>("launch");
            terrainHitSoundEffect = Content.Load<SoundEffect>("terrainhit");
            carriageHitSoundEffect = Content.Load<SoundEffect>("playerhit");
            if (resultionIndependent)
            {
                screenWidth = (int)baseScreenSize.X;
                screenHeight = (int)baseScreenSize.Y;
            }
            else
            {
                screenWidth = device.PresentationParameters.BackBufferWidth;
                screenHeight = device.PresentationParameters.BackBufferHeight;
            }
            playerScaling = 40.0f / (float)carriageTexture.Width;
            GenerateTerrainContour();
            SetupPlayers();
            FlattenTerrainBelowPlayers();
            CreateForeground();
            missileColorArray = TextureTo2DArray(missileTexture);
            carriageColorArray = TextureTo2DArray(carriageTexture);
            cannonColorArray = TextureTo2DArray(cannonTexture);
            explosionColorArray = TextureTo2DArray(explosionTexture);
            
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            UpdateMissile();
            if (missileFlying)
            {
                UpdateMissile();
                CheckCollisions(gameTime);
            }
            if (particleList.Count > 0)
            {
                UpdateParticles(gameTime);
            }
            base.Update(gameTime);
        }

        private void UpdateParticles(GameTime gameTime)
        {
            float now = (float)gameTime.TotalGameTime.TotalMilliseconds;
            for (int i = particleList.Count - 1; i >= 0; i--)
            {
                ParticleData particle = particleList[i];
                float timeAlive = now - particle.BirthTime;

                if (timeAlive > particle.MaxAge)
                {
                    particleList.RemoveAt(i);
                }
                else
                {
                    float relAge = timeAlive / particle.MaxAge;
                    particle.Position = 0.5f * particle.Acceleration * relAge * relAge + particle.Direction * relAge + particle.OriginalPosition;
                    float invAge = 1.0f - relAge;
                    particle.ModColor = new Color(new Vector4(invAge, invAge, invAge, invAge));
                    Vector2 positionFromCenter = particle.Position - particle.OriginalPosition;
                    float distance = positionFromCenter.Length();
                    particle.Scaling = (50.0f + distance) / 200.0f;
                    particleList[i] = particle;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Vector3 screenScalingFactor;
            if (resultionIndependent)
            {
                float horScaling = (float)device.PresentationParameters.BackBufferWidth / baseScreenSize.X;
                float verScaling = (float)device.PresentationParameters.BackBufferHeight / baseScreenSize.Y;
                screenScalingFactor = new Vector3(horScaling, verScaling, 1);
            }
            else
            {
                screenScalingFactor = new Vector3(1, 1, 1);
            }
            Matrix globalTransformation = Matrix.CreateScale(screenScalingFactor);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, globalTransformation);
            DrawScenery();
            DrawPlayers();
            DrawMissile();
            DrawSmoke();
            DrawText();
            spriteBatch.End();
            
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, globalTransformation);
            DrawExplosion();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void UpdateMissile()
        {
            if (missileFlying)
            {
                Vector2 gravity = new Vector2(0, 1);
                missileDirection += gravity / 10.0f;
                missilePosition += missileDirection;
                missileAngle = (float)Math.Atan2(missileDirection.X, -missileDirection.Y);

                for (int i = 0; i < 5; i++)
                {
                    Vector2 smokePosition = missilePosition;
                    smokePosition.X += randomizer.Next(10) - 5;
                    smokePosition.Y += randomizer.Next(10) - 5;
                    smokeList.Add(smokePosition);
                }

            }
        }

        private void DrawScenery()
        {
            Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);
            spriteBatch.Draw(foregroundTexture, screenRectangle, Color.White);
        }

        private void DrawText()
        {
            PlayerData player = players[currentPlayer];
            int currentAngle = (int)MathHelper.ToDegrees(player.Angle);
            spriteBatch.DrawString(font, "Cannon angle: " + currentAngle.ToString(), new Vector2(20, 20), player.Color);
            spriteBatch.DrawString(font, "Cannon power: " + player.Power.ToString(), new Vector2(20, 45), player.Color);
        }

        private void DrawPlayers()
        {
            foreach (PlayerData player in players)
            {
                if (player.IsAlive)
                {
                    int xPos = (int)player.Position.X;
                    int yPos = (int)player.Position.Y;
                    Vector2 cannonOrigin = new Vector2(11, 50);

                    spriteBatch.Draw(cannonTexture, new Vector2(xPos + 20, yPos - 10), null, player.Color, player.Angle, cannonOrigin, playerScaling, SpriteEffects.None, 1);
                    spriteBatch.Draw(carriageTexture, player.Position, null, player.Color, 0, new Vector2(0, carriageTexture.Height), playerScaling, SpriteEffects.None, 0);
                }
            }
        }

        private void DrawMissile()
        {
            if (missileFlying)
                spriteBatch.Draw(missileTexture, missilePosition, null, players[currentPlayer].Color, missileAngle, new Vector2(42, 240), 0.1f, SpriteEffects.None, 1);
        }

        private void DrawSmoke()
        {
            if (smokeList != null && smokeList.Count > 0)
            {
                foreach (var smokePosition in smokeList)
                {
                    spriteBatch.Draw(smokeTexture, smokePosition, null, Color.White, 0, new Vector2(40, 35), 0.2f, SpriteEffects.None, 1);
                }
            }
        }

        private void GenerateTerrainContour()
        {
            terrainCountor = new int[screenWidth];
            double firstRandom = randomizer.NextDouble() + 1;
            double secondRandom = randomizer.NextDouble() + 2;
            double thirdRandom = randomizer.NextDouble() + 3;
            float offset = screenHeight / 2;
            float peakHeight = 100;
            float flatness = 70;

            for (int i = 0; i < screenWidth; i++)
            {
                double height = peakHeight / firstRandom * Math.Sin(i / flatness * firstRandom + firstRandom);
                height += peakHeight / secondRandom * Math.Sin(i / flatness * secondRandom + secondRandom);
                height += peakHeight / thirdRandom * Math.Sin(i / flatness * thirdRandom + thirdRandom);
                height += offset;
                terrainCountor[i] = (int)height;
            }
        }

        private void CreateForeground()
        {
            Color[,] groundColors = TextureTo2DArray(groundTexture);
            Color[] foregroundColors = new Color[screenWidth * screenHeight];
            for (int i = 0; i < screenWidth; i++)
            {
                for (int j = 0; j < screenHeight; j++)
                {
                    if (j > terrainCountor.ElementAt(i))
                    {
                        foregroundColors[i + j * screenWidth] = groundColors[i % groundTexture.Width, j % groundTexture.Height];
                    }
                    else
                    {
                        foregroundColors[i + j * screenWidth] = Color.Transparent;
                    }
                }
            }
            foregroundTexture = new Texture2D(device, screenWidth, screenHeight, false, SurfaceFormat.Color);
            foregroundTexture.SetData(foregroundColors.ToArray());
            foregroundColorArray = TextureTo2DArray(foregroundTexture);
        }

        private void FlattenTerrainBelowPlayers()
        {
            if (players != null && players.Length > 0)
            {
                foreach (var player in players)
                {
                    if (player.IsAlive)
                    {
                        for (int i = 0; i < 40; i++)
                        {
                            terrainCountor[(int)player.Position.X + i] = terrainCountor[(int)player.Position.X];
                        }
                    }
                }
            }
        }

        private Color[,] TextureTo2DArray(Texture2D texture)
        {
            Color[,] result = new Color[texture.Width, texture.Height];
            Color[] colors = new Color[texture.Width * texture.Height];
            texture.GetData(colors);
            for (int i = 0; i < texture.Width; i++)
            {
                for (int j = 0; j < texture.Height; j++)
                {
                    result[i, j] = colors[i + j * texture.Width];
                }
            }
            return result;
        }

        private void NextPlayer()
        {
            currentPlayer = currentPlayer + 1;
            currentPlayer = currentPlayer % playerCount;
            while (!players[currentPlayer].IsAlive)
                currentPlayer = ++currentPlayer % playerCount;
        }

        private Vector2 TexturesCollide(Color[,] texture1, Matrix matrix1, Color[,] texture2, Matrix matrix2)
        {
            Matrix transformedMatrix = matrix1 * Matrix.Invert(matrix2);
            int width1 = texture1.GetLength(0);
            int height1 = texture1.GetLength(1);
            int width2 = texture2.GetLength(0);
            int height2 = texture2.GetLength(1);
            for (int i1 = 0; i1 < width1; i1++)
            {
                for (int j1 = 0; j1 < height1; j1++)
                {
                    Vector2 position1 = new Vector2(i1, j1);
                    Vector2 position2 = Vector2.Transform(position1, transformedMatrix);
                    int i2 = (int)position2.X;
                    int j2 = (int)position2.Y;
                    if (i2 >= 0 && i2 < width2 && j2 >= 0 && j2 < height2)
                    {
                        if (texture1[i1, j1].A > 0)
                        {
                            if (texture2[i2, j2].A > 0)
                            {
                                return Vector2.Transform(position1, matrix1);
                            }
                        }
                    }
                }
            }
            return new Vector2(-1, -1);
        }

        private Vector2 CheckTerrainCollision()
        {
            Matrix missileMatrix = Matrix.CreateTranslation(-10, -240, 0) * Matrix.CreateRotationZ(missileAngle) * Matrix.CreateScale(missileScaling) * Matrix.CreateTranslation(missilePosition.X, missilePosition.Y, 0);
            Matrix terrainMatrix = Matrix.Identity;
            return TexturesCollide(missileColorArray, missileMatrix, foregroundColorArray, terrainMatrix);
        }

        private Vector2 CheckPlayersCollision()
        {
            Matrix missileMatrix = Matrix.CreateTranslation(-42, -240, 0) * Matrix.CreateRotationZ(missileAngle) * Matrix.CreateScale(missileScaling) * Matrix.CreateTranslation(missilePosition.X, missilePosition.Y, 0);
            for (int i = 0; i < playerCount; i++)
            {
                PlayerData player = players[i];
                if (player.IsAlive)
                {
                    if (i != currentPlayer)
                    {
                        int xPos = (int)player.Position.X;
                        int yPos = (int)player.Position.Y;

                        Matrix carriageMatrix = Matrix.CreateTranslation(0, -carriageTexture.Height, 0) * Matrix.CreateScale(playerScaling) * Matrix.CreateTranslation(xPos, yPos, 0);
                        Vector2 carriageCollisionPoint = TexturesCollide(carriageColorArray, carriageMatrix, missileColorArray, missileMatrix);
                        if (carriageCollisionPoint.X > -1)
                        {
                            players[i].IsAlive = false;
                            return carriageCollisionPoint;
                        }

                        Matrix cannonMatrix = Matrix.CreateTranslation(-11, -50, 0) * Matrix.CreateRotationZ(player.Angle) * Matrix.CreateScale(playerScaling) * Matrix.CreateTranslation(xPos + 20, yPos - 10, 0);
                        Vector2 cannonCollisionPoint = TexturesCollide(cannonColorArray, cannonMatrix, missileColorArray, missileMatrix);
                        if (cannonCollisionPoint.X > -1)
                        {
                            players[i].IsAlive = false;
                            return cannonCollisionPoint;
                        }
                    }
                }
            }
            return new Vector2(-1, -1);
        }

        private bool CheckOutOfScreen()
        {
            bool missileOutOfScreen = missilePosition.Y > screenHeight;
            missileOutOfScreen |= missilePosition.X < 0;
            missileOutOfScreen |= missilePosition.X > screenWidth;
            return missileOutOfScreen;
        }

        private void CheckCollisions(GameTime gameTime)
        {
            Vector2 terrainCollisionPoint = CheckTerrainCollision();
            Vector2 playerCollisionPoint = CheckPlayersCollision();
            bool missileOutOfScreen = CheckOutOfScreen();

            if (playerCollisionPoint.X > -1)
            {
                missileFlying = false;
                smokeList = new List<Vector2>();
                AddExplosion(playerCollisionPoint, 10, 80.0f, 2000.0f, gameTime);
                carriageHitSoundEffect.Play();
                NextPlayer();
            }

            if (terrainCollisionPoint.X > -1)
            {
                missileFlying = false;
                smokeList = new List<Vector2>();
                AddExplosion(terrainCollisionPoint, 4, 30.0f, 1000.0f, gameTime);
                terrainHitSoundEffect.Play();
                NextPlayer();
            }

            if (missileOutOfScreen)
            {
                missileFlying = false;

                smokeList = new List<Vector2>();
                NextPlayer();
            }
        }

        private void AddExplosion(Vector2 explosionPos, int numberOfParticles, float size, float maxAge, GameTime gameTime)
        {
            for (int i = 0; i < numberOfParticles; i++)
                AddExplosionParticle(explosionPos, size, maxAge, gameTime);
            float rotation = (float)randomizer.Next(10);
            Matrix mat = Matrix.CreateTranslation(-explosionTexture.Width / 2, -explosionTexture.Height / 2, 0) * Matrix.CreateRotationZ(rotation) * Matrix.CreateScale(size / (float)explosionTexture.Width * 2.0f) * Matrix.CreateTranslation(explosionPos.X, explosionPos.Y, 0);
            AddCrater(explosionColorArray, mat);

            for (int i = 0; i < players.Length; i++)
                players[i].Position = new Vector2(players[i].Position.X, terrainCountor[(int)players[i].Position.X]);
            FlattenTerrainBelowPlayers();
            CreateForeground();
        }

        private void AddCrater(Color[,] texture, Matrix matrix)
        {
            int width = texture.GetLength(0);
            int height = texture.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (texture[x, y].R > 10)
                    {
                        Vector2 imagePosition = new Vector2(x, y);
                        Vector2 screenPosition = Vector2.Transform(imagePosition, matrix);

                        int screenX = (int)screenPosition.X;
                        int screenY = (int)screenPosition.Y;

                        if ((screenX) > 0 && (screenX < screenWidth))
                            if (terrainCountor[screenX] < screenY)
                                terrainCountor[screenX] = screenY;
                    }
                }
            }
        }

        private void AddExplosionParticle(Vector2 explosionPos, float explosionSize, float maxAge, GameTime gameTime)
        {
            ParticleData particle = new ParticleData();
            particle.OriginalPosition = explosionPos;
            particle.Position = particle.OriginalPosition;
            particle.BirthTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
            particle.MaxAge = maxAge;
            particle.Scaling = 0.25f;
            particle.ModColor = Color.White;
            float particleDistance = (float)randomizer.NextDouble() * explosionSize;
            Vector2 displacement = new Vector2(particleDistance, 0);
            float angle = MathHelper.ToRadians(randomizer.Next(360));
            displacement = Vector2.Transform(displacement, Matrix.CreateRotationZ(angle));
            particle.Direction = displacement;
            particle.Acceleration = 3.0f * particle.Direction;
            particleList.Add(particle);
        }

        private void DrawExplosion()
        {
            for (int i = 0; i < particleList.Count; i++)
            {
                ParticleData particle = particleList[i];
                spriteBatch.Draw(explosionTexture, particle.Position, null, particle.ModColor, i, new Vector2(256, 256), particle.Scaling, SpriteEffects.None, 1);
            }
        }


        private void SetupPlayers()
        {
            List<Color> playerColors = new List<Color>();
            playerColors.Add(Color.Red);
            playerColors.Add(Color.Green);
            playerColors.Add(Color.Blue);
            playerColors.Add(Color.Purple);
            playerColors.Add(Color.Orange);
            playerColors.Add(Color.Indigo);
            playerColors.Add(Color.Yellow);
            playerColors.Add(Color.SaddleBrown);
            playerColors.Add(Color.Tomato);
            playerColors.Add(Color.Turquoise);

            players = new PlayerData[playerCount];
            for (int i = 0; i < playerCount; i++)
            {
                players[i] = new PlayerData();
                players[i].IsAlive = true;
                players[i].Color = playerColors[i];
                players[i].Angle = MathHelper.ToRadians(90);
                players[i].Power = 100;
                float x = screenWidth / (playerCount + 1) * (i + 1);
                float y = terrainCountor[(int)x];
                players[i].Position = new Vector2(x, y);
            }
        }
    }
}
