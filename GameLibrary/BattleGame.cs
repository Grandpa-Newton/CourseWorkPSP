using AmmoLibrary;
using GraphicsOpenGL;
using OpenTK;
using PrizesLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using System.Windows.Forms;

namespace GameLibrary
{
    /// <summary>
    /// Движок игры, содержащий всю основную логику
    /// </summary>
    public class BattleGame
    {
        /// <summary>
        /// Текстура заднего фона сцены
        /// </summary>
        Texture backgroundTexture;

        /// <summary>
        /// Текстура земли
        /// </summary>
        Texture landTexture;

        /// <summary>
        /// Текстура травы
        /// </summary>
        Texture grassTexture;

        /// <summary>
        /// Объект первого игрока
        /// </summary>
        Balloon firstPlayer;

        /// <summary>
        /// Объект второго игрока
        /// </summary>
        Balloon secondPlayer;

        /// <summary>
        /// Границы земли
        /// </summary>
        RectangleF landCollider;

        /// <summary>
        /// Границы экрана
        /// </summary>
        RectangleF screenCollider;

        /// <summary>
        /// Границы объекта первого игрока
        /// </summary>
        RectangleF firstPlayerCollider;

        /// <summary>
        /// Границы объекта второго игрока
        /// </summary>
        RectangleF secondPlayerCollider;

        /// <summary>
        /// Границы объекта приза, находящегося на сцене
        /// </summary>
        RectangleF currentPrizeCollider;

        /// <summary>
        /// Список с выпущенными первым игроком снарядами, находящимися на сцене
        /// </summary>
        List<Ammo> firstAmmos;

        /// <summary>
        /// Список с выпущенными вторым игроком снарядами, находящимися на сцене
        /// </summary>
        List<Ammo> secondAmmos;

        /// <summary>
        /// Список с взрывами, происходящими на сцене
        /// </summary>
        List<Explode> explodes;

        /// <summary>
        /// Объект для генерации случайных чисел
        /// </summary>
        Random random = new Random();

        /// <summary>
        /// Объект текущего приза
        /// </summary>
        Prize currentPrize = null;

        /// <summary>
        /// Максимально возможная скорость ветра, умноженная на 10000
        /// </summary>
        int maxWindSpeed = 20;

        /// <summary>
        /// Минимально возможная скорость ветра, умноженная на 10000
        /// </summary>
        int minWindSpeed = 5;

        /// <summary>
        /// Количество тиков таймера ветра
        /// </summary>
        int windTicks = 0;

        /// <summary>
        /// Переменная, отвечающая за направление ветра для первого игркоа (true - ветер дует налево, false - направо)
        /// </summary>
        bool isFirstPlayerWindLeft = false;

        /// <summary>
        /// Переменная, отвечающая за направление ветра для второго игркоа (true - ветер дует налево, false - направо)
        /// </summary>
        bool isSecondPlayerWindLeft = false;

        /// <summary>
        /// Список для проверки нажатия кнопок игроками (W, S, I, K, J, D, A, L, X, M)
        /// </summary>
        List<bool> keysDown;

        /// <summary>
        /// Количество тиков выстрелов второго игрока, отвечает за выпуск снарядов не ранее 50 тиков
        /// </summary>
        int secondPlayerTicks = 50;

        /// <summary>
        /// Количество тиков выстрелов первого игрока, отвечает за выпуск снарядов не ранее 50 тиков
        /// </summary>
        int firstPlayerTicks = 50;
        
        /// <summary>
        /// Объект генератора призов
        /// </summary>
        PrizeGenerator prizeGenerator = new PrizeGenerator();

