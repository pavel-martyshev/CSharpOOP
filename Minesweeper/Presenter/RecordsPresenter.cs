using Minesweeper.Core.Enums;
using Minesweeper.Core.Interfaces;
using Minesweeper.Game.Model;
using System.Text;
using System.Text.Json;

namespace Minesweeper.Presenter;

internal class RecordsPresenter : IRecordsPresenter
{
    private readonly IMinesweeperView _view;

    private readonly List<Record> _records;

    private static readonly string _appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

    private static readonly string _recordsFolderPath = Path.Combine(_appDataPath, "Minesweeper");

    private static readonly string _recordsFilePath = Path.Combine(_recordsFolderPath, "records.json");

    public RecordsPresenter(IMinesweeperView view)
    {
        _view = view;
        _view.RequestRecordsString += GetRecordsString;
        _view.SaveRecord += SaveRecord;

        if (!Directory.Exists(_recordsFolderPath))
        {
            Directory.CreateDirectory(_recordsFolderPath);
        }

        if (!File.Exists(_recordsFilePath))
        {
            File.WriteAllText(_recordsFilePath, "[]");
            _records = [];
        }

        _records = JsonSerializer.Deserialize<List<Record>>(File.ReadAllText(_recordsFilePath))!;
    }

    public string GetRecordsString()
    {
        if (_records.Count == 0)
        {
            return "There are no records";
        }

        StringBuilder stringBuilder = new();

        for (int i = 0; i < _records.Count; i++)
        {
            stringBuilder.AppendLine(
                $"{i + 1} place:{Environment.NewLine}" +
                $"  Player name: {_records[i].PlayerName}{Environment.NewLine}" +
                $"  Time: {_records[i].TimeSeconds}{Environment.NewLine}" +
                $"  Difficulty: {_records[i].Difficulty}{Environment.NewLine}"
            );
        }

        return stringBuilder.ToString();
    }

    public void SaveRecord((string, int, Difficulty) recordInfo)
    {
        var (playerName, timeSeconds, difficulty) = recordInfo;

        _records.Add(new(
            playerName,
            timeSeconds,
            difficulty
        ));

        var json = JsonSerializer.Serialize(_records);
        File.WriteAllText(_recordsFilePath, json);
    }
}
