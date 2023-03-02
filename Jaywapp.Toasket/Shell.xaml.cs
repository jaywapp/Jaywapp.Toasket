using AdonisUI.Controls;

namespace Jaywapp.Toasket
{
    /// <summary>
    /// Shell.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Shell : AdonisWindow
    {
        public Shell(ShellViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
