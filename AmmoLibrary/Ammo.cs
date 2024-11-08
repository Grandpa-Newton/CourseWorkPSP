using GraphicsOpenGL;
using OpenTK;
using System.Drawing;

namespace AmmoLibrary
{
    /// <summary>
    /// Абстрактный класс выпускаемого снаряда
    /// </summary>
    public abstract class Ammo
    {
        /// <summary>
        /// Текстура снаряда
        /// </summary>
        public Texture Sprite;

        /// <summary>
        /// Центр позиции снаряда
        /// </summary>
        public Vector2 PositionCenter;

        /// <summary>
        /// Показатель, отвечающий за направление полёта пули (true - влево, false - вправо)
        /// </summary>
        public bool IsLeft;

        /// <summary>
        /// Скорость полёта пули
        /// </summary>
        public Vector2 Speed { get; set; }

        /// <summary>
        /// Дистанция полёта пули
        /// </summary>
        public float Distance { get; set; }

        /// <summary>
        /// Радиус действия взрыва снаряда
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        /// Позиция объекта
        /// </summary>
        public Vector2[] Position { get; set; }

        /// <summary>
        /// Создание объекта приза с помощью копирования существующего объекта
        /// </summary>
        /// <param name="clone">Копируемый объект</param>
        public Ammo(Ammo clone)
        {
            this.Sprite = clone.Sprite;
            PositionCenter = clone.PositionCenter;
            this.IsLeft = clone.IsLeft;
            Speed = clone.Speed;
            Distance = clone.Distance;
            Radius = clone.Radius;
            Position = clone.Position;
        }

        /// <summary>
        /// Конструктор для создания объекта
        /// </summary>
        public Ammo()
        {

        }

        /// <summary>
        /// Отображение снаряда на экран
        /// </summary>
        public void Draw()
        {
            UpdatePosition(false);
            if (IsLeft)
            {
                ObjectDrawer.Draw(Sprite, Position, false);
            }
            else
                ObjectDrawer.Draw(Sprite, Position, true);
        }

        /// <summary>
        /// Получение границ объекта снаряда
        /// </summary>
        /// <param name="isExploding">Показатель, отвечающий за то, взрывается снаряд в данный момент или нет (true - взрывается, false - не взрывается)</param>
        /// <returns>Коллайдер объекта снаряда</returns>
        public RectangleF GetCollider(bool isExploding = false)
        {
            UpdatePosition(isExploding);

            Vector2[] colliderPosition = Position;

            float colliderWidth = (colliderPosition[2].X - colliderPosition[3].X) / 2.0f;
            float colliderHeight = (colliderPosition[3].Y - colliderPosition[0].Y) / 2.0f;

            float[] convertedLeftTop = CoordinatesConverter.Convert(colliderPosition[3].X, colliderPosition[3].Y);

            RectangleF collider = new RectangleF(convertedLeftTop[0], convertedLeftTop[1], colliderWidth, colliderHeight);

            return collider;
        }

        /// <summary>
        /// Генерация позиции и направления полёта снаряда
        /// </summary>
        /// <param name="position">Позиция снаряда</param>
        /// <param name="isLeft">Показатель, отвечающий за направление полёта пули (true - влево, false - вправо)</param>

        public void Spawn(Vector2 position, bool isLeft)
        {
            this.PositionCenter = position;
            this.IsLeft = isLeft;
        }

        /// <summary>
        /// Обновление состояния снаряда
        /// </summary>
        public void Update()
        {
            if (IsLeft) // для проверки, в какую сторону летит снаряд
                PositionCenter -= Speed;
            else
                PositionCenter += Speed;

            Distance -= Speed.X;
        }

        /// <summary>
        /// Обновление текущей позиции снаряда
        /// </summary>
        /// <param name="isExploding">Показатель, отвечающий за то, взрывается снаряд в данный момент или нет (true - взрывается, false - не взрывается)</param>
        public virtual void UpdatePosition(bool isExploding)
        {
            float spriteWidth = 0.03f;
            float spriteHeight = 0.0125f;
            Position = new Vector2[4]
            {
                PositionCenter + new Vector2(-spriteWidth, -spriteHeight),
                PositionCenter + new Vector2(spriteWidth, -spriteHeight),
                PositionCenter + new Vector2(spriteWidth, spriteHeight),
                PositionCenter + new Vector2(-spriteWidth, spriteHeight),
            };

            if (isExploding)
            {
                Position[0] += new Vector2(-Radius, -Radius);
                Position[1] += new Vector2(Radius, -Radius);
                Position[2] += new Vector2(Radius, Radius);
                Position[3] += new Vector2(-Radius, Radius);
            }
        }
    }
}
