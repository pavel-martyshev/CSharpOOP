using System.Text;

namespace Minesweeper.Game.Model;

internal class AboutInfo
{
    public string? GameName { get; set; }
    public string? Version { get; set; }
    public string? Author { get; set; }
    public string? ReleaseDate { get; set; }
    public string[]? Technologies { get; set; }
    public string? Thanks { get; set; }

    public override string ToString()
    {
        StringBuilder stringBuilder = new();

        stringBuilder
            .Append($"GameName: {GameName}")
            .Append(Environment.NewLine)
            .Append($"Version: {Version}")
            .Append(Environment.NewLine)
            .Append($"Author: {Author}")
            .Append(Environment.NewLine)
            .Append($"ReleaseDate: {ReleaseDate}")
            .Append(Environment.NewLine)
            .Append($"Technologies: {string.Join(", ", Technologies!)}")
            .Append(Environment.NewLine)
            .Append($"Thanks: {Thanks}");

        return stringBuilder.ToString();
    }
}
