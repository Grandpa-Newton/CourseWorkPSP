namespace GraphicsOpenGL
{
    /// <summary>
    /// Класс текстуры
    /// </summary>
    public class Texture
    {
        /// <summary>
        /// Идентификатор, генерируемый OpenGL
        /// </summary>
        public int Id;

        /// <summary>
        /// Ширина текстуры
        /// </summary>
        public int Width;

        /// <summary>
        /// Высота текстуры
        /// </summary>
        public int Height;

        /// <summary>
        /// Конструктор создания объекта текстуры
        /// </summary>
        /// <param name="id">Идентификатор текстуры</param>
        /// <param name="width">Ширина текстуры</param>
        /// <param name="height">Высота текстуры</param>
        public Texture(int id, int width, int height)
        {
            Id = id;
            Width = width;
            Height = height;
        }
    }
}
