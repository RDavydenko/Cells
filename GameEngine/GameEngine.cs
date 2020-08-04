using System;

namespace GameEngine
{
	public class GameEngine
	{
		private readonly int _rows;
		private readonly int _cols;
		public bool[,] GameField { get; private set; }

		public GameEngine(int rows, int cols, int density)
		{
			if (rows < 1 || cols < 1)
			{
				throw new ArgumentOutOfRangeException("Минимальное значение для параматров rows, cols 1 (единица)");
			}
			if (density < 0)
			{
				throw new ArgumentOutOfRangeException("Минимальное значение для density 0 (ноль)");
			}
			Initialize(rows, cols, density);

			_rows = GameField.GetLength(0);
			_cols = GameField.GetLength(1);
		}

		private void Initialize(int rows, int cols, int density)
		{
			Random r = new Random();
			GameField = new bool[rows, cols];

			if (density == 0)
			{
				GameField.Initialize();
				for (int i = 0; i < rows; i++)
				{
					for (int j = 0; j < cols; j++)
					{
						GameField[i, j] = false;
					}
				}
				return;
			}

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					GameField[i, j] = r.Next(density) == 0;
				}
			}
		}

		private int CountNeighbors(int x, int y)
		{
			int count = 0;
			for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j <= 1; j++)
				{
					if (i == 0 && j == 0)
					{
						continue;
					}

					try
					{
						if (GameField[x + i, y + j])
						{
							count++;
						}
					}
					catch (IndexOutOfRangeException) { }
				}
			}
			return count;
		}

		private bool HasLife(int x, int y)
		{
			return GameField[x, y];
		}		

		public void NextGeneration()
		{
			bool[,] newGameField = new bool[_rows, _cols];
			for (int i = 0; i < _rows; i++)
			{
				for (int j = 0; j < _cols; j++)
				{
					if (!HasLife(i, j) && CountNeighbors(i, j) == 3)
					{
						newGameField[i, j] = true;
					}
					else if (HasLife(i, j) && CountNeighbors(i, j) != 3)
					{
						newGameField[i, j] = false;
					}
					else
					{
						newGameField[i, j] = HasLife(i, j);
					}
				}
			}
			GameField = newGameField;
		}
	}
}
