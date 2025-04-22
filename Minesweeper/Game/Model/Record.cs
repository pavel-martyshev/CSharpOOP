using Minesweeper.Core.Enums;
using System.Text;

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

    public override string ToString()
    {
        StringBuilder stringBuilder = new();

        stringBuilder
            .Append(PlayerName)
            .Append(Environment.NewLine)
            .Append(TimeSeconds)
            .Append(Environment.NewLine)
            .Append(Difficulty)
            .Append(Environment.NewLine);
        
        return stringBuilder.ToString();
    }
}
