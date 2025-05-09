using Minesweeper.Core.Enums;
using Minesweeper.Core.Interfaces;
using Minesweeper.GUI;

namespace Minesweeper;

public partial class GameWindow : Form, IMinesweeperView
{
    public int CellSideBaseSize { get; } = 30;

    private int _playingFieldRowsCount;
    private int _playingFieldColumnsCount;

    public bool IsGameOver { get; private set; }

    private int _playingFieldWidth;

    private int _playingFieldHeight;

    public event Func<int, int, ICell?>? RequestCell;
    public event Func<Difficulty, (int, int)>? RequestPlayingFieldSize;

    public event Func<string>? RequestAboutInfo;
    public event Func<string>? RequestRecordsString;

    public event Action? Restart;
    public event Action<int, int>? OnCellLeftClick;

    public event Action<int, int>? OnCellRightClick;
    public event Action<int, int>? OnDifficultyChange;

    public event Action<(string, int, Difficulty)>? SaveRecord;
    public event Action? TimerStopRequest;
    public event Action<int, int>? OnCellMiddleClick;

    private static readonly string BasePath = Path.Combine("..", "..", "..", "GUI", "Data");
    private static readonly Image FlagImage = Image.FromFile(Path.Combine(BasePath, "Flag.png"));

    private static readonly Image MineImage = Image.FromFile(Path.Combine(BasePath, "Mine.png"));
    private static readonly Image OrdinarySmileImage = Image.FromFile(Path.Combine(BasePath, "Ordinary.png"));

    private static readonly Image DeadSmileImage = Image.FromFile(Path.Combine(BasePath, "Dead.png"));
    private readonly Image HappySmileImage = Image.FromFile(Path.Combine(BasePath, "Happy.png"));

    private AboutForm? aboutForm;

    public GameWindow()
    {
        InitializeComponent();



        //SetFields();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        easy9x9ToolStripMenuItem.Checked = true;

        SetSize(0);
        SetFields();
    }

    private void SetFields()
    {
        // Enable double buffering
        typeof(Panel).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!.SetValue(playingField, true, null);

        SetGameWindow();

        SetGameSettingsMenu();

        SetInfoBar();

        SetPlayingField();
    }

