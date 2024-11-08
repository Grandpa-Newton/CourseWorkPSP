using System;
using System.Collections.Generic;
using OpenTK;
using System.Drawing;
using System.Diagnostics;
using GraphicsOpenGL;
using AmmoLibrary;

namespace GameLibrary
{
    /// <summary>
    /// Класс воздушного шара (игрока)
    /// </summary>
    public class Balloon
    {
        /// <summary>
        /// Центр позиции шара
        /// </summary>
        public Vector2 PositionCenter;
        /// <summary>
        /// Скорость шара
        /// </summary>
        public Vector2 Speed;

        /// <summary>
        /// Спрайт воздушного шара
        /// </summary>
        public Texture BalloonSprite;

        /// <summary>
        /// Список с текущими снарядами игрока
        /// </summary>
        
        private List<Ammo> ammos;
        /// <summary>
        /// Показатель, отвечающий за то, какой сейчас снаряд выбран у игрока
        /// </summary>
        
        private int currentAmmo=0;
        /// <summary>
        /// Скорость ветра игрока
        /// </summary>
        private Vector2 windSpeed = new Vector2(0.0f, 0.0f); 

        /// <summary>
        /// Показатель, отвечающий за то, работает ветер или нет (true - работает, false - нет)
        /// </summary>
        private bool isWindOn = false;
        
        /// <summary>
        /// Конструктор создания шара
        /// </summary>
        /// <param name="startPosition">Начальная позиция</param>
        /// <param name="baloonSprite">Спрайт игрока</param>
        public Balloon(Vector2 startPosition, Texture baloonSprite)
        {
            this.PositionCenter = startPosition;
            this.BalloonSprite = baloonSprite;
            this.Speed = new Vector2(0, -0.001f);
            this.currentAmmo = 0;
            this.ammos = new List<Ammo>()
            {
                new SupersonicAmmo(),
                new PiercingAmmo(),
                new ExplosiveAmmo(),
            };
        }

        /// <summary>
        /// Показатель брони
        /// </summary>
        public int Armour { get; set; } = 0;

        /// <summary>
        /// Показатель здоровья
        /// </summary>
        public int Health { get; set; } = 100;

        /// <summary>
        /// Показатель топлива
        /// </summary>
        public int Fuel { get; set; } = 1000;

