using Minesweeper.Core.Interfaces;
using Minesweeper.Game.Model;
using System.Runtime.InteropServices;

namespace Minesweeper;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        IMinesweeperView view = new GameWindow();

        IRecordsPresenter recordsPresenter = new Presenter.RecordsPresenter(view);

        IGamePresenter gamePresenter = new Presenter.GamePresenter(view, new MineField(), new GameTimer());
        gamePresenter.Run();
    }

    //[DllImport("kernel32.dll")]
    //private static extern bool AllocConsole();
}