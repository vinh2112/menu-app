using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Menu_v2
{
    /// <summary>
    /// Interaction logic for UCTool.xaml
    /// </summary>
    public partial class UCTool : System.Windows.Controls.UserControl
    {
        public UCTool()
        {
            InitializeComponent();
        }

        private void btnTool_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Tool tool = new Tool();
            tool.Show();
        }
    }
}
