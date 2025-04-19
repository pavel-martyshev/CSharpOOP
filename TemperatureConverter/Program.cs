using System.Runtime.InteropServices;
using TemperatureConverterTask.Controller;

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

#if DEBUG
        AllocConsole();
        Console.WriteLine("App started!");
#endif

        ApplicationConfiguration.Initialize();

        ITemperatureConversionController controller = new TemperatureConversionController(new TemperatureConverterView());

        controller.Run();
    }

    [DllImport("kernel32.dll")]
    private static extern bool AllocConsole();
}
