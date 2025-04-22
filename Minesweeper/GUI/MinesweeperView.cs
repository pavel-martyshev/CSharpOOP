using Minesweeper.Core.Enums;
using Minesweeper.Core.Interfaces;
using Minesweeper.GUI;

namespace Minesweeper;

public partial class GameWindow : Form, IMinesweeperView
{
    // playingField settings
    public int CellSideBaseSize { get; } = 30;

    public int PlayingFieldRowsCount { get; private set; } = 9;

    public int PlayingFieldColumnsCount { get; private set; } = 9;

    public int ElapsedSeconds { get; private set; }

    public bool IsGameOver { get; private set; }

    private int PlayingFieldWidth => CellSideBaseSize * PlayingFieldColumnsCount;

    private int PlayingFieldHeight => CellSideBaseSize * PlayingFieldRowsCount;

    public event Func<int, int, (bool IsRevealed, bool IsFlagged, bool IsMine, bool IsDeathPlace, int NeighborMinesCount)>? RequestCellState;
    public event Action? Restart;

    public event Action<int, int>? OnCellLeftClick;
    public event Action<int, int>? OnCellRightClick;

    public event Action<int, int>? OnDifficultyChange;
    public event Action<(string, int, Difficulty)>? SaveRecord;

    public event Func<string>? RequestAboutInfo;
    public event Func<string>? RequestRecordsString;

    private static readonly string _basePath = Path.Combine("..", "..", "..", "GUI", "Data");

    private readonly string _flagImagePath = Path.Combine(_basePath, "Flag.png");
    private readonly Image _flagImage;

    private readonly string _mineImagePath = Path.Combine(_basePath, "Mine.png");
    private readonly Image _mineImage;

    private readonly string _ordinarySmileImagePath = Path.Combine(_basePath, "Ordinary.png");
    private readonly Image _ordinarySmileImage;

    private readonly string _deadSmileImagePath = Path.Combine(_basePath, "Dead.png");
    private readonly Image _deadSmileImage;

    private readonly string _happySmileImagePath = Path.Combine(_basePath, "Happy.png");
    private readonly Image _happySmileImage;

    private readonly System.Windows.Forms.Timer _timer = new();

    private AboutForm? aboutForm;

    public GameWindow()
    {
        InitializeComponent();

        easy9x9ToolStripMenuItem.Checked = true;

        _flagImage = Image.FromFile(_flagImagePath);
        _mineImage = Image.FromFile(_mineImagePath);

        _ordinarySmileImage = Image.FromFile(_ordinarySmileImagePath);
        _deadSmileImage = Image.FromFile(_deadSmileImagePath);
        _happySmileImage = Image.FromFile(_happySmileImagePath);

        _timer.Tick += Timer_Tick;

        SettingFields();
    }

    private void SettingFields()
    {
        SettingGameWindow();

        SettingGameSettingsMenu();

        SettingInfoBar();

        SettingPlayingField();
    }

    private void SettingGameWindow()
    {
        ClientSize = new Size(PlayingFieldWidth + 14, PlayingFieldHeight + infoBar.Height + menuBar.Height + 10);
    }

    private void SettingGameSettingsMenu()
    {
        easy9x9ToolStripMenuItem.Tag = Difficulty.Easy;
        medium16x16ToolStripMenuItem.Tag = Difficulty.Medium;
        hard30x16ToolStripMenuItem.Tag = Difficulty.Hard;

        easy9x9ToolStripMenuItem.Click += DifficultyItem_Click!;
        medium16x16ToolStripMenuItem.Click += DifficultyItem_Click!;
        hard30x16ToolStripMenuItem.Click += DifficultyItem_Click!;
    }

    private void SettingInfoBar()
    {
        infoBar.Size = new Size(ClientSize.Width - 12, infoBar.Height);

        timerLabel.Location = new Point(infoBar.Width - timerLabel.Width - 10, 5);

        resetButton.Location = new Point(infoBar.Width / 2 - resetButton.Width / 2, 5);

        _timer.Interval = 1000;
    }

    private void SettingPlayingField()
    {
        playingField.Size = new Size(PlayingFieldWidth + 4, PlayingFieldHeight + 4);
    }

    private static Brush GetBrushForNumber(int number)
    {
        if (number < 0 || number > 8)
        {
            return Brushes.Black;
        }

        Brush[] brushesList =
        [
            Brushes.Blue,
            Brushes.Green,
            Brushes.Red,
            Brushes.DarkBlue,
            Brushes.DarkRed,
            Brushes.Teal,
            Brushes.Black,
            Brushes.Gray
        ];

        return brushesList[number - 1];
    }

