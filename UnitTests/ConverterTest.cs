using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphicsOpenGL;

namespace UnitTests
{
    /// <summary>
    /// Класс для проверки корректности преобразования координат из OpenGL в WinForms
    /// </summary>
    [TestClass]
    public class ConverterTest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pointX">Координата точки по X</param>
        /// <param name="pointY">Координата точки по Y</param>
        /// <param name="expected">Ожидаемое значение точки</param>
        [DataTestMethod]
        [DataRow(0.2f, -0.4f, new float[] { 0.6f, 0.7f})]
        [DataRow(0.0f, 0.0f, new float[] { 0.5f, 0.5f })]
        [DataRow(-0.2f, -0.3f, new float[] { 0.4f, 0.65f })]
        [DataRow(1.0f, 0.5f, new float[] { 1.0f, 0.25f })]
        [DataRow(-0.8f, 0.4f, new float[] { 0.1f, 0.3f })]
        public void ConverterTestMethod(float pointX, float pointY, float[] expected)
        {
            // Arrange
            float[] actual;

            // Act
            actual = CoordinatesConverter.Convert(pointX, pointY);

            // Assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
        }
    }
}
