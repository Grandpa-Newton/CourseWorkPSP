namespace AmmoLibrary
{
    /// <summary>
    /// Декоратор для увеличения радиуса действия пули
    /// </summary>
    public class RadiusDecorator : AmmoDecorator
    {
        /// <summary>
        /// Конструктор для создания декоратора для увеличения радиуса действия снаряда
        /// </summary>
        /// <param name="ammo">Улучшаемый снаряд</param>
        public RadiusDecorator(Ammo ammo) : base(ammo)
        {
            this.Sprite = ammo.Sprite;

            this.Distance = ammo.Distance;
            this.Radius = ammo.Radius * 1.2f;
            this.Speed = ammo.Speed;
        }
    }
}
