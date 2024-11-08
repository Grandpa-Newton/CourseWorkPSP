using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace GraphicsOpenGL
{
    /// <summary>
    /// Класс для отрисовки объектов на экран
    /// </summary>
    public class ObjectDrawer
    {
        /// <summary>
        /// Отрисовка объектов на экран
        /// </summary>
        /// <param name="texture">Текстура изображаемого объекта</param>
        /// <param name="position">Позиция для отрисовки объекта</param>
        /// <param name="isFlipped">Показатель, отвечающий за то, нужно ли отражать по вертикали объект
        /// (true - объект следует отражать, false - не следует)</param>
        public static void Draw(Texture texture, Vector2[] position, bool isFlipped)
        {
            Start();

            Vector2[] vertices;

            if (isFlipped)
            {
                vertices = new Vector2[4] // вершины спрайта
                {
                    new Vector2(1.0f,1.0f), // правый низ
                    new Vector2(0.0f,1.0f), // правый верх
                    new Vector2(0.0f,0.0f), // левый верх
                    new Vector2(1.0f,0.0f), // левый низ
                };
            }
            else
            {
                vertices = new Vector2[4] // вершины спрайта
                {
                    new Vector2(0.0f,1.0f), // левый низ
                    new Vector2(1.0f,1.0f), // правый низ
                    new Vector2(1.0f,0.0f), // правый верх
                    new Vector2(0.0f,0.0f), // левый верх
                };
            }

            GL.Enable(EnableCap.Texture2D);

            GL.BindTexture(TextureTarget.Texture2D, texture.Id);

            GL.Begin(PrimitiveType.Quads);

            for (int i = 0; i < 4; i++)
            {
                GL.TexCoord2(vertices[i]);

                GL.Vertex2(position[i]);
            }

            GL.End();
        }

        /// <summary>
        /// Подготовка OpenGL к отрисовке объектов
        /// </summary>
        private static void Start()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, -1.0, 1.0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }
    }
}
