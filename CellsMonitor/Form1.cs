using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameEngine;

namespace CellsMonitor
{
	public partial class Form1 : Form
	{
		private Engine _engine;
		private int _resolution;
		private int _density;
		private int _rows;
		private int _cols;
		private Graphics _graphics;

		public Form1()
		{
			InitializeComponent();
		}

		private void numericTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (char.IsDigit(e.KeyChar) || (int)e.KeyChar == 8)
			{
				return;
			}
			else
			{
				e.Handled = true;
			}
		}

		private void startButton_Click(object sender, EventArgs e)
		{
			pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
			_graphics = Graphics.FromImage(pictureBox.Image);

			int.TryParse(resolutionTextBox.Text, out _resolution);
			int.TryParse(densityTextBox.Text, out _density);
			_rows = pictureBox.Width / _resolution;
			_cols = pictureBox.Height / _resolution;

			_engine = new Engine(_rows, _cols, _density);

			timer1.Start();
			stopButton.Enabled = true;
			startButton.Enabled = false;
		}

		private void DrawGameField(bool[,] gameField)
		{
			_graphics.Clear(Color.Black);
			for (int i = 0; i < _rows; i++)
			{
				for (int j = 0; j < _cols; j++)
				{
					if (gameField[i, j])
					{
						_graphics.FillRectangle(Brushes.Red, i * _resolution, j * _resolution, _resolution - 1, _resolution - 1);
					}
				}
			}
			pictureBox.Refresh();
		}

		private void stopButton_Click(object sender, EventArgs e)
		{
			timer1.Stop();
			stopButton.Enabled = false;
			startButton.Enabled = true;
		}

		private async void timer1_Tick(object sender, EventArgs e)
		{
			await Task.Run(() =>
			{
				_engine.NextGeneration();
				DrawGameField(_engine.GameField);
			});			
		}
	}
}
