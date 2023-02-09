using PCController.UserUI.Models;

namespace PCController.UserUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Setting.Current = Setting.Load();
            InitializeComponent();
        }
    }
}