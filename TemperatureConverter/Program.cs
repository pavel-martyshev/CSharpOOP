using TemperatureConverterTask.Controllers;

namespace TemperatureConverterTask;

internal static partial class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        TemperatureConversionController controller = new(new TemperatureConverterView());

        controller.Run();
    }
}