        /// <summary>
        /// Загружает объекты, необходимые для процесса игры
        /// </summary>
        public void LoadObjects()
        {
            backgroundTexture = TextureLoader.LoadTexure("clouds.jpg");

            Texture firstPlayerTexture = TextureLoader.LoadTexure("firstPlayerBalloon.png");

            firstPlayer = new Balloon(new Vector2(-0.7f, 0.0f), firstPlayerTexture);

            Texture secondPlayerTexture = TextureLoader.LoadTexure("secondPlayerBalloon.png");

            secondPlayer = new Balloon(new Vector2(0.7f, 0.0f), secondPlayerTexture);

            landTexture = TextureLoader.LoadTexure("testLandNew.png");

            grassTexture = TextureLoader.LoadTexure("grasstexture.png");

            firstAmmos = new List<Ammo>();

            secondAmmos = new List<Ammo>();

            screenCollider = new RectangleF(0.0f, 0.05f, 1.0f, 0.825f);

            landCollider = new RectangleF(0.0f, 0.89f, 1.0f, 0.12f);

            explodes = new List<Explode>();

            keysDown = new List<bool>();

            for (int i = 0; i < 8; i++)
                keysDown.Add(false);
        }

        /// <summary>
        /// Включение, необходимых для функционирования OpenGL, функций
        /// </summary>
        public void LoadGLControl()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        /// <summary>
        /// Отрисовывает объекты (фон, траву, землю, игроков, снаряды, взрывы и приз)
        /// </summary>
        public void Draw()
        {
            ObjectDrawer.Draw(backgroundTexture, new Vector2[4]
            {
                new Vector2(-1.0f, -1.0f),
                new Vector2(1.0f, -1.0f),
                new Vector2(1.0f, 1.0f),
                new Vector2(-1.0f, 1.0f),
            }, false);

            ObjectDrawer.Draw(grassTexture, new Vector2[4]
            {
                new Vector2(-1.0f, -0.8f),
                new Vector2(1.0f, -0.8f),
                new Vector2(1.0f, -0.55f),
                new Vector2(-1.0f, -0.55f),
            }, false);

            ObjectDrawer.Draw(landTexture, new Vector2[4]
            {
                new Vector2(-1.0f, -1.0f),
                new Vector2(1.0f, -1.0f),
                new Vector2(1.0f, -0.78f),
                new Vector2(-1.0f, -0.78f),
            }, false);


            firstPlayer.Draw(false);
            secondPlayer.Draw(false);

            foreach (var item in firstAmmos)
            {
                item.Draw();
            }
            foreach (var item in secondAmmos)
            {
                item.Draw();
            }

            for (int i = 0; i < explodes.Count; i++)
            {
                int thisCount = explodes.Count;

                if (!explodes[i].Draw(false))
                {
                    explodes.RemoveAt(i);
                }

                if (thisCount != explodes.Count)
                    i--;

            }
            if (currentPrize != null)
                currentPrize.Draw(false);
        }

        /// <summary>
        /// Обновление игры
        /// </summary>
        /// <returns>Код завершения программы (0 - продолжение игры,
        /// 1 - завершение игры поражением первого игрока, 
        /// 2 - завершение игры поражением второго игрока, 
        /// 3 - завершение игры ничьей)
        /// </returns>
        public int Update()
        {
            firstPlayerTicks++;
            secondPlayerTicks++;

            firstPlayer.Update();
            secondPlayer.Update();

            firstPlayerCollider = firstPlayer.GetCollider();
            secondPlayerCollider = secondPlayer.GetCollider();

            UpdateInput();
            int codeResult = CheckCollisions(); 
            if (codeResult != 0)
            {
                return codeResult;
            }
            UpdateAmmos();
            UpdatePrize();

            return 0;
        }

