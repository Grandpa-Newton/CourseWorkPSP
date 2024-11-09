using System.Collections.Generic;
using System.Windows.Forms;

namespace Ballon_Battle
{
    internal class NetworkControlsContainer
    {
        private IEnumerable<Control> _controls;

        public NetworkControlsContainer(IEnumerable<Control> controls)
        {
            _controls = controls;
        }

        public void UpdateVisibility(bool isVisible)
        {
            foreach (Control control in _controls)
            {
                control.Visible = isVisible;
            }
        }
    }
}
