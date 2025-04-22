namespace Minesweeper.GUI;

public partial class SaveRecordForm : Form
{
    public string PlayerName { get; private set; } = "player";

    public SaveRecordForm()
    {
        InitializeComponent();
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        if (nameTextBox.Text.Length != 0)
        {
            PlayerName = nameTextBox.Text;
        }

        DialogResult = DialogResult.OK;
        Close();
    }
}
