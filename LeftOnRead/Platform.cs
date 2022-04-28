using System;

namespace LeftOnRead
{
	public class Platform
	{
		public int X { get; private set; }
		public int Y { get; private set; }
		public static int Height = 25;
		public static int Width = 120;

		public Platform(int x, int y)
		{
			X = x;
			Y = y;
		}

		public bool IsColliding(Box box)
		{
			if (box.X + box.Width < X || box.X > X + Width)
				return false;
			return box.Y + box.Height >= Y && box.Y < Y + Height;
		}
	}
}