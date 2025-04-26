using System.Text.RegularExpressions;
using TemperatureConverterTask.Model;
using TemperatureConverterTask.View;

namespace TemperatureConverterTask;

public partial class TemperatureConverterView : Form, ITemperatureConverterView
{
    public event Action<object>? InputScaleChanged;
    public event Action<double>? TemperatureConversionRequest;

    public TemperatureConverterView()
    {
        InitializeComponent();

        // ActiveControl set to focus on input field 
        ActiveControl = inputTemperatureTextBox;
    }

    private static List<TemperatureScaleItem> ConvertScalesToTemperatureScaleItems(Dictionary<TemperatureScale, string> scales)
    {
        return scales.Select(kvp => new TemperatureScaleItem(kvp.Key, kvp.Value)).ToList();
    }

    public void InitData(Dictionary<TemperatureScale, string> scales)
    {
        var temperatureScaleItems = ConvertScalesToTemperatureScaleItems(scales);

        inputScalesComboBox.DataSource = temperatureScaleItems;
        inputScalesComboBox.DisplayMember = "DisplayName";
        inputScalesComboBox.ValueMember = "Scale";

        conversionScalesComboBox.DataSource = temperatureScaleItems.Skip(1).ToList();
        conversionScalesComboBox.DisplayMember = "DisplayName";
        conversionScalesComboBox.ValueMember = "Scale";
    }

    public void SetConversionScalesData(Dictionary<TemperatureScale, string> scales)
    {
        conversionScalesComboBox.DataSource = ConvertScalesToTemperatureScaleItems(scales);
        conversionScalesComboBox.DisplayMember = "DisplayName";
        conversionScalesComboBox.ValueMember = "Scale";
    }

    private void InputScalesComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        inputTemperatureTextBox.Focus();
        inputTemperatureTextBox.SelectionStart = inputTemperatureTextBox.Text.Length;

        InputScaleChanged?.Invoke(inputScalesComboBox.SelectedValue!);

        InputTemperatureTextBox_TextChanged(inputTemperatureTextBox, e);
    }

    private void ConversionScalesComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        inputTemperatureTextBox.Focus();
        inputTemperatureTextBox.SelectionStart = inputTemperatureTextBox.Text.Length;

        if (inputTemperatureTextBox.Text != "")
        {
            TemperatureConversionRequest?.Invoke(double.Parse(inputTemperatureTextBox.Text));
        }
    }

    private void InputTemperatureTextBox_TextChanged(object sender, EventArgs e)
    {
        var inputText = inputTemperatureTextBox.Text.Replace('.', ',').Trim();

        if (!Regex.IsMatch(inputText, @"^-?\d*,?\d*$"))
        {
            ShowError($"Некорректный ввод температуры.{Environment.NewLine}{Environment.NewLine}Разрешено:{Environment.NewLine}- Цифры{Environment.NewLine}- Один минус в начале");

            inputTemperatureTextBox.Text = inputText[..^1];
            inputTemperatureTextBox.SelectionStart = inputTemperatureTextBox.Text.Length;

            return;
        }

        if (inputText.Length == 0 || inputText == "-")
        {
            convertedTemperatureLabel.Text = "";
            return;
        }

        TemperatureConversionRequest?.Invoke(double.Parse(inputText));
    }

    public TemperatureScale GetInputScaleValue()
    {
        return (TemperatureScale)inputScalesComboBox.SelectedValue!;
    }

    public TemperatureScale GetConversionScaleValue()
    {
        return (TemperatureScale)conversionScalesComboBox.SelectedValue!;
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
