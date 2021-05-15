using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FlappyBirds
{
    public class Game1 : Game
    {
        static int gapBetweenPipes = 300;

        static Texture2D pipeTexture;
        static Texture2D birdTexture;
        static SpriteFont scoreFont;
        static SpriteFont gameOverFont;
        static Random random = new Random(2);

        static float secondsUntilNextPipe;
        static List<GameObject> gameObjects { get; set; } = new List<GameObject>();
        static int score;
        static bool gameOver;

        static GraphicsDeviceManager _graphics;
        static SpriteBatch _spriteBatch;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this); 
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1000;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 750;   // set this value to the desired height of your window
            _graphics.ApplyChanges();
            
            secondsUntilNextPipe = (float)(random.NextDouble()*3);


            // TODO: Add your initialization logic here
            base.Initialize();

            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            pipeTexture = Content.Load<Texture2D>("Graphics/Pipe");
            birdTexture = Content.Load<Texture2D>("Graphics/Bird");
            scoreFont = Content.Load<SpriteFont>("Score");
            gameOverFont = Content.Load<SpriteFont>("GameOver");


            Point playerPosition = new Point((int)GetResolution().X / 2, (int)GetResolution().Y / 2);
            gameObjects.Add(new Player(playerPosition, birdTexture));
            Console.WriteLine(gameObjects[0].GetType());

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!gameOver)
            {
                // Updates every Game-Object
                foreach (GameObject gameObject in gameObjects.ToArray())
                {
                    gameObject.Update(gameTime);
                }

                if (gameTime.TotalGameTime.TotalSeconds > secondsUntilNextPipe)
                {
                    int lowerPipePosition = random.Next(gapBetweenPipes + 30, _graphics.PreferredBackBufferHeight - (gapBetweenPipes + 30));

                    Pipe upperPipe = new Pipe(new Point(_graphics.PreferredBackBufferWidth, lowerPipePosition - (Pipe.Size.Y + gapBetweenPipes)), pipeTexture); //Pipe.Size.Y
                    Pipe lowerPipe = new Pipe(new Point(_graphics.PreferredBackBufferWidth, lowerPipePosition), pipeTexture, upperPipe);
                    gameObjects.Add(upperPipe);
                    gameObjects.Add(lowerPipe);
                    secondsUntilNextPipe = (float)(gameTime.TotalGameTime.TotalSeconds + 2 + (random.NextDouble() * 3));
                    Console.WriteLine("Created Pipe");
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            // Draws every Game-Object
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(_spriteBatch);
            }

            if (gameOver)
            {
                Vector2 gameOverPosition = GetResolution() / 2;
                _spriteBatch.DrawString(gameOverFont, "Game Over", gameOverPosition, Color.Black);
            }
            else
            {
                Vector2 scorePosition = new Vector2(GetResolution().X / 2, 20);
                _spriteBatch.DrawString(gameOverFont, "Score: " + score.ToString(), scorePosition, Color.Black);

            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public static Vector2 GetResolution()
        {
            return new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        public static void RemoveGameObject(GameObject gameObject)
        {
            gameObjects.Remove(gameObject);
        }
        public static List<GameObject> GetCollisions(GameObject caller)
        {
            List<GameObject> output = new List<GameObject>();
            foreach(GameObject gameObject in gameObjects)
            {
                if(gameObject == caller) { continue; }

                if (gameObject.Hitbox.Intersects(caller.Hitbox)){
                    output.Add(caller);
                }
            }
            return output;
        }
        public static bool DoesCollide(GameObject caller)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject == caller) { continue; }

                if (gameObject.Hitbox.Intersects(caller.Hitbox))
                {
                    return true;
                }
            }
            return false;
        }

        public static GameObject getObjectOfClass(Type _object)
        {
            foreach(GameObject gameObject in gameObjects)
            {
                if(gameObject.GetType() == _object)
                {
                    return gameObject;
                }
            }
            return null;
        }

        public static List<GameObject> getAllObjectsOfClass(Type _object)
        {
            List<GameObject> output = new List<GameObject>();
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.GetType() == _object)
                {
                    output.Add(gameObject);
                }
            }
            return output;
        }

        public static void IncrementScore()
        {
            score++;
        }

        public static void GameOver()
        {
            gameObjects.Clear();
            gameOver = true;
        }
    }
}
