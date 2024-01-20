namespace MedX.Desktop;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs args)
    {
        base.OnStartup(e: args);

        new MainWindow(
            new ServiceCollection()
                .AddApiServices()
                .BuildServiceProvider())
            .Show();
    }
}
