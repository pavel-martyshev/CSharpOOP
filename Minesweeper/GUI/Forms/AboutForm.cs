namespace Minesweeper.GUI;

public partial class AboutForm : Form
{
    public AboutForm()
    {
        InitializeComponent();
    }

    public void SetAboutInfoLableText(string text)
    {
        aboutInfoLable.Text = text;
    }
}
