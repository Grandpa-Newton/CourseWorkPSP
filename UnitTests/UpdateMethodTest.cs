using GameLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Graphics;
using OpenTK;

namespace UnitTests
{
    /// <summary>
    /// Класс для проверки корректности выполнения метода Update у игрока 
    /// </summary>
    [TestClass]
    public class UpdateMethodTest
    {
        /// <summary>
        /// Проверка корректности выполнения уменьшения значения запасов топлива при вызове метода Update
        /// (создаётся окно GameWindow для загрузки необходимых для выполнения OpenGL функций)
        /// </summary>
        [TestMethod]
        public void UpdateTestMethod()
        {
            // Arrange
            GameWindow window = new GameWindow(1, 1, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 3, 0, GraphicsContextFlags.Default);
            window.Visible = false;
            BattleGame game = new BattleGame();
            game.LoadGLControl();
            Balloon balloon = new Balloon(Vector2.Zero, null);
            int expectedFuel = 999;
            int actualFuel;

            // Act
            balloon.Update(Vector2.Zero);
            actualFuel = balloon.Fuel;

            // Assert
            Assert.AreEqual(expectedFuel, actualFuel);
        }
    }
}
