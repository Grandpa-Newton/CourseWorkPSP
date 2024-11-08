namespace AmmoLibrary
{
    /// <summary>
    /// Декоратор для увеличения скорости полёта снаряда
    /// </summary>
    public class SpeedDecorator : AmmoDecorator
    {
        /// <summary>
        /// Конструктор для создания декоратора на увеличение скорости полёта снаряда
        /// </summary>
        /// <param name="ammo">Улучшаемый снаряд</param>
        public SpeedDecorator(Ammo ammo) : base(ammo)
        {
            this.Sprite = ammo.Sprite;
            this.Distance = ammo.Distance;
            this.Radius = ammo.Radius;
            this.Speed = ammo.Speed * 1.3f;
        }
    }
}
