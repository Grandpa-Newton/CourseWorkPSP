using GraphicsOpenGL;
using OpenTK;

namespace AmmoLibrary
{
    /// <summary>
    /// Фугасный снаряд, отличается высоким радиусом действия, средней дистанцией и низкой скоростью
    /// </summary>
    public class ExplosiveAmmo : Ammo
    {
        /// <summary>
        /// Конструктор для создания объекта фугасного снаряда
        /// </summary>
        public ExplosiveAmmo() : base()
        {
            this.Sprite = TextureLoader.LoadTexure("explosiveAmmo_2.png");
            this.Distance = 1.2f;
            this.Radius = 0.2f;
            this.Speed = new Vector2(0.009f, 0.0f);
        }

        /// <summary>
        /// Конструктор для копирования объекта уже существующего снаряда
        /// </summary>
        /// <param name="clone">Копируемый объект снаряда</param>
        public ExplosiveAmmo(Ammo clone) : base(clone) 
        {
        }
        
    }
}
