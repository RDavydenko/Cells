using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngine.Tests
{
	[TestClass]
	public class GameEngineTests
	{
		[TestMethod]
		public void Initialize_GameField_IsNotNull()
		{
			// Arrange

			// Act
			GameEngine gameEngine = new GameEngine(100, 100, 1);

			// Assert
			Assert.IsNotNull(gameEngine.GameField);
		}

		[TestMethod]
		public void Initialize_MaxDensity_GameFieldFullFilled()
		{
			// Arrange
			int density = 1; // Плотность (максимальное заполнение)

			// Act
			GameEngine gameEngine = new GameEngine(3, 3, density);

			// Assert
			CollectionAssert.DoesNotContain(gameEngine.GameField, false);
		}

		[TestMethod]
		public void Initialize_MinDensity_GameFieldFullFilled()
		{
			// Arrange
			int density = 0; // Плотность (минимальное заполнение)

			// Act
			GameEngine gameEngine = new GameEngine(100, 100, density);

			// Assert
			CollectionAssert.DoesNotContain(gameEngine.GameField, true);
		}

		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[TestMethod]
		public void Initialize_NotValidCols_ExceptionThrowed()
		{
			// Arrange
			int cols = 0;

			// Act
			GameEngine gameEngine = new GameEngine(1, cols, 1);
		}

		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[TestMethod]
		public void Initialize_NotValidRows_ExceptionThrowed()
		{
			// Arrange
			int rows = 0;

			// Act
			GameEngine gameEngine = new GameEngine(rows, 1, 1);
		}

		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[TestMethod]
		public void Initialize_NotValidDensity_ExceptionThrowed()
		{
			// Arrange
			int density = -1;

			// Act
			GameEngine gameEngine = new GameEngine(1, 1, density);
		}

		[TestMethod]
		public void NextGeneration_NewField_NotIncludedNull()
		{
			// Arrange
			GameEngine gameEngine = new GameEngine(100, 100, 1);

			// Act
			gameEngine.NextGeneration();

			// Assert
			CollectionAssert.DoesNotContain(gameEngine.GameField, null);
		}

		[TestMethod]
		public void NextGeneration_MinDensity_NotIncludeTrue()
		{
			// Arrange
			GameEngine gameEngine = new GameEngine(100, 100, 0); // Пусто (density = 0)

			// Act
			gameEngine.NextGeneration();

			// Assert
			CollectionAssert.DoesNotContain(gameEngine.GameField, true);
		}

		[TestMethod]
		public void NextGeneration_MaxDensity_MeetExpectations()
		{
			// Arrange
			int density = 1; // Масимальная плотность
			GameEngine gameEngine = new GameEngine(3, 3, density); // Полностью заполненный
			bool[,] expected = new bool[3,3]
			{
				{ true, false, true },
				{ false, false, false },
				{ true, false, true }
			};

			// Act
			gameEngine.NextGeneration();			

			// Assert
			CollectionAssert.AreEqual(expected, gameEngine.GameField);

			// Act 2
			gameEngine.NextGeneration();

			// Assert 2
			CollectionAssert.DoesNotContain(gameEngine.GameField, true);
		}
	}
}