    private void SetSize(Difficulty difficulty)
    {
        var size = RequestPlayingFieldSize?.Invoke(difficulty);

        if (size is (int rows, int columns))
        {
            _playingFieldRowsCount = rows;
            _playingFieldColumnsCount = columns;

            _playingFieldHeight = CellSideBaseSize * rows;
            _playingFieldWidth = CellSideBaseSize * columns;
        }
        else
        {
            MessageBox.Show("The size of the playing field is not set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Close();
        }
    }

    private void SetGameWindow()
    {
        ClientSize = new Size(_playingFieldWidth + 14, _playingFieldHeight + infoBar.Height + menuBar.Height + 10);
    }

    private void SetGameSettingsMenu()
    {
        easy9x9ToolStripMenuItem.Tag = Difficulty.Easy;
        medium16x16ToolStripMenuItem.Tag = Difficulty.Medium;
        hard30x16ToolStripMenuItem.Tag = Difficulty.Hard;

        easy9x9ToolStripMenuItem.Click += DifficultyItem_Click!;
        medium16x16ToolStripMenuItem.Click += DifficultyItem_Click!;
        hard30x16ToolStripMenuItem.Click += DifficultyItem_Click!;
    }

    private void SetInfoBar()
    {
        infoBar.Size = new Size(ClientSize.Width - 12, infoBar.Height);

        timerLabel.Location = new Point(infoBar.Width - timerLabel.Width - 10, 5);

        resetButton.Location = new Point(infoBar.Width / 2 - resetButton.Width / 2, 5);
    }

    private void SetPlayingField()
    {
        playingField.Size = new Size(_playingFieldWidth + 4, _playingFieldHeight + 4);
    }

    private static void ValidateDelegates(Delegate? callback, string name)
    {
        if (callback is null)
        {
            throw new InvalidOperationException($"{name} delegate is not assigned.");
        }
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
        var graphics = e.Graphics;

        for (var row = 0; row < _playingFieldRowsCount; row++)
        {
            for (var column = 0; column < _playingFieldColumnsCount; column++)
            {
                ValidateDelegates(RequestCell, nameof(RequestCell));

                int x = column * CellSideBaseSize;
                int y = row * CellSideBaseSize;

                var cellRectangle = new Rectangle(x, y, CellSideBaseSize, CellSideBaseSize);

                var cell = RequestCell!(row, column);

                if (cell is null)
                {
                    return;
                }

                var borderStyle = cell.IsRevealed ? Border3DStyle.SunkenOuter : Border3DStyle.Raised;

                graphics.FillRectangle(Brushes.LightGray, cellRectangle);
                ControlPaint.DrawBorder3D(graphics, cellRectangle, borderStyle);

                if (cell.NeighborMinesCount != 0)
                {
                    var text = cell.NeighborMinesCount.ToString();

                    var brush = GetBrushForNumber(cell.NeighborMinesCount);
                    var font = new Font("Consolas", CellSideBaseSize * 0.5f, FontStyle.Bold);
                    var textSize = graphics.MeasureString(text, font);

                    var textX = x + (CellSideBaseSize - textSize.Width) / 2;
                    var textY = y + (CellSideBaseSize - textSize.Height) / 2;

                    graphics.DrawString(text, font, brush, textX, textY);
                }

                if (cell.IsMine && cell.IsRevealed)
                {
                    if (cell.IsDeathPlace)
                    {
                        graphics.FillRectangle(Brushes.Firebrick, cellRectangle);
                    }

                    var mineRectangle = new Rectangle(
                        cellRectangle.X + 5,
                        cellRectangle.Y + 5,
                        cellRectangle.Width - 2 * 5,
                        cellRectangle.Height - 2 * 5
                    );

                    graphics.DrawImage(MineImage, mineRectangle);
                }

                if (cell.IsFlagged)
                {
                    var flagRectangle = new Rectangle(
                        cellRectangle.X + 5,
                        cellRectangle.Y + 5,
                        cellRectangle.Width - 2 * 5,
                        cellRectangle.Height - 2 * 5
                    );

                    graphics.DrawImage(FlagImage, flagRectangle);
                }
            }
        }
    }

    private void DifficultyItem_Click(object sender, EventArgs e)
    {
        var checkedItem = (ToolStripMenuItem)sender;

        if (!checkedItem.Checked)
        {
            easy9x9ToolStripMenuItem.Checked = false;
            medium16x16ToolStripMenuItem.Checked = false;
            hard30x16ToolStripMenuItem.Checked = false;

            checkedItem.Checked = true;

            RestartGame();
            SetSize((Difficulty)checkedItem.Tag!);

            SetFields();
            playingField.Invalidate();
        }
    }

    public void RedrawCell(int row, int column)
    {
        var cell = new Rectangle(column * CellSideBaseSize, row * CellSideBaseSize, CellSideBaseSize, CellSideBaseSize);
        playingField.Invalidate(cell);
    }

    public void InvalidatePlayingField()
    {
        SetFields();
        playingField.Invalidate();
    }

    public void SetMinesCount(int count)
    {
        minesCounterLabel.Text = count >= 100 ? count.ToString() : (count >= 10 ? $"0{count}" : $"00{count}");
    }

    private void PlayingField_MouseDown(object sender, MouseEventArgs e)
    {
        var y = e.Y / CellSideBaseSize;
        var x = e.X / CellSideBaseSize;

        if (e.Button == MouseButtons.Left)
        {
            OnCellLeftClick?.Invoke(y, x);
        }
        else if (e.Button == MouseButtons.Right)
        {
            OnCellRightClick?.Invoke(y, x);
        }
        else if (e.Button == MouseButtons.Middle)
        {
            OnCellMiddleClick?.Invoke(y, x);
        }
    }

    public void SetElapsedSeconds(int elapsedSecond)
    {
        if (elapsedSecond < 1000)
        {
            timerLabel.Text = elapsedSecond > 99 ? elapsedSecond.ToString() : (elapsedSecond > 9 ? "0" : "00") + elapsedSecond.ToString();
        }
    }

    public void SetGameOver()
    {
        IsGameOver = true;

        resetButton.Image = DeadSmileImage;
    }

    public void SetVictory(int elapsedSeconds)
    {
        IsGameOver = true;

        resetButton.Image = HappySmileImage;

        playingField.Invalidate();

        var saveRecordForm = new SaveRecordForm();
        var result = saveRecordForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            var difficulty = _playingFieldColumnsCount == 30 ? Difficulty.Hard : (_playingFieldColumnsCount == 16 ? Difficulty.Medium : Difficulty.Easy);
            SaveRecord?.Invoke((saveRecordForm.PlayerName, elapsedSeconds, difficulty));
        }
    }

    private void RestartGame()
    {
        timerLabel.Text = "000";

        resetButton.Image = OrdinarySmileImage;

        IsGameOver = false;

        TimerStopRequest?.Invoke();
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
        ValidateDelegates(RequestAboutInfo, nameof(RequestAboutInfo));

        string aboutInfo = RequestAboutInfo!();

        aboutForm = new();

        aboutForm.SetAboutInfoLabelText(aboutInfo);
        aboutForm.Show();
    }

    private void RecordsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ValidateDelegates(RequestRecordsString, nameof(RequestRecordsString));

        string recordsText = RequestRecordsString!();

        MessageBox.Show(recordsText, "Records", MessageBoxButtons.OK);
    }
}
