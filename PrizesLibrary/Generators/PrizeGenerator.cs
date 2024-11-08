using OpenTK;
using System;

namespace PrizesLibrary
{
    /// <summary>
    /// Генератор призов, реализующий шаблон "фабричный метод"
    /// </summary>
    public class PrizeGenerator
    {
        /// <summary>
        /// Объект для случайной генерации приза
        /// </summary>
        Random random;

        /// <summary>
        /// Конструктор для создания объекта генератора приза
        /// </summary>
        public PrizeGenerator()
        {
            random = new Random();
        }
        /// <summary>
        /// Создание объекта приза
        /// </summary>
        /// <param name="height">Высота экрана</param>
        /// <returns>Созданный генератором приз</returns>
        public Prize Create(int height)
        {
            Prize newPrize=null;
            float prizePozitionX;
            bool isLeft;

            int prizeSpawnSide = random.Next(0, 2);

            if (prizeSpawnSide == 0)
            {
                isLeft = false;
                prizePozitionX = -1.05f;
            }

            else
            {
                isLeft = true;
                prizePozitionX = 1.05f;
            }

            float prizePozitionY = (float)(random.Next((int)(-0.6f * height), (int)(0.7f * height))) / (float)height;

            int prizeType = random.Next(0, 4);

            switch (prizeType)
            {
                case 0:
                    newPrize = new AmmoPrize(new Vector2(prizePozitionX, prizePozitionY), isLeft);
                    break;
                case 1:
                    newPrize = new ArmourPrize(new Vector2(prizePozitionX, prizePozitionY), isLeft);
                    break;
                case 2:
                    newPrize = new FuelPrize(new Vector2(prizePozitionX, prizePozitionY), isLeft);
                    break;
                case 3:
                    newPrize = new HealthPrize(new Vector2(prizePozitionX, prizePozitionY), isLeft);
                    break;
            }

            return newPrize;
        }
    }
}