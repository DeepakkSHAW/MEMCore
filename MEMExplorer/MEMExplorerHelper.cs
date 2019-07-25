using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEMExplorer
{
   internal static class MEMExplorerHelper
    {
        internal static void SelectAllText(this System.Windows.Controls.TextBox tb)
        {
            tb.Dispatcher.BeginInvoke(
                new Action(delegate
                {
                    tb.SelectAll();
                }), System.Windows.Threading.DispatcherPriority.Input);
        }
    }
}
