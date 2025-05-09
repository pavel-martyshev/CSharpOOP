using Minesweeper.Core.Enums;

namespace Minesweeper.Game.Model;

internal class Record
{
    public string? PlayerName { get; set; }

    public int TimeSeconds { get; set; }

    public Difficulty Difficulty { get; set; }

    public Record() { }

    public Record(string playerName, int timeSeconds, Difficulty difficulty)
    {
        PlayerName = playerName;
        TimeSeconds = timeSeconds;
        Difficulty = difficulty;
    }

    public override string ToString() => $"""{PlayerName}{Environment.NewLine}{TimeSeconds}{Environment.NewLine}{Difficulty}""";
}
