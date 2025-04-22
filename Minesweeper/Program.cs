using Minesweeper.Core.Interfaces;
using Minesweeper.Game.Model;
using System.Runtime.InteropServices;

namespace Minesweeper;

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

#if DEBUG
        AllocConsole();
        Console.WriteLine("App started!");
#endif

        ApplicationConfiguration.Initialize();

        IPresenter presenter = new Presenter.Presenter(new GameWindow(), new Minefield());
        presenter.Run();
    }

    [DllImport("kernel32.dll")] 
    private static extern bool AllocConsole();
}