namespace Minesweeper.Game.Model;

using Minesweeper.Core.Interfaces;
using System.Timers;

internal class GameTimer : IGameTimer
{
    private readonly Timer _timer;

    public int ElapsedSeconds { get; set; }

    public event Action<int>? Tick;

    public GameTimer()
    {
        _timer = new Timer(1000);
        _timer.Elapsed += OnElapsed;
        _timer.AutoReset = true;
    }

    private void OnElapsed(object? sender, ElapsedEventArgs e)
    {
        ElapsedSeconds++;
        Tick?.Invoke(ElapsedSeconds);
    }

    public void Start()
    {
        ElapsedSeconds = 0;
        _timer.Start();
    }

    public void Stop() => _timer.Stop();
}
