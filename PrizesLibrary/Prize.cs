using GraphicsOpenGL;
using OpenTK;
using System.Drawing;

namespace PrizesLibrary
{
    /// <summary>
    /// Класс создаваемого на сцене приза
    /// </summary>
    public abstract class Prize
    {
        /// <summary>
        /// Центр позиции приза
        /// </summary>
        protected Vector2 CenterPosition;

        /// <summary>
        /// Показатель, отвечающий за направление полёта приза (true - летит влево, false - вправо)
        /// </summary>
        protected bool IsLeft;

        /// <summary>
        /// Текстура приза
        /// </summary>
        protected Texture Sprite;

        /// <summary>
        /// Получение скорости полёта приза
        /// </summary>
        /// <returns>Скорость полёта приза</returns>
        protected abstract Vector2 GetSpeed();

        /// <summary>
        /// Отрисовка приза
        /// </summary>
        /// <param name="isFlipped">Показатель, отвечающий за то, следует отразить по горизонтали объект или нет 
        /// (true - следует отразить объект, false - не следует)\
        /// </param>
        public void Draw(bool isFlipped)
        {
            ObjectDrawer.Draw(Sprite, GetPosition(), isFlipped);
        }
        /// <summary>
        /// Получение границ объекта приза
        /// </summary>
        /// <returns>Коллайдер приза</returns>
        public RectangleF GetCollider()
        {
            Vector2[] colliderPosition = GetPosition();


            float colliderWidth = (colliderPosition[2].X - colliderPosition[3].X) / 2.0f;
            float colliderHeight = (colliderPosition[3].Y - colliderPosition[0].Y) / 2.0f;

            float[] convertedLeftTop = CoordinatesConverter.Convert(colliderPosition[3].X, colliderPosition[3].Y);

            RectangleF collider = new RectangleF(convertedLeftTop[0], convertedLeftTop[1], colliderWidth, colliderHeight);

            return collider;
        }

        /// <summary>
        /// Получение позиции приза
        /// </summary>
        /// <returns>Массив из четырёх крайних точек объекта</returns>
        protected Vector2[] GetPosition()
        {
            return new Vector2[4]
            {
                CenterPosition + new Vector2(-0.06f, -0.06f),
                CenterPosition + new Vector2(0.06f, -0.06f),
                CenterPosition + new Vector2(0.06f, 0.06f),
                CenterPosition + new Vector2(-0.06f, 0.06f),
            };
        }

        /// <summary>
        /// Обновление позиции приза
        /// </summary>
        public void Update()
        {
            if (IsLeft)
                CenterPosition -= GetSpeed();
            else
                CenterPosition += GetSpeed();
        }

        /// <summary>
        /// Конструктор приза при его генерации на экран
        /// </summary>
        /// <param name="centerPosition">Начальная позиция приза</param>
        /// <param name="isLeft">Направление полёта приза (true - влево, false - вправо)</param>
        public Prize(Vector2 centerPosition, bool isLeft)
        {
            this.CenterPosition = centerPosition;
            this.IsLeft = isLeft;
        }

    }
}
