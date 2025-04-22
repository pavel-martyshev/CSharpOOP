namespace Minesweeper;

partial class GameWindow
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameWindow));
        infoBar = new Panel();
        resetButton = new Button();
        timerLabel = new Label();
        minesCounterLabel = new Label();
        playingField = new Panel();
        menuBar = new FlowLayoutPanel();
        gameSettingsMenu = new MenuStrip();
        settingsToolStripMenuItem = new ToolStripMenuItem();
        newGameToolStripMenuItem = new ToolStripMenuItem();
        recordsToolStripMenuItem = new ToolStripMenuItem();
        difficultyToolStripMenuItem = new ToolStripMenuItem();
        easy9x9ToolStripMenuItem = new ToolStripMenuItem();
        medium16x16ToolStripMenuItem = new ToolStripMenuItem();
        hard30x16ToolStripMenuItem = new ToolStripMenuItem();
        aboutToolStripMenuItem = new ToolStripMenuItem();
        exitToolStripMenuItem = new ToolStripMenuItem();
        infoBar.SuspendLayout();
        menuBar.SuspendLayout();
        gameSettingsMenu.SuspendLayout();
        SuspendLayout();
        // 
        // infoBar
        // 
        infoBar.BackColor = Color.LightGray;
        infoBar.BorderStyle = BorderStyle.Fixed3D;
        infoBar.Controls.Add(resetButton);
        infoBar.Controls.Add(timerLabel);
        infoBar.Controls.Add(minesCounterLabel);
        infoBar.Location = new Point(6, 25);
        infoBar.Name = "infoBar";
        infoBar.Size = new Size(333, 50);
        infoBar.TabIndex = 1;
        // 
        // resetButton
        // 
        resetButton.BackColor = Color.DimGray;
        resetButton.FlatAppearance.BorderColor = Color.Silver;
        resetButton.FlatAppearance.BorderSize = 2;
        resetButton.FlatAppearance.MouseOverBackColor = Color.DarkGray;
        resetButton.FlatStyle = FlatStyle.Flat;
        resetButton.Image = (Image)resources.GetObject("resetButton.Image");
        resetButton.Location = new Point(147, 5);
        resetButton.Name = "resetButton";
        resetButton.Size = new Size(35, 35);
        resetButton.TabIndex = 2;
        resetButton.UseVisualStyleBackColor = false;
        resetButton.MouseClick += ResetButton_MouseClick;
        // 
        // timerLabel
        // 
        timerLabel.BackColor = Color.Black;
        timerLabel.Font = new Font("Neo Sans", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
        timerLabel.ForeColor = Color.Red;
        timerLabel.Location = new Point(251, 5);
        timerLabel.Name = "timerLabel";
        timerLabel.Size = new Size(75, 35);
        timerLabel.TabIndex = 1;
        timerLabel.Text = "000";
        timerLabel.TextAlign = ContentAlignment.MiddleRight;
        // 
        // minesCounterLabel
        // 
        minesCounterLabel.BackColor = Color.Black;
        minesCounterLabel.Font = new Font("Neo Sans", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
        minesCounterLabel.ForeColor = Color.Red;
        minesCounterLabel.Location = new Point(5, 5);
        minesCounterLabel.Name = "minesCounterLabel";
        minesCounterLabel.Size = new Size(75, 35);
        minesCounterLabel.TabIndex = 0;
        minesCounterLabel.Text = "000";
        minesCounterLabel.TextAlign = ContentAlignment.MiddleRight;
        // 
        // playingField
        // 
        playingField.BorderStyle = BorderStyle.Fixed3D;
        playingField.Location = new Point(6, 75);
        playingField.Margin = new Padding(0);
        playingField.Name = "playingField";
        playingField.Size = new Size(333, 204);
        playingField.TabIndex = 2;
        playingField.Paint += PlayingField_Paint;
        playingField.MouseDown += PlayingField_MouseDown;
        // 
        // menuBar
        // 
        menuBar.Controls.Add(gameSettingsMenu);
        menuBar.Location = new Point(6, 0);
        menuBar.Name = "menuBar";
        menuBar.Size = new Size(310, 25);
        menuBar.TabIndex = 3;
        // 
        // gameSettingsMenu
        // 
        gameSettingsMenu.GripStyle = ToolStripGripStyle.Visible;
        gameSettingsMenu.Items.AddRange(new ToolStripItem[] { settingsToolStripMenuItem });
        gameSettingsMenu.Location = new Point(0, 0);
        gameSettingsMenu.Name = "gameSettingsMenu";
        gameSettingsMenu.Size = new Size(182, 24);
        gameSettingsMenu.TabIndex = 0;
        gameSettingsMenu.Text = "Settings";
        // 
        // settingsToolStripMenuItem
        // 
        settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newGameToolStripMenuItem, recordsToolStripMenuItem, difficultyToolStripMenuItem, aboutToolStripMenuItem, exitToolStripMenuItem });
        settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
        settingsToolStripMenuItem.Size = new Size(50, 20);
        settingsToolStripMenuItem.Text = "Game";
        // 
        // newGameToolStripMenuItem
        // 
        newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
        newGameToolStripMenuItem.Size = new Size(180, 22);
        newGameToolStripMenuItem.Text = "New Game";
        newGameToolStripMenuItem.Click += NewGameToolStripMenuItem_Click;
        // 
        // recordsToolStripMenuItem
        // 
        recordsToolStripMenuItem.Name = "recordsToolStripMenuItem";
        recordsToolStripMenuItem.Size = new Size(180, 22);
        recordsToolStripMenuItem.Text = "High Scores";
        recordsToolStripMenuItem.Click += RecordsToolStripMenuItem_Click;
        // 
        // difficultyToolStripMenuItem
        // 
        difficultyToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { easy9x9ToolStripMenuItem, medium16x16ToolStripMenuItem, hard30x16ToolStripMenuItem });
        difficultyToolStripMenuItem.Name = "difficultyToolStripMenuItem";
        difficultyToolStripMenuItem.Size = new Size(180, 22);
        difficultyToolStripMenuItem.Text = "Difficulty";
        // 
        // easy9x9ToolStripMenuItem
        // 
        easy9x9ToolStripMenuItem.Name = "easy9x9ToolStripMenuItem";
        easy9x9ToolStripMenuItem.Size = new Size(159, 22);
        easy9x9ToolStripMenuItem.Text = "Easy (9x9)";
        // 
        // medium16x16ToolStripMenuItem
        // 
        medium16x16ToolStripMenuItem.Name = "medium16x16ToolStripMenuItem";
        medium16x16ToolStripMenuItem.Size = new Size(159, 22);
        medium16x16ToolStripMenuItem.Text = "Medium (16x16)";
        // 
        // hard30x16ToolStripMenuItem
        // 
        hard30x16ToolStripMenuItem.Name = "hard30x16ToolStripMenuItem";
        hard30x16ToolStripMenuItem.Size = new Size(159, 22);
        hard30x16ToolStripMenuItem.Text = "Hard (30x16";
        // 
        // aboutToolStripMenuItem
        // 
        aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
        aboutToolStripMenuItem.Size = new Size(180, 22);
        aboutToolStripMenuItem.Text = "About";
        aboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;
        // 
        // exitToolStripMenuItem
        // 
        exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        exitToolStripMenuItem.Size = new Size(180, 22);
        exitToolStripMenuItem.Text = "Exit";
        exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
        // 
        // GameWindow
        // 
        AutoScaleMode = AutoScaleMode.Inherit;
        ClientSize = new Size(334, 305);
        Controls.Add(menuBar);
        Controls.Add(playingField);
        Controls.Add(infoBar);
        Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Icon = (Icon)resources.GetObject("$this.Icon");
        MainMenuStrip = gameSettingsMenu;
        MaximizeBox = false;
        Name = "GameWindow";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Minesweeper";
        infoBar.ResumeLayout(false);
        menuBar.ResumeLayout(false);
        menuBar.PerformLayout();
        gameSettingsMenu.ResumeLayout(false);
        gameSettingsMenu.PerformLayout();
        ResumeLayout(false);
    }
    private Panel infoBar;
    private Panel playingField;
    private FlowLayoutPanel menuBar;
    private MenuStrip gameSettingsMenu;
    private ToolStripMenuItem settingsToolStripMenuItem;
    private ToolStripMenuItem difficultyToolStripMenuItem;
    private ToolStripMenuItem easy9x9ToolStripMenuItem;
    private ToolStripMenuItem medium16x16ToolStripMenuItem;
    private ToolStripMenuItem hard30x16ToolStripMenuItem;
    private Label minesCounterLabel;
    private Label timerLabel;
    private Button resetButton;
    private ToolStripMenuItem newGameToolStripMenuItem;
    private ToolStripMenuItem exitToolStripMenuItem;
    private ToolStripMenuItem aboutToolStripMenuItem;
    private ToolStripMenuItem recordsToolStripMenuItem;
}

#endregion
