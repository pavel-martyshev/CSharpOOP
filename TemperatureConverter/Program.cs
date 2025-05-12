using TemperatureConverterTask.Controller;
using TemperatureConverterTask.Model;
using TemperatureConverterTask.View;

namespace TemperatureConverterTask;

internal static class Program
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

        var controller = new TemperatureConversionController(new TemperatureConverterView(), new TemperatureScalesRegistry());

        controller.Run();
    }
}
