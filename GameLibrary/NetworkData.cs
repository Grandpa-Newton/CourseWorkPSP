namespace GameLibrary
{
    /// <summary>
    /// Класс с данными для передачи по сети
    /// </summary>
    public class NetworkData
    {
        /// <summary>
        /// Координата X позиции шара
        /// </summary>
        public float BalloonPositionX;

        /// <summary>
        /// Координата Y позиции шара
        /// </summary>
        public float BalloonPositionY;

        /// <summary>
        /// Данные о выпускаемом снаряде
        /// </summary>
        public BulletData BulletData;

        /// <summary>
        /// Количество топлива
        /// </summary>
        public int Fuel;

        /// <summary>
        /// Был ли изменён тип выпускаемых снарядов
        /// </summary>
        public bool WasAmmoChanged;

        /// <summary>
        /// Код результата выполнения цикла
        /// </summary>
        public int ResultCode;
    }
}