    private void PlayingField_Paint(object sender, PaintEventArgs e)
    {
        Graphics graphics = e.Graphics;

        for (int row = 0; row < PlayingFieldRowsCount; row++)
        {
            for (int column = 0; column < PlayingFieldColumnsCount; column++)
            {
                int x = column * CellSideBaseSize;
                int y = row * CellSideBaseSize;

                Rectangle cellRectangle = new(x, y, CellSideBaseSize, CellSideBaseSize);

                var (isRevealed, isFlagged, isMine, isDeathPlace, neighborMinesCount) = RequestCellState!(row, column);

                var borderStyle = isRevealed ? Border3DStyle.SunkenOuter : Border3DStyle.Raised;

                graphics.FillRectangle(Brushes.LightGray, cellRectangle);
                ControlPaint.DrawBorder3D(graphics, cellRectangle, borderStyle);

                if (neighborMinesCount != 0)
                {
                    string text = neighborMinesCount.ToString();

                    Brush brush = GetBrushForNumber(neighborMinesCount);
                    Font font = new("Consolas", CellSideBaseSize * 0.5f, FontStyle.Bold);
                    SizeF textSize = graphics.MeasureString(text, font);

                    float textX = x + (CellSideBaseSize - textSize.Width) / 2;
                    float textY = y + (CellSideBaseSize - textSize.Height) / 2;

                    graphics.DrawString(text, font, brush, textX, textY);
                }

                if (isMine && isRevealed)
                {
                    if (isDeathPlace)
                    {
                        graphics.FillRectangle(Brushes.Firebrick, cellRectangle);
                    }

                    Rectangle mineRectangle = new(
                        cellRectangle.X + 5,
                        cellRectangle.Y + 5,
                        cellRectangle.Width - 2 * 5,
                        cellRectangle.Height - 2 * 5
                    );

                    graphics.DrawImage(_mineImage, mineRectangle);
                }

                if (isFlagged)
                {
                    Rectangle flagRectangle = new(
                        cellRectangle.X + 5,
                        cellRectangle.Y + 5,
                        cellRectangle.Width - 2 * 5,
                        cellRectangle.Height - 2 * 5
                    );

                    graphics.DrawImage(_flagImage, flagRectangle);
                }
            }
        }
    }

    private void UpdateDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                PlayingFieldRowsCount = 9;
                PlayingFieldColumnsCount = 9;
                break;
            case Difficulty.Medium:
                PlayingFieldRowsCount = 16;
                PlayingFieldColumnsCount = 16;
                break;
            case Difficulty.Hard:
                PlayingFieldRowsCount = 16;
                PlayingFieldColumnsCount = 30;
                break;
        }
    }

    private void DifficultyItem_Click(object sender, EventArgs e)
    {
        ToolStripMenuItem checkedItem = (ToolStripMenuItem)sender;

        if (!checkedItem.Checked)
        {
            easy9x9ToolStripMenuItem.Checked = false;
            medium16x16ToolStripMenuItem.Checked = false;
            hard30x16ToolStripMenuItem.Checked = false;

            UpdateDifficulty((Difficulty)checkedItem.Tag!);

            checkedItem.Checked = true;

            OnDifficultyChange?.Invoke(PlayingFieldRowsCount, PlayingFieldColumnsCount);

            SettingFields();
            playingField.Invalidate();
        }
    }

    public void RedrawCell(int row, int column)
    {
        Rectangle cell = new(column * CellSideBaseSize, row * CellSideBaseSize, CellSideBaseSize, CellSideBaseSize);
        playingField.Invalidate(cell);
    }

    public void InvalidatePlayingField()
    {
        SettingFields();
        playingField.Invalidate();
    }

    public void SetMinesCount(int count)
    {
        minesCounterLabel.Text = count >= 100 ? count.ToString() : (count >= 10 ? $"0{count}" : $"00{count}");
    }

    private void PlayingField_MouseDown(object sender, MouseEventArgs e)
    {
        int y = e.Y / CellSideBaseSize;
        int x = e.X / CellSideBaseSize;

        if (e.Button == MouseButtons.Left)
        {
            OnCellLeftClick?.Invoke(y, x);
        }
        else if (e.Button == MouseButtons.Right)
        {
            OnCellRightClick?.Invoke(y, x);
        }
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        if (ElapsedSeconds != 999)
        {
            ElapsedSeconds++;
            timerLabel.Text = ElapsedSeconds.ToString("D3");
        }
    }

    public void StartTimer() => _timer.Start();

    public void SetGameOver()
    {
        _timer.Stop();

        IsGameOver = true;

        resetButton.Image = _deadSmileImage;
    }

    public void SetVictory()
    {
        _timer.Stop();

        IsGameOver = true;

        resetButton.Image = _happySmileImage;

        playingField.Invalidate();

        SaveRecordForm saveRecordForm = new();
        var result = saveRecordForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            Difficulty difficulty = PlayingFieldColumnsCount == 30 ? Difficulty.Hard : (PlayingFieldColumnsCount == 16 ? Difficulty.Medium : Difficulty.Easy);
            SaveRecord?.Invoke((saveRecordForm.PlayerName, ElapsedSeconds, difficulty));
        }
    }

    private void ResetTimer()
    {
        _timer.Stop();

        ElapsedSeconds = 0;
        timerLabel.Text = "000";
    }

    private void RestartGame()
    {
        ResetTimer();

        resetButton.Image = _ordinarySmileImage;

        IsGameOver = false;

        Restart?.Invoke();
    }

    private void ResetButton_MouseClick(object sender, MouseEventArgs e)
    {
        RestartGame();
    }

    private void NewGameToolStripMenuItem_Click(object sender, EventArgs e)
    {
        RestartGame();
    }

    private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        string aboutInfo = RequestAboutInfo!();

        aboutForm = new();

        aboutForm.SetAboutInfoLableText(aboutInfo);
        aboutForm.Show();
    }

    private void RecordsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        string recordsText = RequestRecordsString!();

        RecordsForm recordsForm = new();

        recordsForm.SetRecordsLabelText(recordsText);
        recordsForm.Show();
    }
}
