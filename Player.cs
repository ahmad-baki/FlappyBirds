using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public class Player : GameObjects
{
	public Player(int x, int y)
	{
		position = new Vector2D(x, y);
	}
}
