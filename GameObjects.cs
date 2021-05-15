using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public abstract class Class1
{
	Point position { get; set; };

	public abstract void Update(GameTime gameTime);
	public abstract void Draw(SpriteBatch sb);
}
