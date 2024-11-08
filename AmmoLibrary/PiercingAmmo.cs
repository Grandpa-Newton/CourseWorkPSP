using GraphicsOpenGL;
using OpenTK;

namespace AmmoLibrary
{
    /// <summary>
    /// Бронебойный снаряд, отличающийся высокой дальностью полёта, средней скоростью и радиусом действия
    /// </summary>
    public class PiercingAmmo : Ammo
    {
        /// <summary>
        /// Конструктор для создания объекта
        /// </summary>
        public PiercingAmmo() : base()
        {
            this.Sprite = TextureLoader.LoadTexure("piercingAmmo.png");
            this.Distance = 1.8f;
            this.Radius = 0.1f;
            this.Speed = new Vector2(0.012f, 0.0f);
        }

        /// <summary>
        /// Конструктор для копирования объекта уже существующего снаряда
        /// </summary>
        /// <param name="clone">Копируемый объект снаряда</param>
        public PiercingAmmo(Ammo clone) : base(clone) // для копирования объектов, а не ссылок
        {
        }
    }
}
