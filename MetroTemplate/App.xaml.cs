using System.Windows;

namespace Gridex
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjYzMDc5QDMxMzgyZTMxMmUzMGJzTytocEN3NGNuNlVzaTdrcnRNR0hzL2xlakdPY1VnYUZMalRpUVc0aDQ9");
            MainWindow wnd = new MainWindow(e.Args);
            wnd.Show();
        }
    }
}
