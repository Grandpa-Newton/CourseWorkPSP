using System.Collections.Generic;
using System.Windows.Forms;

namespace Ballon_Battle
{
    /// <summary>
    /// Контейнер для управления элементами UI
    /// </summary>
    internal class NetworkControlsContainer
    {
        /// <summary>
        /// Список с элементами UI
        /// </summary>
        private IEnumerable<Control> _controls;

        /// <summary>
        /// Конструктор для контейнера для управления элементами UI
        /// </summary>
        /// <param name="controls">Список элементов UI</param>
        public NetworkControlsContainer(IEnumerable<Control> controls)
        {
            _controls = controls;
        }

        /// <summary>
        /// Обновление видимости элементов UI
        /// </summary>
        /// <param name="isVisible">Значение, будут ли показаны элементы на экране</param>
        public void UpdateVisibility(bool isVisible)
        {
            foreach (Control control in _controls)
            {
                control.Visible = isVisible;
            }
        }
    }
}
