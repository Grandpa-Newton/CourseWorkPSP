namespace AmmoLibrary
{
    /// <summary>
    /// Декоратор для увеличения дальности полёта пули
    /// </summary>
    public class DistanceDecorator : AmmoDecorator
    {
        /// <summary>
        /// Конструктор для создания декоратора на увеличение дальности полёта пули
        /// </summary>
        /// <param name="ammo">Улучшаемый снаряд</param>
        public DistanceDecorator(Ammo ammo) : base(ammo)
        {
            this.Sprite = ammo.Sprite;
            this.Distance = ammo.Distance * 1.25f;
            this.Radius = ammo.Radius;
            this.Speed = ammo.Speed;
        }
    }
}
