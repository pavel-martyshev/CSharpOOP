using System.Text;

namespace Minesweeper.Game.Model;

internal class AboutInfo
{
    public string? GameName { get; set; }
    public string? Version { get; set; }
    public string? Author { get; set; }
    public string? ReleaseDate { get; set; }
    public List<string>? Technologies { get; set; }
    public string? Thanks { get; set; }

    public override string ToString()
    {
        StringBuilder stringBuilder = new();

        stringBuilder.AppendLine(
            $"GameName: {GameName}" + Environment.NewLine + 
            $"Version: {Version}" + Environment.NewLine +
            $"Author: {Author}" + Environment.NewLine +
            $"ReleaseDate: {ReleaseDate}" + Environment.NewLine +
            $"Technologies: {string.Join(", ", Technologies!)}" + Environment.NewLine +
            $"Thanks: {Thanks}"
        );

        return stringBuilder.ToString();
    }
}
