using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using GameLibrary;
using HttpConnectionLibrary;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Ballon_Battle
{
    public partial class GameForm : Form
    {
        /// <summary>
        /// Объект игрового движка
        /// </summary>
        BattleGame gameEngine;
        private NetworkControlsContainer _networkControlsContainer;

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
            gameEngine = new BattleGame();
            _networkControlsContainer = new NetworkControlsContainer(new Control[]
            {
                ipTextBox,
                connectButton,
                createServerButton,
                infoLabel
            });
            _networkControlsContainer.UpdateVisibility(true);
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
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);

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

        private void UpdateNetworkUIScale()
        {
            connectButton.SetBounds((int)(0.05 * Width), (int)(0.85 * Height), (int)(0.25 * Width), (int)(0.05 * Height));
            ipTextBox.SetBounds((int)(0.375 * Width), (int)(0.85 * Height), (int)(0.25 * Width), (int)(0.05 * Height));
            createServerButton.SetBounds((int)(0.7 * Width), (int)(0.85 * Height), (int)(0.25 * Width), (int)(0.05 * Height));

            connectButton.Font = new Font("Arial", 0.01f * Width);
            ipTextBox.Font = new Font("Arial", 0.01f * Width);
            createServerButton.Font = new Font("Arial", 0.01f * Width);

            infoLabel.SetBounds((int)(0.05 * Width), (int)(0.02 * Height), (int)(0.3 * Width), (int)(0.05 * Height));
            infoLabel.Font = new Font("Arial", 0.01f * Width);
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
        private async void glTimer_Tick(object sender, EventArgs e)
        {
            await GetResult();
        }

        private async Task GetResult()
        {
            int resultCode = await gameEngine.Update();

            switch (resultCode)
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

            UpdateNetworkUIScale();
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

        private void createServerButton_Click(object sender, EventArgs e)
        {
            Server server = new Server();
            infoLabel.Text = string.Format("Сервер запущен на IP-адресе: {0}", IpAddressGetter.GetLocalIPAddress());
            int seed = new Random().Next();
            server.OnGetData += (_) => StartGame(seed, server, true);
            server.UpdateData(seed);
        }

        private void StartGame(int seed, IHttpHandler handler, bool isServer)
        {
            handler.ClearAllListeners();
            gameEngine.SetNetworkStartData(handler, isServer, seed);
            glControl.Visible = true;
            glTimer.Start();
            prizeTimer.Start();
            windTimer.Start();
            _networkControlsContainer.UpdateVisibility(false);
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            var ipText = ipTextBox.Text;
            Client client = new Client(ipText);
            infoLabel.Text = string.Format("Попытка подключения к серверу {0}", ipText);
            client.OnGetData += (obj) =>
            {
                StartGame((int)obj, client, false);
            };
            client.GetData<int>();
        }
    }
}
