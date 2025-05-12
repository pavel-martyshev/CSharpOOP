using TemperatureConverterTask.Model;
using TemperatureConverterTask.View;

namespace TemperatureConverterTask.Controller;

class TemperatureConversionController : ITemperatureConversionController
{
    private readonly TemperatureConverterView _view;

    private readonly TemperatureScalesRegistry _temperatureScalesRegistry;

    public TemperatureConversionController(TemperatureConverterView view, TemperatureScalesRegistry temperatureScalesRegistry)
    {
        _view = view;
        _temperatureScalesRegistry = temperatureScalesRegistry;

        _view.InputScaleChanged += OnInputScaleChanged;
        _view.TemperatureConversionRequest += GetConvertedTemperature;

        _view.InitData(_temperatureScalesRegistry.Scales.Select(kvp => new KeyValuePair<string, string>(kvp.Key, kvp.Value.ToString()!)).ToDictionary());
    }

    public void OnInputScaleChanged(string selectedScale)
    {
        _view.SetConversionScalesData(_temperatureScalesRegistry.Scales
            .Where(kvp => kvp.Key != selectedScale)
            .Select(kvp => new KeyValuePair<string, string>(kvp.Key, kvp.Value.ToString()!))
            .ToDictionary());
    }

    public void GetConvertedTemperature(double inputTemperature)
    {
        var fromScale = _view.GetInputScale();
        var toScale = _view.GetConversionScale();

        var convertedTemperature = TemperatureConverter.Convert(inputTemperature, _temperatureScalesRegistry[fromScale], _temperatureScalesRegistry[toScale]);

        if (convertedTemperature == -0)
        {
            convertedTemperature = 0;
        }

        _view.SetConvertedTemperature(convertedTemperature);
    }

    public void Run()
    {
        Application.Run(_view);
    }
}
