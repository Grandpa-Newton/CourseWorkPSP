using GameLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Graphics;
using OpenTK;

namespace UnitTests
{
    /// <summary>
    /// Проверка на корректность выполнения метода CheckAlive у игрока
    /// </summary>
    [TestClass]
    public class CheckAliveTest
    {
        /// <summary>
        /// Метод для проверки корректности выполнения CheckAlive при значении здоровья равным нулю 
        /// (создаётся окно GameWindow для загрузки необходимых для выполнения OpenGL функций)
        /// </summary>
        [TestMethod]
        public void CheckAliveTestMethod1()
        {
            // Arrange
            GameWindow window = new GameWindow(1, 1, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 3, 0, GraphicsContextFlags.Default);
            window.Visible = false;
            BattleGame game = new BattleGame();
            game.LoadGLControl();
            Balloon balloon = new Balloon(Vector2.Zero, null);
            balloon.Health = 0;
            bool expectedResult = false;
            bool actualResult;

            // Act
            actualResult = balloon.CheckAlive();

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Метод для проверки корректности выполнения CheckAlive при значении здоровья больше нуля 
        /// (создаётся окно GameWindow для загрузки необходимых для выполнения OpenGL функций)
        /// </summary>
        [TestMethod]
        public void IncreaseHealthTestMethod2()
        {
            // Arrange
            GameWindow window = new GameWindow(1, 1, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 3, 0, GraphicsContextFlags.Default);
            window.Visible = false;
            BattleGame game = new BattleGame();
            game.LoadGLControl();
            Balloon balloon = new Balloon(Vector2.Zero, null);
            balloon.Health = 2;
            bool expectedResult = true;
            bool actualResult;

            // Act
            actualResult = balloon.CheckAlive();

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}

