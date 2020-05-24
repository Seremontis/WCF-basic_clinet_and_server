using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace ProjectClient
{
    public class BaseWindow:Window
    {
        internal static ChannelProjectServer connector = null;
        public BaseWindow()
        {
            if (connector == null)
            {
                connector = new ChannelProjectServer();
                if (!connector.ConnectWCF())
                {
                    DialogResult button = MessageBox.Show(connector.ErroMessage, "Błąd komunikacji z serwerem", MessageBoxButtons.OK);
                    if (button == System.Windows.Forms.DialogResult.OK)
                        Environment.Exit(0);
                }
            }
        }
    }
}
