using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LeftOnRead
{
	/// <summary>
	/// game logic
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly DispatcherTimer _timer = new DispatcherTimer();
		private Box _player;
		private readonly Platform[] _platforms;

		public MainWindow()
		{
			InitializeComponent();
			_timer.Tick += MainEventTimer;
			_timer.Interval = TimeSpan.FromMilliseconds(20);
			_platforms = GetPlatforms()
				.Select(e => new Platform((int) Canvas.GetLeft(e), (int) Canvas.GetTop(e)))
				.ToArray();
			
			StartGame();
		}


		private void MainEventTimer(object sender, EventArgs args) {
			Canvas.SetTop(BoxElement, _player.Y);
			Canvas.SetLeft(BoxElement, _player.X);
			_player.UpdatePosition();

			foreach (var p in _platforms)
			{
				if (p.IsColliding(_player))
				{
					_player.StopFalling(p);
				}
			}
		}

		private void StartGame()
		{
			MainCanvas.Focus();

			_player = new Box();

			Canvas.SetTop(BoxElement, _player.Y);
			Canvas.SetLeft(BoxElement, _player.X);

			_timer.Start();
		}

		private IEnumerable<Border> GetPlatforms()
		{
			return MainCanvas.Children.OfType<Border>()
				.Where(x => ((string) x.Tag).StartsWith("Platform"));
		}

		private void KeyIsUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.A)
				_player.StopMovingLeft();
			
			if (e.Key == Key.D)
				_player.StopMovingRight();
		}

		private void KeyIsDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.A)
				_player.MoveLeft();
			
			if (e.Key == Key.D)
				_player.MoveRight();
			
			if (e.Key == Key.Space)
				_player.Jump();
		}
	}
}
