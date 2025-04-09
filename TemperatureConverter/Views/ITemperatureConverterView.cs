namespace TemperatureConverterTask.Views;

internal interface ITemperatureConverterView
{
    event EventHandler InputScaleChanged;
    event EventHandler InputTemperatureChanged;

    void SetConvertedTemperature(string convertedTemperature);

    void ShowError(string message);
}
