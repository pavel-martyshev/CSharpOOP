using System.Text.RegularExpressions;

namespace TemperatureConverterTask.View;

public partial class TemperatureConverterView : Form, ITemperatureConverterView
{
    private const string _incorrectInputMessage =
        "Некорректный ввод температуры.\r\n\r\n" +
        "Разрешено:\r\n" +
        "  - Цифры\r\n" +  
        "  - Один минус в начале";

    public event Action<string>? InputScaleChanged;
    public event Action<double>? TemperatureConversionRequest;

    public TemperatureConverterView()
    {
        InitializeComponent();

        // ActiveControl set to focus on input field 
        ActiveControl = inputTemperatureTextBox;
    }

    private static List<TemperatureScaleItem> ConvertScalesToTemperatureScaleItems(Dictionary<string, string> scales)
    {
        return scales.Select(kvp => new TemperatureScaleItem(kvp.Key, kvp.Value)).ToList();
    }

    public void InitData(Dictionary<string, string> scales)
    {
        var temperatureScaleItems = ConvertScalesToTemperatureScaleItems(scales);

        inputScalesComboBox.DataSource = temperatureScaleItems;
        inputScalesComboBox.DisplayMember = "DisplayName";
        inputScalesComboBox.ValueMember = "Scale";

        conversionScalesComboBox.DataSource = temperatureScaleItems.Skip(1).ToList();
        conversionScalesComboBox.DisplayMember = "DisplayName";
        conversionScalesComboBox.ValueMember = "Scale";
    }

    public void SetConversionScalesData(Dictionary<string, string> scales)
    {
        conversionScalesComboBox.DataSource = ConvertScalesToTemperatureScaleItems(scales);
        conversionScalesComboBox.DisplayMember = "DisplayName";
        conversionScalesComboBox.ValueMember = "Scale";
    }

    private void InputScalesComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        inputTemperatureTextBox.Focus();
        inputTemperatureTextBox.SelectionStart = inputTemperatureTextBox.Text.Length;

        InputScaleChanged?.Invoke((string)inputScalesComboBox.SelectedValue!);

        InputTemperatureTextBox_TextChanged(inputTemperatureTextBox, e);
    }

    private void ConversionScalesComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        inputTemperatureTextBox.Focus();
        inputTemperatureTextBox.SelectionStart = inputTemperatureTextBox.Text.Length;

        if (inputTemperatureTextBox.Text != "")
        {
            try
            {
                TemperatureConversionRequest?.Invoke(double.Parse(inputTemperatureTextBox.Text));
            }
            catch (FormatException)
            {
                ShowError(_incorrectInputMessage);
            }
        }
    }

    private void InputTemperatureTextBox_TextChanged(object sender, EventArgs e)
    {
        var inputText = inputTemperatureTextBox.Text.Replace('.', ',').Trim();

        if (!Regex.IsMatch(inputText, @"^-?\d*,?\d*$"))
        {
            ShowError(_incorrectInputMessage);

            inputTemperatureTextBox.Text = inputText[..^1];
            inputTemperatureTextBox.SelectionStart = inputTemperatureTextBox.Text.Length;

            return;
        }

        if (inputText.Length == 0 || inputText == "-")
        {
            convertedTemperatureLabel.Text = "";
            return;
        }

        try
        {
            TemperatureConversionRequest?.Invoke(double.Parse(inputText));
        }
        catch (FormatException)
        {
            ShowError(_incorrectInputMessage);
        }
    }

    public string GetInputScale()
    {
        return (string)inputScalesComboBox.SelectedValue!;
    }

    public string GetConversionScale()
    {
        return (string)conversionScalesComboBox.SelectedValue!;
    }

    public void SetConvertedTemperature(double convertedTemperature)
    {
        convertedTemperatureLabel.Text = Math.Round(convertedTemperature, 2, MidpointRounding.AwayFromZero).ToString();
    }

    private static void ShowError(string message)
    {
        MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
