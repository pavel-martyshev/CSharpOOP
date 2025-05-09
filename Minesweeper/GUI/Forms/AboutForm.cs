namespace Minesweeper.GUI;

public partial class AboutForm : Form
{
    public AboutForm()
    {
        InitializeComponent();
    }

    public void SetAboutInfoLabelText(string text)
    {
        aboutInfoLable.Text = text;
    }
}
