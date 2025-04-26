using TemperatureConverterTask.Model;

namespace TemperatureConverterTask.View;

internal interface ITemperatureConverterView
{
    event Action<object>? InputScaleChanged;
    event Action<double>? TemperatureConversionRequest;

    void InitData(Dictionary<TemperatureScale, string> scales);

    void SetConversionScalesData(Dictionary<TemperatureScale, string> scales);

    void SetConvertedTemperature(double convertedTemperature);

    TemperatureScale GetInputScaleValue();

    TemperatureScale GetConversionScaleValue();
}
