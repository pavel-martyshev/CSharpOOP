using TemperatureConverterTask.Models;
using TemperatureConverterTask.Views;

namespace TemperatureConverterTask;

public partial class TemperatureConverterView : Form, ITemperatureConverterView
{
    public event EventHandler? InputScaleChanged;
    public event EventHandler? InputTemperatureChanged;

    public readonly List<TemperatureScaleItem> scales;

    public TemperatureConverterView()
    {
        InitializeComponent();

        // Initialize scales list for combo boxes
        scales =
        [
        new(TemperatureScale.Celsius, "Цельсий °C"),
        new(TemperatureScale.Fahrenheit, "Фаренгейт °F"),
        new(TemperatureScale.Kelvin, "Кельвин °K")
        ];

        // inputTemperatureSelectionComboBox setting
        inputScalesComboBox.DataSource = scales;
        inputScalesComboBox.DisplayMember = "DisplayName";
        inputScalesComboBox.ValueMember = "Scale";

        // conversionScalesComboBox setting
        conversionScalesComboBox.DataSource = scales.Skip(1).ToList();
        conversionScalesComboBox.DisplayMember = "DisplayName";
        conversionScalesComboBox.ValueMember = "Scale";

        // ActiveControl set to focus on input field 
        ActiveControl = inputTemperatureTextBox;
    }

    private void InputScalesComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        inputTemperatureTextBox.Focus();

        InputScaleChanged!.Invoke(sender, EventArgs.Empty);

        InputTemperatureTextBox_TextChanged(inputTemperatureTextBox, e);
    }

    private void ConversionScalesComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        inputTemperatureTextBox.Focus();

        InputTemperatureTextBox_TextChanged(inputTemperatureTextBox, e);
    }

    private void InputTemperatureTextBox_TextChanged(object sender, EventArgs e)
    {
        InputTemperatureChanged!.Invoke(sender, e);
    }

    public void SetConvertedTemperature(string convertedTemperature)
    {
        convertedTemperatureLabel.Text = convertedTemperature;
    }

    public void ShowError(string message)
    {
        MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
