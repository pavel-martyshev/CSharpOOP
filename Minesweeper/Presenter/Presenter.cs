using Minesweeper.Core.Enums;
using Minesweeper.Core.Interfaces;
using Minesweeper.Game.Model;
using System.Text;
using System.Text.Json;

namespace Minesweeper.Presenter;

internal class Presenter : IPresenter
{
    private readonly IMinesweeperView _view;

    private readonly IMinefield _minefield;

    private List<Record> _records;

    private static readonly string _appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

    private static readonly string _recordsFolderPath = Path.Combine(_appDataPath, "Minesweeper");

    private static readonly string _recordsFilePath = Path.Combine(_recordsFolderPath, "records.json");

    public Presenter(IMinesweeperView view, IMinefield minefield)
    {
        _records = DeserializeRecords();

        _view = view;
        _minefield = minefield;

        _minefield.AllSafeCellsRevealed += AllSafeRevealedCellsHandler;
        _minefield.GenerateNewMineField(_view.PlayingFieldRowsCount, _view.PlayingFieldColumnsCount);

# if DEBUG
        Console.WriteLine(_minefield);
#endif

        _view.SetMinesCount(_minefield.GetMinesLeft());

        _view.RequestCellState += GetCellState;
        _view.OnCellLeftClick += OnCellLeftClickHandler;

        _view.OnCellRightClick += OnCellRightClickHandler;
        _view.OnDifficultyChange += DifficultyChangeHandler;

        _view.Restart += Restart;
        _view.RequestAboutInfo += GetAboutInfo;

        _view.RequestRecordsString += GetRecordsString;
        _view.SaveRecord += SaveRecord;
    }

    private List<Record> DeserializeRecords()
    {
        if (!Directory.Exists(_recordsFolderPath))
        {
            Directory.CreateDirectory(_recordsFolderPath);
        }

        if (!File.Exists(_recordsFilePath))
        {
            File.WriteAllText(_recordsFilePath, "[]");
            return [];
        }

        List<Record> records = JsonSerializer.Deserialize<List<Record>>(File.ReadAllText(_recordsFilePath))!;
        return records.OrderBy(r => r.TimeSeconds).ToList();
    }

    public string GetRecordsString()
    {
        StringBuilder stringBuilder = new();

        for (int i = 0; i < _records.Count; i++)
        {
            stringBuilder
            .Append($"{i + 1} place:{Environment.NewLine}")
            .Append($"  Player name: {_records[i].PlayerName}{Environment.NewLine}")
            .Append($"  Time: {_records[i].TimeSeconds}{Environment.NewLine}")
            .Append($"  Difficulty: {_records[i].Difficulty}{Environment.NewLine}")
            .Append(Environment.NewLine);
        }

        return stringBuilder.ToString();
    }

    public (bool IsRevealed, bool IsFlagged, bool IsMine, bool IsDeathPlace, int NeighborMinesCount) GetCellState(int row, int column)
    {
        return _minefield.GetCellProperties(row, column);
    }

    private bool ValidateCellClick(int row, int column)
    {
        if (_view.IsGameOver)
        {
            return false;
        }

        if (_view.ElapsedSeconds == 0)
        {
            _view.StartTimer();
        }

        if (_minefield.IsRevealed(row, column))
        {
            return false;
        }

        return true;
    }

    public void OnCellLeftClickHandler(int row, int column)
    {
        if (!ValidateCellClick(row, column))
        {
            return;
        }

        if (_minefield.IsMine(row, column))
        {
            _minefield.SetDeathPlace(row, column);
            _view.SetGameOver();
        }
        else
        {
            _minefield.RevealChainReaction(row, column);
        }

        _view.InvalidatePlayingField();
    }

    public void OnCellRightClickHandler(int row, int column)
    {
        if (!ValidateCellClick(row, column))
        {
            return;
        }

        _minefield.SetFlagged(row, column);

        _view.SetMinesCount(_minefield.GetMinesLeft());
        _view.InvalidatePlayingField();
    }

    public void DifficultyChangeHandler(int newRowsCount, int newColumnsCount)
    {
        _minefield.GenerateNewMineField(newRowsCount, newColumnsCount);

# if DEBUG
        Console.WriteLine(_minefield);
#endif

        _view.SetMinesCount(_minefield.GetMinesLeft());
    }

    public void AllSafeRevealedCellsHandler()
    {
        _view.SetVictory();
    }

    public void Restart()
    {
        _minefield.GenerateNewMineField(_view.PlayingFieldRowsCount, _view.PlayingFieldColumnsCount);

#if DEBUG
        Console.WriteLine(_minefield);
#endif

        _view.SetMinesCount(_minefield.GetMinesLeft());
        _view.InvalidatePlayingField();
    }

    public string GetAboutInfo()
    {
        string json = File.ReadAllText(Path.Combine("..", "..", "..", "GUI", "Data", "about.json"));
        AboutInfo about = JsonSerializer.Deserialize<Dictionary<string, AboutInfo>>(json)!["About"];

        return about.ToString();
    }

    public void SaveRecord((string, int, Difficulty) recordInfo)
    {
        var (playerName, timeSeconds, difficulty) = recordInfo;

        _records.Add(new(
            playerName,
            timeSeconds,
            difficulty
        ));

        string json = JsonSerializer.Serialize(_records);
        File.WriteAllText(_recordsFilePath, json);
    }

    public void Run()
    {
        Application.Run((GameWindow)_view);
    }
}
