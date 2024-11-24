namespace AmmoLibrary
{
    /// <summary>
    /// Класс расширений для класса Ammo
    /// </summary>
    public static class AmmoExtensions
    {
        /// <summary>
        /// Сериализация типа снаряда
        /// </summary>
        /// <param name="ammo">Объект снаряда</param>
        /// <returns>Код типа снаряда</returns>
        public static int SerializeAmmo(this Ammo ammo)
        {
            if (ammo is ExplosiveAmmo)
            {
                return 0;
            }
            if (ammo is PiercingAmmo)
            {
                return 1;
            }
            if (ammo is SupersonicAmmo)
            {
                return 2;
            }

            return -1;
        }
    }
}
