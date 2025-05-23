﻿using Minesweeper.Core.Enums;
using Minesweeper.Core.Interfaces;
using Minesweeper.Game.Model;
using System.Text.Json;

namespace Minesweeper.Presenter;

internal class GamePresenter : IGamePresenter
{
    private readonly IMinesweeperView _view;

    private readonly IMineField _minefield;

    private readonly IGameTimer _gameTimer;

    public GamePresenter(IMinesweeperView view, IMineField minefield, IGameTimer gameTimer)
    {
        _view = view;
        _minefield = minefield;
        _gameTimer = gameTimer;

        _view.SetMinesCount(_minefield.GetMinesLeft());

        _view.RequestCell += GetCell;
        _view.RequestPlayingFieldSize += GetPlayingFieldSize;

        _view.OnCellLeftClick += OnCellLeftClickHandler;
        _view.OnCellRightClick += OnCellRightClickHandler;

        _view.OnDifficultyChange += DifficultyChangeHandler;
        _view.Restart += Restart;

        _view.RequestAboutInfo += GetAboutInfo;
        _view.TimerStopRequest += _gameTimer.Stop;

        _view.OnCellMiddleClick += OnCellMiddleClickHandler;

        _minefield.AllSafeCellsRevealed += AllSafeRevealedCellsHandler;
        _minefield.OnMineStepped += OnMineSteppedHandler;

        _gameTimer.Tick += OnTimerTick;
    }

    public ICell GetCell(int row, int column)
    {
        var cell = _minefield.GetCell(row, column);

        if (cell is null)
        {
            return new Cell(false, false, false, 0, false);
        }

        return cell;
    }

    private bool ValidateCellClick(int row, int column)
    {
        if (_view.IsGameOver || _minefield.IsRevealed(row, column))
        {
            return false;
        }

        if (_gameTimer.ElapsedSeconds == 0)
        {
            _gameTimer.Start();
        }

        return true;
    }

    public (int, int) GetPlayingFieldSize(Difficulty difficulty)
    {
        _minefield.UpdateSizeByDifficulty(difficulty);
        return (_minefield.RowsCount, _minefield.ColumnsCount);
    }

    public void OnCellLeftClickHandler(int row, int column)
    {
        if (!ValidateCellClick(row, column) || _minefield.IsFlagged(row, column))
        {
            return;
        }

        if (!_minefield.IsGenerated)
        {
            _minefield.GenerateNewMineField(_minefield.RowsCount, _minefield.ColumnsCount, row, column);
        }

        if (_minefield.IsMine(row, column))
        {
            _gameTimer.Stop();
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

    public void OnCellMiddleClickHandler(int row, int column)
    {
        if (!_view.IsGameOver && _minefield.IsChordingPossible(row, column))
        {
            _minefield.Chording(row, column);
            _view.InvalidatePlayingField();
        }
    }

    public void OnMineSteppedHandler()
    {
        _gameTimer.Stop();
        _view.SetGameOver();
    }

    public void DifficultyChangeHandler(int newRowsCount, int newColumnsCount)
    {
        _gameTimer.Stop();
        _view.SetMinesCount(_minefield.GetMinesLeft());
    }

    public void AllSafeRevealedCellsHandler()
    {
        _gameTimer.Stop();
        _view.SetVictory(_gameTimer.ElapsedSeconds);
    }

    public void OnTimerTick(int elapsedSeconds)
    {
        _view.SetElapsedSeconds(elapsedSeconds);
    }

    public void Restart()
    {
        _gameTimer.ElapsedSeconds = 0;
        _minefield.ResetMineField();

        _view.SetMinesCount(_minefield.GetMinesLeft());
        _view.InvalidatePlayingField();
    }

    public string GetAboutInfo()
    {
        var json = File.ReadAllText(Path.Combine("..", "..", "..", "GUI", "Data", "about.json"));
        var about = JsonSerializer.Deserialize<Dictionary<string, AboutInfo>>(json)!["About"];

        return about.ToString();
    }

    public void Run()
    {
        Application.Run((GameWindow)_view);
    }
}
