using GameLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Graphics;
using OpenTK;

namespace UnitTests
{
    /// <summary>
    /// Проверка корректности выполнения метода IncreaseFuel у игрока
    /// </summary>
    [TestClass]
    public class FuelTest
    {
        /// <summary>
        /// Проверка корректности выполнения увеличения запасов топлива у игрока при значении ниже 700
        /// (создаётся окно GameWindow для загрузки необходимых для выполнения OpenGL функций)
        /// </summary>
        [TestMethod]
        public void IncreaseFuelTestMethod1()
        {
            // Arrange
            GameWindow window = new GameWindow(1, 1, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 3, 0, GraphicsContextFlags.Default);
            window.Visible = false;
            BattleGame game = new BattleGame();
            game.LoadGLControl();
            Balloon balloon = new Balloon(Vector2.Zero, null);
            balloon.Fuel = 200;
            int expectedFuel = 550;
            int actualFuel;

            // Act
            balloon.IncreaseFuel();
            actualFuel = balloon.Fuel;

            // Assert
            Assert.AreEqual(expectedFuel, actualFuel);
        }

        /// <summary>
        /// Проверка корректности выполнения увеличения запасов топлива у игрока при значении выше 700
        /// (создаётся окно GameWindow для загрузки необходимых для выполнения OpenGL функций)
        /// </summary>
        [TestMethod]
        public void IncreaseFuelTestMethod2()
        {
            // Arrange
            GameWindow window = new GameWindow(1, 1, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 3, 0, GraphicsContextFlags.Default);
            window.Visible = false;
            BattleGame game = new BattleGame();
            game.LoadGLControl();
            Balloon balloon = new Balloon(Vector2.Zero, null);
            balloon.Fuel = 800;
            int expectedFuel = 1000;
            int actualFuel;

            // Act
            balloon.IncreaseFuel();
            actualFuel = balloon.Fuel;

            // Assert
            Assert.AreEqual(expectedFuel, actualFuel);
        }
    }
}
