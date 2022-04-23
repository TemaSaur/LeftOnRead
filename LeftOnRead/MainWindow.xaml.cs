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
		DispatcherTimer timer = new DispatcherTimer();
		int gravity = 9;
		Rect box;

		public MainWindow()
		{
			InitializeComponent();
			timer.Tick += MainEventTimer;
			timer.Interval = TimeSpan.FromMilliseconds(20);

			StartGame();
		}


		private void MainEventTimer(object sender, EventArgs args) {
			box = new Rect(Canvas.GetLeft(HAHAHA), Canvas.GetTop(HAHAHA), HAHAHA.Width, HAHAHA.Height);

			Canvas.SetTop(HAHAHA, Canvas.GetTop(HAHAHA) + gravity);
		}

		private void StartGame()
		{
			MainCanvas.Focus();

			Canvas.SetTop(HAHAHA, 100);

			foreach (var x in MainCanvas.Children.OfType<Border>())
			{
				if ((string)x.Tag == "Platform")
				{
					Canvas.SetLeft(x, 62);
				}
			}

			timer.Start();
		}

		private void KeyIsUp(object sender, KeyEventArgs e)
		{
			HAHAHA.RenderTransform = new RotateTransform(5, HAHAHA.Width / 2, HAHAHA.Height / 2);
			gravity = 9;
		}

		private void KeyIsDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Space)
			{
				HAHAHA.RenderTransform = new RotateTransform(-20, HAHAHA.Width/2, HAHAHA.Height/2);
				gravity = -9;
			}
		}
	}
}
