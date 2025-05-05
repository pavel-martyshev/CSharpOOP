namespace TemperatureConverterTask.View;

internal interface ITemperatureConverterView
{
    event Action<string>? InputScaleChanged;
    event Action<double>? TemperatureConversionRequest;

    void InitData(Dictionary<string, string> scales);

    void SetConversionScalesData(Dictionary<string, string> scales);

    void SetConvertedTemperature(double convertedTemperature);

    string GetInputScale();

    string GetConversionScale();
}
