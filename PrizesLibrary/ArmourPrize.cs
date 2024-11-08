using GraphicsOpenGL;
using OpenTK;

namespace PrizesLibrary
{
    /// <summary>
    /// Приз, отвечающий за увеличение показателя брони воздушного шара
    /// </summary>
    public class ArmourPrize : Prize
    {
        /// <summary>
        /// Конструктор приза при её генерации на экран
        /// </summary>
        /// <param name="centerPosition">Начальная позиция приза</param>
        /// <param name="isLeft">Направление полёта приза (true - влево, false - вправо)</param>
        public ArmourPrize(Vector2 centerPosition, bool isLeft) : base(centerPosition, isLeft)
        {
            this.CenterPosition = centerPosition;
            this.IsLeft = isLeft;
            this.Sprite = TextureLoader.LoadTexure("armorPrize.png");
        }

        /// <summary>
        /// Получение значения скорости приза
        /// </summary>
        /// <returns>Скорость приза</returns>
        protected override Vector2 GetSpeed()
        {
            return new Vector2(0.005f, 0.0f);
        }
    }
}
