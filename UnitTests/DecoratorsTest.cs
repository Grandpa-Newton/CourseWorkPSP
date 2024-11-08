using GameLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Graphics;
using OpenTK;
using System;
using AmmoLibrary;

namespace UnitTests
{
    /// <summary>
    /// Класс для проверки корректности функционирования декораторов
    /// </summary>
    [TestClass]
    public class DecoratorsTest
    {
        /// <summary>
        /// Проверка корректности выполнения декоратора на увеличение дистанции полёта снаряда
        /// (создаётся окно GameWindow для загрузки необходимых для выполнения OpenGL функций)
        /// </summary>
        [TestMethod]
        public void DistanceDecoratorTestMethod()
        {
            // Arrange
            GameWindow window = new GameWindow(1, 1, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 3, 0, GraphicsContextFlags.Default);
            window.Visible = false;
            BattleGame game = new BattleGame();
            Ammo ammo;
            game.LoadGLControl();
            ammo = new ExplosiveAmmo();
            float actualDistance;
            float expectedDistance=1.5f;

            // Act
            ammo = new DistanceDecorator(ammo);
            actualDistance = ammo.Distance;

            // Assert
            Assert.AreEqual(actualDistance, expectedDistance);
        }

        /// <summary>
        /// Проверка корректности выполнения декоратора на увеличение радиуса действия снаряда
        /// (создаётся окно GameWindow для загрузки необходимых для выполнения OpenGL функций)
        /// </summary>
        [TestMethod]
        public void RadiusDecoratorTestMethod()
        {
            // Arrange
            GameWindow window = new GameWindow(1, 1, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 3, 0, GraphicsContextFlags.Default);
            window.Visible = false;
            BattleGame game = new BattleGame();
            Ammo ammo;
            game.LoadGLControl();
            ammo = new SupersonicAmmo();
            float actualRadius;
            float expectedRadius = 0.06f;

            // Act
            ammo = new RadiusDecorator(ammo);
            actualRadius = (float)Math.Round(ammo.Radius, 2); // округление из-за неточности float

            // Assert
            Assert.AreEqual(actualRadius, expectedRadius);
        }

        /// <summary>
        /// Проверка корректности выполнения декоратора на увеличение скорости полёта снаряда
        /// (создаётся окно GameWindow для загрузки необходимых для выполнения OpenGL функций)
        /// </summary>
        [TestMethod]
        public void SpeedDecoratorTestMethod()
        {
            // Arrange
            GameWindow window = new GameWindow(1, 1, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 3, 0, GraphicsContextFlags.Default);
            window.Visible = false;
            BattleGame game = new BattleGame();
            Ammo ammo;
            game.LoadGLControl();
            ammo = new PiercingAmmo(); 
            float actualSpeed;
            float expectedSpeed = 0.0156f;

            // Act
            ammo = new SpeedDecorator(ammo);
            actualSpeed = ammo.Speed.X;

            // Assert
            Assert.AreEqual(actualSpeed, expectedSpeed);
        }
    }
}