        /// <summary>
        /// Обновление нажатия кнопок на клавиатуре игроками
        /// </summary>
        private void UpdateInput()
        {
            if (keysDown[0] && (firstPlayerCollider.Y > screenCollider.Y))
                firstPlayer.Update(new Vector2(0f, 0.01f));
            if (keysDown[1])
                firstPlayer.Update(new Vector2(0f, -0.01f));
            if (keysDown[2] && (secondPlayerCollider.Y > screenCollider.Y))
                secondPlayer.Update(new Vector2(0f, 0.01f));
            if (keysDown[3])
                secondPlayer.Update(new Vector2(0f, -0.01f));
            if ((keysDown[4] || keysDown[7]) && secondPlayerTicks >= 50)
            {
                secondPlayerTicks = 0;
                Ammo newAmmo = null;
                if (keysDown[4])
                    newAmmo = secondPlayer.GetCurrentAmmo(true);
                else if (keysDown[7])
                    newAmmo = secondPlayer.GetCurrentAmmo(false);
                //   Debug.WriteLine($"Distance={newAmmo.Distance}, Radius={newAmmo.Radius}, Speed={newAmmo.Speed.X}");
                secondAmmos.Add(newAmmo);

            }
            if ((keysDown[5] || keysDown[6]) && firstPlayerTicks >= 50)
            {
                firstPlayerTicks = 0;
                Ammo newAmmo = null;
                if (keysDown[5])
                    newAmmo = firstPlayer.GetCurrentAmmo(false);
                else if (keysDown[6])
                    newAmmo = firstPlayer.GetCurrentAmmo(true);

                //   Debug.WriteLine($"Distance={newAmmo.Distance}, Radius={newAmmo.Radius}, Speed={newAmmo.Speed.X}");
                firstAmmos.Add(newAmmo);
            }
        }

        /// <summary>
        /// Обработка столкновения коллайдеров и проверка на то, больше ли 0 значение здоровья шаров
        /// </summary>
        /// <returns>Код завершения метода (0 - продолжение игры,
        /// 1 - завершение игры поражением первого игрока, 
        /// 2 - завершение игры поражением второго игрока, 
        /// 3 - завершение игры ничьей)
        /// </returns></returns>
        private int CheckCollisions()
        {
            if ((firstPlayerCollider.X <= screenCollider.X) && isFirstPlayerWindLeft) // ?
            {
                firstPlayer.ChangeWindCondition(false);
            }
            else if ((firstPlayerCollider.X + secondPlayerCollider.Width >= screenCollider.X + screenCollider.Width) && !isFirstPlayerWindLeft)
            {
                firstPlayer.ChangeWindCondition(false);
            }
            else
                firstPlayer.ChangeWindCondition(true);
            if ((secondPlayerCollider.X + secondPlayerCollider.Width >= screenCollider.X + screenCollider.Width) && !isSecondPlayerWindLeft) // || 
            {
                secondPlayer.ChangeWindCondition(false);
            }
            else if ((secondPlayerCollider.X <= screenCollider.X) && isSecondPlayerWindLeft)
            {
                secondPlayer.ChangeWindCondition(false);
            }
            else
                secondPlayer.ChangeWindCondition(true);
            if (landCollider.IntersectsWith(firstPlayerCollider) || !firstPlayer.CheckAlive())
            {
                explodes.Add(new Explode(firstPlayer.GetPosition()));
                return 1;
            }
            if (landCollider.IntersectsWith(secondPlayerCollider) || !secondPlayer.CheckAlive())
            {

                explodes.Add(new Explode(secondPlayer.GetPosition()));
                return 2;
            }
            if (firstPlayerCollider.IntersectsWith(secondPlayerCollider))
            {
                explodes.Add(new Explode(firstPlayer.GetPosition()));
                explodes.Add(new Explode(secondPlayer.GetPosition()));
                return 3;
            }
            return 0;
        }

