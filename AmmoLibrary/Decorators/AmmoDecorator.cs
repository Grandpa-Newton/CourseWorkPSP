namespace AmmoLibrary
{
    /// <summary>
    /// Декоратор для улучшения характеристик снаряда
    /// </summary>
    public abstract class AmmoDecorator : Ammo
    {
        /// <summary>
        /// Улучшаемый снаряд
        /// </summary>
        protected Ammo Ammo;

        /// <summary>
        /// Конструктор создания декоратора для улучшения характеристик снаряда
        /// </summary>
        /// <param name="ammo">Улучшаемый снаряд</param>
        public AmmoDecorator(Ammo ammo) : base(ammo)
        {
            this.Ammo = ammo;
        }
    }
}
