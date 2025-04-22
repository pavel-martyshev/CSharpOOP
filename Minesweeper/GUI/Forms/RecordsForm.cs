namespace Minesweeper.GUI;

public partial class RecordsForm : Form
{
    public RecordsForm()
    {
        InitializeComponent();
    }

    public void SetRecordsLabelText(string text)
    {
        recordsLabel.Text = text;
    }
}
