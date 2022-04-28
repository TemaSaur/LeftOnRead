using System;
using System.Windows;

namespace LeftOnRead
{
	public class Box
	{
		public int X { get; private set; }
		public int Y { get; private set; }
		public int Height => 40;
		public int Width => 40;

		private Vector _velocity;
		private const double Gravity = 2;
		private double g = Gravity;
		private const double JumpSize = 30;
		private const double MaximumSideVelocity = 10;
		private const double SideVelocityInc = 0.6;
		private const double SideVelocityDec = 1.9;

		private bool _currentlyColliding = false;
		private Platform _currentCollider;
		
		private int _direction = 0;

		public Box()
		{
			X = 100;
			Y = 100;
			_velocity = new Vector(0, 0);
			_currentCollider = null;
		}

		public void UpdatePosition()
		{
			X += (int)_velocity.X;
			Y += (int)_velocity.Y;

			if (_currentlyColliding)
			{
				if (X > _currentCollider.X + Platform.Width || X - Width < _currentCollider.X)
				{
					_currentlyColliding = false;
					_currentCollider = null;
					g = Gravity;
				}
			}

			if (_direction == 0)
			{
				if (_velocity.X > 1e-4)
					_velocity.X = Math.Max(0, _velocity.X - SideVelocityDec);

				if (_velocity.X < -1e-4)
					_velocity.X = Math.Min(0, _velocity.X + SideVelocityDec);
			}
			else if (_direction == 1)
			{
				_velocity.X = Math.Min(MaximumSideVelocity, _velocity.X + SideVelocityInc);
			}
			else
			{
				_velocity.X = Math.Max(-MaximumSideVelocity, _velocity.X - SideVelocityInc);
			}
			_velocity.Y += g;
		}

		public void Jump()
		{
			_velocity.Y = -JumpSize;
			g = Gravity;
		}

		public void MoveLeft()
		{
			_direction = -1;
			// _velocity.X = Math.Max(-MaximumSideVelocity, _velocity.X - SideVelocityInc);
		}
		
		public void MoveRight()
		{
			_direction = 1;
			// _velocity.X = Math.Min(MaximumSideVelocity, _velocity.X + SideVelocityInc);
		}
		
		
		public void StopMovingLeft()
		{
			_direction = 0;
		}
		
		public void StopMovingRight()
		{
			_direction = 0;
		}

		public void NoMoveDown(Platform p)
		{
			if (_velocity.Y >= 0)
			{
				g = 0;
				_velocity.Y = 0;

				Y = p.Y - Height;

				_currentlyColliding = true;
				_currentCollider = p;
			}
		}
	}
}