using GameLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK;
using OpenTK.Graphics;

namespace UnitTests
{
    /// <summary>
    /// Класс для проверки корректности выполнения метода GetDamage у игрока
    /// </summary>
    [TestClass]
    public class DamageTest
    {
        /// <summary>
        /// Проверка корректности выполнения метода GetDamage без наличия брони у игрока
        /// (создаётся окно GameWindow для загрузки необходимых для выполнения OpenGL функций)
        /// </summary>
        [TestMethod]
        public void GetDamageTestMethod()
        {
            // Arrange
            GameWindow window = new GameWindow(1, 1, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 3, 0, GraphicsContextFlags.Default);
            window.Visible = false;
            BattleGame game = new BattleGame();
            game.LoadGLControl();
            Balloon balloon = new Balloon(Vector2.Zero, null);
            int expectedHealth = 85;
            int actualHealth;

            // Act
            balloon.GetDamage();
            actualHealth = balloon.Health;

            // Assert
            Assert.AreEqual(expectedHealth, actualHealth);
        }

        /// <summary>
        /// Проверка корректности выполнения метода GetDamage с наличием брони у игрока
        /// (создаётся окно GameWindow для загрузки необходимых для выполнения OpenGL функций)
        /// </summary>
        [TestMethod]
        public void GetDamageWithArmourTestMethod()
        {
            // Arrange
            GameWindow window = new GameWindow(1, 1, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 3, 0, GraphicsContextFlags.Default);
            window.Visible = false;
            BattleGame game = new BattleGame();
            game.LoadGLControl();
            Balloon balloon = new Balloon(Vector2.Zero, null); // отрисовка и позиция для проверки нам не требуется
            int expectedHealth = 90;
            int expectedArmour = 0;
            int actualHealth;
            int actualArmour;

            // Act
            balloon.IncreaseArmour();
            balloon.GetDamage();
            balloon.GetDamage();
            actualHealth = balloon.Health;
            actualArmour = balloon.Armour;

            // Assert
            Assert.AreEqual(expectedHealth, actualHealth);
            Assert.AreEqual(expectedArmour, actualArmour);
        }
    }
}
