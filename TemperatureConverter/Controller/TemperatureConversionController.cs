using TemperatureConverterTask.Model;

namespace TemperatureConverterTask.Controller;

class TemperatureConversionController : ITemperatureConversionController
{
    private readonly TemperatureConverterView _view;

    private readonly TemperatureScaleRegistry _temperatureScaleRegistry;

    public TemperatureConversionController(TemperatureConverterView view, TemperatureScaleRegistry temperatureScaleRegistry)
    {
        _view = view;
        _temperatureScaleRegistry = temperatureScaleRegistry;

        _view.InputScaleChanged += OnInputScaleChanged;
        _view.TemperatureConversionRequest += GetConvertedTemperature;

        _view.InitData(_temperatureScaleRegistry.Scales.Select(kvp => new KeyValuePair<TemperatureScale, string>(kvp.Key, kvp.Value.ToString()!)).ToDictionary());
    }

    public void OnInputScaleChanged(object selectedScale)
    {
        _view.SetConversionScalesData(_temperatureScaleRegistry.Scales
            .Select(kvp => new KeyValuePair<TemperatureScale, string>(kvp.Key, kvp.Value.ToString()!))
            .Where(kvp => kvp.Key != (TemperatureScale)selectedScale).ToDictionary());
    }

    public void GetConvertedTemperature(double inputTemperature)
    {
        var fromScale = _view.GetInputScaleValue();
        var toScale = _view.GetConversionScaleValue();

        var convertedTemperature = TemperatureConverter.Convert(inputTemperature, _temperatureScaleRegistry[fromScale], _temperatureScaleRegistry[toScale]);

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