        /// <summary>
        /// Обновление состояния выпущенных снарядов
        /// </summary>
        private void UpdateAmmos()
        {
            for (int i = 0; i < firstAmmos.Count; i++)
            {
                int thisCount = firstAmmos.Count;
                RectangleF ammoCollider = firstAmmos[i].GetCollider(false);
                firstAmmos[i].Update();
                if (secondPlayerCollider.IntersectsWith(ammoCollider))
                {
                    firstAmmos[i].UpdatePosition(true);
                    explodes.Add(new Explode(firstAmmos[i].Position));
                    firstAmmos.RemoveAt(i);
                    secondPlayer.GetDamage();
                }
                else if (!ammoCollider.IntersectsWith(screenCollider))
                {
                    firstAmmos.RemoveAt(i);
                }
                else if (firstAmmos[i].Distance <= 0)
                {
                    RectangleF ammoExplode = firstAmmos[i].GetCollider(true);
                    explodes.Add(new Explode(firstAmmos[i].Position));
                    firstAmmos.RemoveAt(i);
                    if (ammoExplode.IntersectsWith(secondPlayerCollider))
                    {
                        secondPlayer.GetDamage();
                    }
                }
                if (thisCount != firstAmmos.Count)
                    i--;
            }

            for (int i = 0; i < secondAmmos.Count; i++)
            {
                int thisCount = secondAmmos.Count;
                RectangleF ammoCollider = secondAmmos[i].GetCollider(false);
                secondAmmos[i].Update();
                if (firstPlayerCollider.IntersectsWith(ammoCollider))
                {
                    secondAmmos[i].UpdatePosition(true);
                    explodes.Add(new Explode(secondAmmos[i].Position));
                    secondAmmos.RemoveAt(i);
                    firstPlayer.GetDamage();
                }
                else if (!ammoCollider.IntersectsWith(screenCollider))
                {
                    secondAmmos.RemoveAt(i);
                }
                else if (secondAmmos[i].Distance <= 0)
                {
                    RectangleF ammoExplode = secondAmmos[i].GetCollider(true);
                    explodes.Add(new Explode(secondAmmos[i].Position));
                    secondAmmos.RemoveAt(i);
                    if (ammoExplode.IntersectsWith(firstPlayerCollider))
                    {

                        firstPlayer.GetDamage();
                    }
                }
                if (thisCount != secondAmmos.Count)
                    i--;
            }
        }

        /// <summary>
        /// Обновление состояния текущего приза и добавление эффектов от него
        /// </summary>
        private void UpdatePrize()
        {
            if (currentPrize != null)
            {
                currentPrizeCollider = currentPrize.GetCollider();
                currentPrize.Update();
                if (firstPlayerCollider.IntersectsWith(currentPrizeCollider))
                {
                    if (currentPrize is AmmoPrize)
                    {
                        firstPlayer.ChangeAmmoCharesterictics();
                        currentPrize = null;
                    }
                    else if (currentPrize is ArmourPrize)
                    {
                        firstPlayer.IncreaseArmour();
                        currentPrize = null;
                    }
                    else if (currentPrize is FuelPrize)
                    {
                        firstPlayer.IncreaseFuel();
                        currentPrize = null;
                    }
                    else if (currentPrize is HealthPrize)
                    {
                        firstPlayer.IncreaseHealth();
                        currentPrize = null;
                    }
                }
                if (currentPrize != null && secondPlayerCollider.IntersectsWith(currentPrizeCollider))
                {
                    if (currentPrize is AmmoPrize)
                    {
                        secondPlayer.ChangeAmmoCharesterictics();
                        currentPrize = null;
                    }
                    else if (currentPrize is ArmourPrize)
                    {
                        secondPlayer.IncreaseArmour();
                        currentPrize = null;
                    }
                    else if (currentPrize is FuelPrize)
                    {
                        secondPlayer.IncreaseFuel();
                        currentPrize = null;
                    }
                    else if (currentPrize is HealthPrize)
                    {
                        secondPlayer.IncreaseHealth();
                        currentPrize = null;
                    }
                }
                if (currentPrize != null && !screenCollider.IntersectsWith(currentPrizeCollider))
                {
                    currentPrize = null;
                }
            }
        }

