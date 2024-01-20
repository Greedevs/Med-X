namespace MedX.Desktop;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        new MainWindow(
            new ServiceCollection()
                .AddApiServices()
                .BuildServiceProvider())
            .Show();
    }
}
