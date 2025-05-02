namespace Minesweeper.Core.Interfaces;

internal interface IGameTimer
{
    int ElapsedSeconds { get; set; }

    event Action<int>? Tick;

    void Start();

    void Stop();
}
