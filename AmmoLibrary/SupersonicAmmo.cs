using GraphicsOpenGL;
using OpenTK;

namespace AmmoLibrary
{
    /// <summary>
    /// Сверхзвуковой снаряд, отличается высокой скоростью, низким радиусом и дистанцией
    /// </summary>
    public class SupersonicAmmo : Ammo // сверхзвуковой
    {
        /// <summary>
        /// Конструктор для создания объекта сверхзвукового снаряда
        /// </summary>
        public SupersonicAmmo() : base()
        {
            this.Sprite = TextureLoader.LoadTexure("supersonicAmmo.png");
            this.Distance = 1.0f;
            this.Radius = 0.05f;
            this.Speed = new Vector2(0.0145f, 0.0f);
        }

        /// <summary>
        /// Конструктор для копирования объекта уже существующего снаряда
        /// </summary>
        /// <param name="clone">Копируемый объект снаряда</param>
        public SupersonicAmmo(Ammo clone) : base(clone) // для копирования объектов, а не ссылок
        {
        }
    }
}
