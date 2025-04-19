using TemperatureConverterTask.Model;

namespace TemperatureConverterTask.View;

internal interface ITemperatureConverterView
{
    event Action<object>? InputScaleChanged;
    event Action<double>? InputTemperatureChanged;

    void InitComboBoxesData(Dictionary<TemperatureScale, string> scales);

    void SetConversionScalesComboBoxData(Dictionary<TemperatureScale, string> scales);

    void SetConvertedTemperature(double convertedTemperature);

    object GetInputScaleValue();

    object GetConversionScaleValue();
}
