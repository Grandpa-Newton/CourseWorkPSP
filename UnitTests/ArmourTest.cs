using GameLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Graphics;
using OpenTK;

namespace UnitTests
{
    /// <summary>
    /// Тесты для проверки добавления пунктов брони
    /// </summary>
    [TestClass]
    public class ArmourTest
    {
        /// <summary>
        /// Тест для проверки корректности выполнения добавления пунктов брони
        /// </summary>
        [TestMethod]
        public void GetArmourTestMethod()
        {
            GameWindow window = new GameWindow(1, 1, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 3, 0, GraphicsContextFlags.Default);
            window.Visible = false;
            BattleGame game = new BattleGame();
            game.LoadGLControl();
            Balloon balloon = new Balloon(Vector2.Zero, null);
            int expectedArmour = 20;
            int actualArmour;

            // Act
            balloon.IncreaseArmour();
            actualArmour = balloon.Armour;

            // Assert
            Assert.AreEqual(expectedArmour, actualArmour);
        }
    }
}
