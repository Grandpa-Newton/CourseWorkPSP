using OpenTK;
using System.Collections.Generic;
using GraphicsOpenGL;

namespace GameLibrary
{
    /// <summary>
    /// Взрыв после столкновения коллайдеров снарядов или игроков
    /// </summary>
    public class Explode
    {
        /// <summary>
        /// Список текстур для анимации взрыва
        /// </summary>
        List<Texture> animation;

        /// <summary>
        /// Позиция для отрисовки взрыва
        /// </summary>
        Vector2[] position;

        /// <summary>
        /// Номер анимации в списке
        /// </summary>
        public int Count;

        /// <summary>
        /// Конструктор создания взрыва
        /// </summary>
        /// <param name="position">Позиция взрыва</param>
        public Explode(Vector2[] position)
        {
            this.position = position;
            this.Count = 0;
            this.animation = new List<Texture>()
            {
                TextureLoader.LoadTexure("Animation/1.png"),
                TextureLoader.LoadTexure("Animation/2.png"),
                TextureLoader.LoadTexure("Animation/3.png"),
                TextureLoader.LoadTexure("Animation/4.png"),
                TextureLoader.LoadTexure("Animation/5.png"),
            };
        }

        /// <summary>
        /// Отрисовка взрыва
        /// </summary>
        /// <param name="isFlipped">Значение, отвечающее за то, следует ли отразить текстуру объекта или нет 
        /// (true - текстура отражается, false - не отражается</param>
        /// <returns>Показатель, отвечающий за то, закончилась ли анимация или нет
        /// (true - не закончилась, false - закончилась)</returns>
        public bool Draw(bool isFlipped)
        {
            if(Count >= animation.Count)
            {
                return false;
            }

            ObjectDrawer.Draw(animation[Count], position, isFlipped);

            Count++;

            return true;
        }
    }
}
