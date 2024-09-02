using Microsoft.Extensions.DependencyInjection;
using NLog.Config;
using NLog;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace SCPI_Command_Test_APP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NLog.config");
                LogManager.Configuration = new XmlLoggingConfiguration(configFilePath);

            }
            catch
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string parentDirectory = Directory.GetParent(baseDirectory).Parent.Parent.Parent.FullName;
                string configFilePath = Path.Combine(parentDirectory, "NLog.config");
                LogManager.Configuration = new XmlLoggingConfiguration(configFilePath);
            }

            MainWindow mainView = Services.GetRequiredService<MainWindow>();
            mainView.Show();
        }


        private IServiceProvider ConfigureServices()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainWindowViewModel>()
            });
            return services.BuildServiceProvider();

        }

        public App()
        {
            Services = ConfigureServices();

        }

    }

}
