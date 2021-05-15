using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBirds
{
    public abstract class GameObject
    {
        protected Texture2D texture;
        protected Point position;
        protected Rectangle hitbox;

        public Point Position
        {
            get => position;
            set => position = value;
        }
        public Rectangle Hitbox
        {
            get => hitbox;
        }

        float rotation { get; set; }

        //protected GameObject() { }
        protected GameObject(int x, int y) => this.position = new Point(x, y);
        protected GameObject(Point position) => this.position = position;

        protected void Destroy()
        {
            Game1.RemoveGameObject(this);
        }
        
        protected List<GameObject> GetCollisions()
        {
            return Game1.GetCollisions(this);
        }
        protected bool DoesCollide()
        {
            return Game1.DoesCollide(this);
        }
        public abstract void Update(GameTime gm);
        public abstract void Draw(SpriteBatch sb);
        
    }
}