        /// <summary>
        /// Проверка на то, равняется ли показатель здоровья игрока нулю
        /// </summary>
        /// <returns>true - показатель здоровья игрока выше нуля, false - показатель здоровья игрока равно нулю</returns>
        public bool CheckAlive()
        {
            if (Health <= 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Обновление позиции шара при его движении
        /// </summary>
        /// <param name="movement">Показатель, отвечающий за направление движение игрока</param>
        public void Update(Vector2 movement)
        {
            if (Fuel <= 0)
                return;
            PositionCenter += movement;
            Fuel--;
            if (isWindOn)
                PositionCenter += windSpeed;
        }

        /// <summary>
        /// Обновление падения игрока вниз при отсутствии нажатия клавиш
        /// </summary>
        public void Update()
        {
            PositionCenter += Speed;
            if(isWindOn)
                PositionCenter += windSpeed;
        }

        /// <summary>
        /// Получение урона при столкновении со снарядами
        /// </summary>
        public void GetDamage()
        {
            int damage = 15;
            if(Armour>0)
            {
                if(Armour>damage)
                {
                    Armour -= damage;
                }
                else
                {
                    int remainder = damage - Armour;
                    Armour = 0;
                    Health -= remainder;
                }
            }
            else
                Health -= damage;
            if (Health < 0)
                Health = 0;
        }

        /// <summary>
        /// Повышение показателя здоровья
        /// </summary>
        public void IncreaseHealth()
        {
            int extraHealth = 20;
            Health += extraHealth;
            if (Health > 100)
                Health = 100;
        }

        /// <summary>
        /// Повышение показателя брони
        /// </summary>
        public void IncreaseArmour()
        {
            int extraArmour = 20;
            Armour += extraArmour;
            if (Armour > 100)
                Armour = 100;
        }

        /// <summary>
        /// Повышение запасов топлива
        /// </summary>
        public void IncreaseFuel()
        {
            int extraFuel = 350;
            Fuel += extraFuel;
            if (Fuel > 1000)
                Fuel = 1000;
        }

        /// <summary>
        /// Изменение скорости ветра 
        /// </summary>
        /// <param name="windSpeed">Новая скорость ветра</param>
        public void ChangeWindSpeed(Vector2 windSpeed)
        {
            this.windSpeed = windSpeed; 
        }

        /// <summary>
        /// Изменение состояния ветра
        /// </summary>
        /// <param name="isWindOn">Состояние ветра (true - работает, false - не рабоатет)</param>
        public void ChangeWindCondition(bool isWindOn)
        {
            this.isWindOn = isWindOn;
        }

        /// <summary>
        /// Получение границ объекта игрока
        /// </summary>
        /// <returns>Коллайдер игрока</returns>
        public RectangleF GetCollider()
        {
            Vector2[] colliderPosition = GetPosition();

            colliderPosition[3].X += 0.02f; // делаем это для более точного коллайдера; т.к. модель вытянута, будем считать касание о шар дальше его крайней точки
            colliderPosition[2].X -= 0.02f;

            float colliderWidth = (colliderPosition[2].X - colliderPosition[3].X)/2.0f;
            float colliderHeight = (colliderPosition[3].Y - colliderPosition[0].Y)/2.0f;

            float[] convertedLeftTop = CoordinatesConverter.Convert(colliderPosition[3].X, colliderPosition[3].Y);

            RectangleF collider = new RectangleF(convertedLeftTop[0], convertedLeftTop[1], colliderWidth, colliderHeight);

            return collider;
        }

        /// <summary>
        /// Отрисовка игрока
        /// </summary>
        /// <param name="isFlipped">Показатель, отвечающий за отражение по вертикали объекта (true - нужно отражать, false - не нужно)</param>
        public void Draw(bool isFlipped)
        {
            ObjectDrawer.Draw(BalloonSprite, GetPosition(), isFlipped);
        }

        /// <summary>
        /// Замена текущего снаряда
        /// </summary>
        public void ChangeAmmo()
        {
            currentAmmo++;
            if (currentAmmo >= ammos.Count)
                currentAmmo = 0;
        }

        /// <summary>
        /// Получение текущего снаряда
        /// </summary>
        /// <param name="isLeft">Показатель, отвечающий за выпуск снаряда влево (true - выпускается влево, false - вправо)</param>
        /// <returns>Текущий снаряд игрока</returns>
        public Ammo GetCurrentAmmo(bool isLeft)
        {
            Ammo newAmmo = null;
            ammos[currentAmmo].Spawn(PositionCenter-new Vector2(0.01f, 0.07f), isLeft);
            switch(currentAmmo)
            {
                case 0:
                    newAmmo = new SupersonicAmmo(ammos[currentAmmo]);
                    break;
                case 1:
                    newAmmo = new PiercingAmmo(ammos[currentAmmo]);
                    break;
                case 2:
                    newAmmo = new ExplosiveAmmo(ammos[currentAmmo]);
                    break;
            }
            
            return newAmmo;
        }

        /// <summary>
        /// Изменение характеристик снарядов с помощью "декоратора"
        /// </summary>
        public void ChangeAmmoCharesterictics()
        {
            Random random = new Random();
            int decoratorType = random.Next(0, 3);
            switch(decoratorType)
            {
                case 0:
                    for (int i = 0; i < ammos.Count; i++)
                    {
                        ammos[i] = new DistanceDecorator(ammos[i]);
                        Debug.WriteLine("Distance Decorator");
                    }
                    break;
                case 1:
                    for (int i = 0; i < ammos.Count; i++)
                    {
                        ammos[i] = new RadiusDecorator(ammos[i]);

                        Debug.WriteLine("Radius Decorator");
                    }
                    break;
                case 2:
                    for (int i = 0; i < ammos.Count; i++)
                    {
                        ammos[i] = new SpeedDecorator(ammos[i]);

                        Debug.WriteLine("Speed Decorator");
                    }
                    break;
            }
        }

        /// <summary>
        /// Получение текущей позиции игрока
        /// </summary>
        /// <returns>Массив из четырёх элементов, содержащий края позиции игрока</returns>
        public Vector2[] GetPosition()
        {
            float spriteWidth = 0.07f;
            float spriteHeight = 0.14f;
            return new Vector2[4]
            {
                PositionCenter + new Vector2(-spriteWidth, -spriteHeight),
                PositionCenter + new Vector2(spriteWidth, -spriteHeight),
                PositionCenter + new Vector2(spriteWidth, spriteHeight),
                PositionCenter + new Vector2(-spriteWidth, spriteHeight),
            };
        }

        /// <summary>
        /// Получение информации об игроке
        /// </summary>
        /// <returns>Информация об игроке</returns>
        public string GetInfo()
        {
            return $"Health = {Health}, Armour = {Armour}, Fuel = {Fuel}, Radius = {100*ammos[currentAmmo].Radius}, Distance = {100*ammos[currentAmmo].Distance}, Speed = {100*ammos[currentAmmo].Speed.X}";
        }
    }
}
