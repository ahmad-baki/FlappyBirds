using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlappyBirds
{
    class Player : GameObject
    {
        float movementSpeed = 650;
        float gravity = 50;

        float acceleration = 0;
        KeyboardState oldState;
        float scale = 0.2f;

        // Constructes
        public Player(Point position, Texture2D texture) : base(position){
            this.texture = texture;
            hitbox = new Rectangle(position, new Point((int)(texture.Width * scale), (int)(texture.Height * scale)));
            Console.WriteLine("created hitbox");
        }

        public override void Update(GameTime gm)
        {
            move(gm);
            hitbox = new Rectangle(position, new Point((int)(texture.Width * scale), (int)(texture.Height * scale)));

            // when it collides with anything
            if (DoesCollide())
            {
                Game1.GameOver();
            }

        }

        public override void Draw(SpriteBatch sb)
        {
            // becouse "up" is positive and "down" is negative
            int direction = -Math.Sign(acceleration);

            // what of the texture should be used
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);

            // on which point should object be rotated
            Vector2 origin = new Vector2(texture.Width/2, texture.Height/2);
            float degree = (float)direction * 30.0f * (float)Math.PI / 180.0f;
            sb.Draw(texture, new Vector2(position.X, position.Y), sourceRectangle, Color.White, degree, origin, scale, SpriteEffects.None, 1);
        }

        private void move(GameTime gm)
        {
            if (Position.Y > Game1.GetResolution().Y)
            {
                Console.WriteLine("Destroyed");
                Destroy();
                Console.WriteLine("After destroyed");
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Space) && !oldState.IsKeyDown(Keys.Space))
            {
                acceleration = -movementSpeed * (float)gm.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                acceleration += gravity * (float)gm.ElapsedGameTime.TotalSeconds;
            }
            oldState = Keyboard.GetState();
            position.Y += (int)acceleration;
        }
    }
}
