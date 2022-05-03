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
		private double _verticalAcceleration = Gravity;
		private const double JumpAcceleration = 30;
		private const double MaximumVerticalVelocity = 91;
		private const double MaximumSideVelocity = 10;
		private const double SideVelocityInc = 1.4;
		private const double SideVelocityDec = 2.6;

		private bool _standing;
		private Platform _currentCollider;
		
		private Direction _direction;

		private enum Direction
		{
			Left,
			None,
			Right
		}

		public Box()
		{// todo: if about to hit platform, change velocity accordingly 
			X = 100;
			Y = 100;
			_velocity = new Vector(0, 0);
			_currentCollider = null;
		}

		public void UpdatePosition()
		{
			
			X += (int)_velocity.X;
			Y += (int)_velocity.Y;

			if (_standing && OverVoid())
					Fall();

			// dev only
			if (Y > 800)
				Y = -50;

			HandleSideMovement();

			HandleVerticalMovement();
		}

		private void HandleVerticalMovement()
		{
			_velocity.Y = Math.Min(_velocity.Y + _verticalAcceleration, MaximumVerticalVelocity);
		}

		private void HandleSideMovement()
		{
			if (_direction == Direction.None)
			{
				if (_velocity.X > 1e-4)
					_velocity.X = Math.Max(0, _velocity.X - SideVelocityDec);

				if (_velocity.X < -1e-4)
					_velocity.X = Math.Min(0, _velocity.X + SideVelocityDec);
			}

			if (_direction == Direction.Left)
			{
				_velocity.X = Math.Max(-MaximumSideVelocity, _velocity.X - SideVelocityInc);
			}

			if (_direction == Direction.Right)
			{
				_velocity.X = Math.Min(MaximumSideVelocity, _velocity.X + SideVelocityInc);
			}
		}

		private void Fall()
		{
			_standing = false;
			_currentCollider = null;
			_verticalAcceleration = Gravity;
		}

		private bool OverVoid()
		{
			return X > _currentCollider.X + Platform.Width || X - Width < _currentCollider.X;
		}

		public void Jump()
		{
			_velocity.Y = -JumpAcceleration;
			_verticalAcceleration = Gravity;
		}

		public void MoveLeft()
		{
			_direction = Direction.Left;
		}
		
		public void MoveRight()
		{
			_direction = Direction.Right;
		}

		public void StopMovingLeft()
		{
			_direction = Direction.None;
		}
		
		public void StopMovingRight()
		{
			StopMovingLeft();
		}

		public void StopFalling(Platform p)
		{
			if (_velocity.Y >= 0)
			{
				_verticalAcceleration = 0;
				_velocity.Y = 0;

				Y = p.Y - Height;

				_standing = true;
				_currentCollider = p;
			}
		}
	}
}
