namespace GameLibrary
{
    /// <summary>
    /// Класс с информацией о выпускаемом снаряде
    /// </summary>
    public class BulletData
    {
        /// <summary>
        /// Координата X позиции снаряда
        /// </summary>
        public float PositionX;
        /// <summary>
        /// Координата Y позиции снаряда
        /// </summary>
        public float PositionY;
        /// <summary>
        /// Показатель, направлен ли снаряд влево
        /// </summary>
        public bool IsLeft;

        /// <summary>
        /// Код типа снаряда
        /// </summary>
        public int AmmoType;
    }
}