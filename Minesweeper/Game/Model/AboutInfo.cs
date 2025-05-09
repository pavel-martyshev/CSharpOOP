namespace Minesweeper.Game.Model;

internal class AboutInfo
{
    public string? GameName { get; set; }
    public string? Version { get; set; }
    public string? Author { get; set; }
    public string? ReleaseDate { get; set; }
    public List<string>? Technologies { get; set; }
    public string? Thanks { get; set; }

    public override string ToString() => 
        $"""
        GameName: {GameName}
        Version: {Version}
        Author: {Author}
        ReleaseDate: {ReleaseDate}
        Technologies: {string.Join(", ", Technologies!)}
        Thanks: {Thanks}
        """;
}
