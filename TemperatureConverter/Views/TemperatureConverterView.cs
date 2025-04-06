using System.Text.RegularExpressions;
using TemperatureConverterTask.Controllers;
using TemperatureConverterTask.Models;
using TemperatureConverterTask.Views;

namespace TemperatureConverterTask;

public partial class TemperatureConverterView : Form
{
    public readonly List<TemperatureScaleItem> scales =
    [
        new(TemperatureScale.Celsius, "Цельсий °C"),
        new(TemperatureScale.Fahrenheit, "Фаренгейт °F"),
        new(TemperatureScale.Kelvin, "Кельвин °K")
    ];

    public TemperatureConverterView()
    {
        InitializeComponent();

        TemperatureConversionController controller = new(new TemperatureConverter(), this);

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

    private static void ShowError(string message)
    {
        MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    private void InputScalesComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        inputTemperatureTextBox.Focus();
        //ComboBox senderComboBox = (ComboBox)sender;

        //conversionScalesComboBox.DataSource = scales.Where(s => s.Scale != (TemperatureScale)senderComboBox.SelectedValue!).ToList();
        controller

        InputTemperatureTextBox_TextChanged(inputTemperatureTextBox, e);
    }

    private void ConversionScalesComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        inputTemperatureTextBox.Focus();

        InputTemperatureTextBox_TextChanged(inputTemperatureTextBox, e);
    }

    private void InputTemperatureTextBox_TextChanged(object sender, EventArgs e)
    {
        TextBox inputTemperatureTextBox = (TextBox)sender;
        string inputText = inputTemperatureTextBox.Text.Replace('.', ',').Trim();

        if (!Regex.IsMatch(inputText, @"^-?\d*,?\d*$"))
        {
            ShowError($"Некорректный ввод температуры.{Environment.NewLine}{Environment.NewLine}Разрешено:{Environment.NewLine}- Цифры{Environment.NewLine}- Один минус в начале");
            inputTemperatureTextBox.Text = "";
        }
        else if (inputText.Length == 0 || inputText == "-")
        {
            convertedTemperatureLabel.Text = "";
        }
        else
        {
            int selectedInputKey = (int)inputScalesComboBox.SelectedValue!;
            int selectedConversionKey = (int)conversionScalesComboBox.SelectedValue!;

            ITemperatureConverter converter = new TemperatureConverter();

            double inputDegrees = double.Parse(inputText);
            double convertedTemperature;

            if (selectedInputKey == 0)
            {
                if (selectedConversionKey == 1)
                {
                    convertedTemperature = Math.Round(converter.GetFromCelsiusToFahrenheit(inputDegrees), 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    convertedTemperature = Math.Round(converter.GetFromCelsiusToKelvin(inputDegrees), 2, MidpointRounding.AwayFromZero);
                }

                convertedTemperatureLabel.Text = $"{convertedTemperature}";
            }
            else if (selectedInputKey == 1)
            {
                if (selectedConversionKey == 0)
                {
                    convertedTemperature = Math.Round(converter.GetFromFahrenheitToCelsius(inputDegrees), 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    convertedTemperature = Math.Round(converter.GetFromFahrenheitToKelvin(inputDegrees), 2, MidpointRounding.AwayFromZero);
                }

                convertedTemperatureLabel.Text = $"{convertedTemperature}";
            }
            else if (selectedInputKey == 2)
            {
                if (selectedConversionKey == 0)
                {
                    convertedTemperature = Math.Round(converter.GetFromKelvinToCelsius(inputDegrees), 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    convertedTemperature = Math.Round(converter.GetFromKelvinToFahrenheit(inputDegrees), 2, MidpointRounding.AwayFromZero);
                }

                convertedTemperatureLabel.Text = $"{convertedTemperature}";
            }
        }
    }
}
