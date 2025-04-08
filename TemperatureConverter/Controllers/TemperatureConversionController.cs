using System.Text.RegularExpressions;
using TemperatureConverterTask.Models;

namespace TemperatureConverterTask.Controllers;

class TemperatureConversionController : ITemperatureConversionController
{
    private readonly TemperatureConverterView _view;

    public TemperatureConversionController(TemperatureConverterView view)
    {
        _view = view;

        _view.InputScaleChanged += OnInputScaleChanged;
        _view.InputTemperatureChanged += OnInputTemperatureChanged;
    }

    private static void ValidateSender(object? sender)
    {
        ArgumentNullException.ThrowIfNull(sender, nameof(sender));
    }

    public void OnInputScaleChanged(object? sender, EventArgs e)
    {
        ValidateSender(sender);

        ComboBox senderComboBox = (ComboBox)sender!;

        _view.conversionScalesComboBox.DataSource = _view.scales.Where(s => s.Scale != (TemperatureScale)senderComboBox.SelectedValue!).ToList();
    }

    public void OnInputTemperatureChanged(object? sender, EventArgs e)
    {
        ValidateSender(sender);

        TextBox inputTemperatureTextBox = (TextBox)sender!;
        string inputText = inputTemperatureTextBox.Text.Replace('.', ',').Trim();

        if (!Regex.IsMatch(inputText, @"^-?\d*,?\d*$"))
        {
            _view.ShowError($"Некорректный ввод температуры.{Environment.NewLine}{Environment.NewLine}Разрешено:{Environment.NewLine}- Цифры{Environment.NewLine}- Один минус в начале");
            inputTemperatureTextBox.Text = "";
            return;
        }

        if (inputText.Length == 0 || inputText == "-")
        {
            _view.convertedTemperatureLabel.Text = "";
            return;
        }

        TemperatureScale fromScale = (TemperatureScale)_view.inputScalesComboBox.SelectedValue!;
        TemperatureScale toScale = (TemperatureScale)_view.conversionScalesComboBox.SelectedValue!;

        double convertedTemperature = Math.Round(TemperatureConverter.Convert(double.Parse(inputText), fromScale, toScale), 2, MidpointRounding.AwayFromZero);

        _view.convertedTemperatureLabel.Text = convertedTemperature.ToString();
    }

    public void Run()
    {
        Application.Run(_view);
    }
}