        /// <summary>
        /// Обновление состояния ветра
        /// </summary>
        public void UpdateWind()
        {
            if (windTicks == 1)
            {
                firstPlayer.ChangeWindCondition(false);
                secondPlayer.ChangeWindCondition(false);
            }
            else
            {
                int windDirection = random.Next(0, 2);
                float windSpeed = random.Next(minWindSpeed, maxWindSpeed + 1) / 10000f;

                switch (windDirection)
                {
                    case 0:
                        firstPlayer.ChangeWindSpeed(new Vector2(-windSpeed, 0.0f));
                        isFirstPlayerWindLeft = true;
                        break;
                    case 1:

                        firstPlayer.ChangeWindSpeed(new Vector2(windSpeed, 0.0f));
                        isFirstPlayerWindLeft = false;
                        break;
                }
                firstPlayer.ChangeWindCondition(true);

                windDirection = random.Next(0, 2);
                windSpeed = random.Next(minWindSpeed, maxWindSpeed + 1) / 10000f;

                switch (windDirection)
                {
                    case 0:

                        secondPlayer.ChangeWindSpeed(new Vector2(-windSpeed, 0.0f));
                        isSecondPlayerWindLeft = true;
                        break;
                    case 1:

                        secondPlayer.ChangeWindSpeed(new Vector2(windSpeed, 0.0f));
                        isSecondPlayerWindLeft = false;
                        break;
                }
                secondPlayer.ChangeWindCondition(true);
            }
            windTicks++;
            if (windTicks >= 2)
                windTicks = 0;
        }

        /// <summary>
        /// Генерация приза в случае отсутствия на сцене приза
        /// </summary>
        /// <param name="height">Текущая высота экрана</param>
        public void SpawnPrize(int height)
        {
            if (currentPrize != null)
                return;

            Prize newPrize = prizeGenerator.Create(height);

            currentPrize = newPrize;
        }

        /// <summary>
        /// Обновление списка с нажатыми кнопками после события нажатия клавиши
        /// </summary>
        /// <param name="e">Нажатая клавиша</param>
        public void UpdateKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    keysDown[0] = true;
                    break;

                case Keys.S:
                    keysDown[1] = true;
                    break;

                case Keys.I:
                    keysDown[2] = true;
                    break;

                case Keys.K:
                    keysDown[3] = true;
                    break;

                case Keys.J:
                    keysDown[4] = true;
                    break;

                case Keys.D:
                    keysDown[5] = true;
                    break;

                case Keys.A:
                    keysDown[6] = true;
                    break;

                case Keys.L:
                    keysDown[7] = true;
                    break;
            }
        }

        /// <summary>
        /// Обновление списка с нажатыми кнопками после события отпускания клавиши
        /// </summary>
        /// <param name="e">Отжатая клавиша</param>
        public void UpdateKeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    keysDown[0] = false;
                    break;

                case Keys.S:
                    keysDown[1] = false;
                    break;

                case Keys.I:
                    keysDown[2] = false;
                    break;

                case Keys.K:
                    keysDown[3] = false;
                    break;

                case Keys.J:
                    keysDown[4] = false;
                    break;

                case Keys.D:
                    keysDown[5] = false;
                    break;

                case Keys.A:
                    keysDown[6] = false;
                    break;

                case Keys.L:
                    keysDown[7] = false;
                    break;

                case Keys.M:
                    secondPlayer.ChangeAmmo();
                    break;

                case Keys.X:
                    firstPlayer.ChangeAmmo();
                    break;

            }
        }

        /// <summary>
        /// Получение информации о первом игроке
        /// </summary>
        /// <returns>Информация о первом игроке</returns>
        public string GetFirstPlayerInfo()
        {
            return firstPlayer.GetInfo();
        }

        /// <summary>
        /// Получение информации о втором игркое
        /// </summary>
        /// <returns>Информация о втором игроке</returns>
        public string GetSecondPlayerInfo()
        {
            return secondPlayer.GetInfo();
        }

        /// <summary>
        /// Получение значения количества взрывов на экране
        /// </summary>
        /// <returns>Количество взрывов на экране</returns>
        public int GetExplodesCount()
        {
            return explodes.Count;
        }
    }
}