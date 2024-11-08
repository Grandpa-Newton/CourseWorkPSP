namespace GraphicsOpenGL
{
    /// <summary>
    /// Класс, предназначенный для конвертации из системы координат OpenGL в WinForms
    /// </summary>
    public static class CoordinatesConverter
    {
        /// <summary>
        /// Метод конвертации точки из системы координат OpenGL в WinForms
        /// </summary>
        /// <param name="pointX">Координата X заданной точки в системе координат OpenGL</param>
        /// <param name="pointY">Координата Y заданной точки в системе координат OpenGL</param>
        /// <returns>Точка в системе координат WInForms</returns>
        public static float[] Convert(float pointX, float pointY)
        {
            decimal centralPointX = 0.5M;
            decimal centralPointY = 0.5M; 

            float[] resultPoint = new float[2];

            resultPoint[0] = (float)(centralPointX + (decimal)(pointX / 2.0f));
            resultPoint[1] = (float)(centralPointY - (decimal)(pointY / 2.0f));
            
            return resultPoint;
        }
    }
}
