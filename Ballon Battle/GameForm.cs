using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using GameLibrary;

namespace Ballon_Battle
{
    public partial class GameForm : Form
    {
        /// <summary>
        /// Объект игрового движка
        /// </summary>
        BattleGame gameEngine;

        /// <summary>
        /// Label для отображения текущего состояния первого игрока
        /// </summary>
        Label firstPlayerInfo;

        /// <summary>
        /// Label для отображения текущего состояния второго игрока
        /// </summary>
        Label secondPlayerInfo; // label для отображения текущего состояния второго игрока

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public GameForm()
        {

            InitializeComponent();
            CenterToScreen();
            glControl.Size = this.Size;
            glControl.Visible = true;
            glTimer.Start();
            prizeTimer.Start();
            windTimer.Start();
            gameEngine = new BattleGame();
        }

        /// <summary>
        /// Обработчик события загрузки GLControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glControl_Load(object sender, EventArgs e)
        {
            glControl.MakeCurrent();

            gameEngine.LoadGLControl();

            gameEngine.LoadObjects();

            glControl.SendToBack();

            this.WindowState = FormWindowState.Maximized; // для открытия окна в полном экране
        }

        /// <summary>
        /// Обработчик события для отрисовки GLControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit); // ?

            gameEngine.Draw();

            glControl.SwapBuffers();
        }

        /// <summary>
        /// Обновление информации об игроках в Label
        /// </summary>
        private void updateInfo()
        {
            firstPlayerInfo.SetBounds((int)(0.05 * Width), (int)(0.01 * Height), (int)(0.45 * Width), (int)(0.03 * Height)); // информация первого игрока (здоровье, топливо, броня)
            firstPlayerInfo.Font = new Font("Arial", 0.008f * Width);
            firstPlayerInfo.Text = gameEngine.GetFirstPlayerInfo();

            secondPlayerInfo.SetBounds((int)(0.55 * Width), (int)(0.01 * Height), (int)(0.45 * Width), (int)(0.03 * Height)); // информация первого игрока (здоровье, топливо, броня)
            secondPlayerInfo.Font = new Font("Arial", 0.008f * Width);
            secondPlayerInfo.Text = gameEngine.GetSecondPlayerInfo();
        }

        /// <summary>
        /// Завершение игры
        /// </summary>
        /// <param name="message">Сообщение, выводимое при завершении игры</param>
        private void endGame(string message)
        {
            glTimer.Stop();
            DialogResult result = MessageBox.Show(message, "Конец игры", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Application.Restart();
                Environment.Exit(0);
            }
            else
                this.Close();
        }

        /// <summary>
        /// Обработчик события тика таймера игры перед окончанием игры в случае ничьей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glTimer_DrawTick(object sender, EventArgs e)
        {
            glControl.Refresh();

            if (gameEngine.GetExplodesCount() <= 0)
            {
                endGame("ИГРА ОКОНЧЕНА! НИЧЬЯ! Хотите начать заново?");
            }
        }

        /// <summary>
        /// Обработчик события тика таймера игры перед окончанием игры в случае поражения первого игрока
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glTimer_FirstPlayerLooseTick(object sender, EventArgs e)
        {
            glControl.Refresh();
            if (gameEngine.GetExplodesCount() <= 0)
            {
                endGame("ИГРА ОКОНЧЕНА! ПЕРВОЙ ИГРОК ПРОИГРАЛ. Хотите начать заново?");
            }
        }
        
        /// <summary>
        /// Обработчик события тика таймера игры перед окончанием игры в случае поражения второго игрока
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glTimer_SecondPlayerLooseTick(object sender, EventArgs e)
        {
            glControl.Refresh();
            if (gameEngine.GetExplodesCount() <= 0)
            {
                endGame("ИГРА ОКОНЧЕНА! ВТОРОЙ ИГРОК ПРОИГРАЛ. Хотите начать заново?");
            }
        }

        /// <summary>
        /// Обработчик события тика таймера, отвечающего за появление ветра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void windTimer_Tick(object sender, EventArgs e)
        {
            gameEngine.UpdateWind();
        }

        /// <summary>
        /// Обработчик события для обновления картинки каждые 10 миллисекунд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glTimer_Tick(object sender, EventArgs e)
        {
            int resultCode = gameEngine.Update();

            switch(resultCode)
            {
                case 0:
                    break;
                case 1:
                    glTimer.Stop();
                    prizeTimer.Stop();
                    windTimer.Stop();
                    glTimer.Tick -= glTimer_Tick;
                    
                    glTimer.Tick += glTimer_FirstPlayerLooseTick;
                    glTimer.Start();
                    break;
                case 2:
                    glTimer.Stop();
                    prizeTimer.Stop();
                    windTimer.Stop();
                    glTimer.Tick -= glTimer_Tick;
                    glTimer.Tick += glTimer_SecondPlayerLooseTick;
                    glTimer.Start();
                    break;
                case 3:
                    glTimer.Stop();
                    prizeTimer.Stop();
                    windTimer.Stop();
                    glTimer.Tick -= glTimer_Tick;
                    glTimer.Tick += glTimer_DrawTick;
                    glTimer.Start();
                    break;

            }

            updateInfo();

            glControl.Refresh();
        }

        /// <summary>
        /// Обработчик события тика таймера, отвечающего за генерацию призов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void prizeTimer_Tick(object sender, EventArgs e)
        {
            gameEngine.SpawnPrize(this.Height);

        }

        /// <summary>
        /// Отвечает за изменение размера GLControl при изменении размера окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_Resize(object sender, EventArgs e)
        {
            glControl.Size = this.Size;
            GL.Viewport(0, 0, Width, Height);
        }

        /// <summary>
        /// Обработчик события отпускания кнопки клавиатуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glControl_KeyUp(object sender, KeyEventArgs e)
        {
            gameEngine.UpdateKeyUp(e);
        }

        /// <summary>
        /// Обработчик события нажатия кнопки клавиатуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            gameEngine.UpdateKeyDown(e);
        }
    }
}
