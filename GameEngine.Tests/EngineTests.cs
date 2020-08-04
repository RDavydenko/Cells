using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngine.Tests
{
	[TestClass]
	public class EngineTests
	{
		[TestMethod]
		public void Initialize_GameField_IsNotNull()
		{
			// Arrange

			// Act
			Engine gameEngine = new Engine(100, 100, 1);

			// Assert
			Assert.IsNotNull(gameEngine.GameField);
		}

		[TestMethod]
		public void Initialize_MaxDensity_GameFieldFullFilled()
		{
			// Arrange
			int density = 1; // Плотность (максимальное заполнение)

			// Act
			Engine gameEngine = new Engine(3, 3, density);

			// Assert
			CollectionAssert.DoesNotContain(gameEngine.GameField, false);
		}

		[TestMethod]
		public void Initialize_MinDensity_GameFieldFullFilled()
		{
			// Arrange
			int density = 0; // Плотность (минимальное заполнение)

			// Act
			Engine gameEngine = new Engine(100, 100, density);

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
			Engine gameEngine = new Engine(1, cols, 1);
		}

		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[TestMethod]
		public void Initialize_NotValidRows_ExceptionThrowed()
		{
			// Arrange
			int rows = 0;

			// Act
			Engine gameEngine = new Engine(rows, 1, 1);
		}

		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[TestMethod]
		public void Initialize_NotValidDensity_ExceptionThrowed()
		{
			// Arrange
			int density = -1;

			// Act
			Engine gameEngine = new Engine(1, 1, density);
		}

		[TestMethod]
		public void NextGeneration_NewField_NotIncludedNull()
		{
			// Arrange
			Engine gameEngine = new Engine(100, 100, 1);

			// Act
			gameEngine.NextGeneration();

			// Assert
			CollectionAssert.DoesNotContain(gameEngine.GameField, null);
		}

		[TestMethod]
		public void NextGeneration_MinDensity_NotIncludeTrue()
		{
			// Arrange
			Engine gameEngine = new Engine(100, 100, 0); // Пусто (density = 0)

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
			Engine gameEngine = new Engine(3, 3, density); // Полностью заполненный
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
