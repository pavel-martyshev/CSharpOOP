namespace TemperatureConverterTask.Views;

internal interface ITemperatureConverterView
{
    event EventHandler InputScaleChanged;
    event EventHandler InputTemperatureChanged;

    void SetConvertedTemperature(double value);

    void ShowError(string message);
}
