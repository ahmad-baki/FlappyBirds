using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlappyBirds
{
    class Pipe : GameObject
    {
        int movementSpeed = 10;
        // just 0.325f is the width/height ration in the texture
        static Point size = new Point((int)(300 * 0.325f), 300);
        Pipe upperPipe;
        bool isPassedByPlayer = false;
        public static Point Size
        {
            get => size;
        }


        public Pipe(Point position, Texture2D texture, Pipe upperPipe = null) : base(position)
        {
            this.upperPipe = upperPipe;
            hitbox = new Rectangle(position, size);
            this.texture = texture;
        }

        public override void Draw(SpriteBatch sb)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Rectangle destinationrectangle = new Rectangle(position, size);
            Vector2 origin = new Vector2(0, 0);
            if (upperPipe == null)
            {
                sb.Draw(texture, destinationrectangle, sourceRectangle, Color.White, 0, origin, SpriteEffects.FlipVertically, 1);
            }
            else
            {
                sb.Draw(texture, destinationrectangle, sourceRectangle, Color.White, 0, origin, SpriteEffects.None, 1);
            }
        }

        public override void Update(GameTime gm)
        {
            position.X -= movementSpeed;
            hitbox = new Rectangle(position, size);

            // when the players passes by, the score is incremented
            GameObject player = Game1.getObjectOfClass(typeof(Player));
            if (player != null){
                if (!isPassedByPlayer && upperPipe != null && position.X + size.X < player.Position.X)
                {
                    isPassedByPlayer = true;
                    Game1.IncrementScore();
                }
            }
            if (position.X + size.X < 0)
            {
                Console.WriteLine("Destroyed Pipe");
                Destroy();
            }
        }
    }
}
