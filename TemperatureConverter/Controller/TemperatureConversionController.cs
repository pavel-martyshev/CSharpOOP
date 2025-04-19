using TemperatureConverterTask.Model;

namespace TemperatureConverterTask.Controller;

class TemperatureConversionController : ITemperatureConversionController
{
    private readonly TemperatureConverterView _view;

    private readonly Dictionary<TemperatureScale, ITemperatureScale> _scales = new()
    {
        { TemperatureScale.Celsius, new Celsius() },
        { TemperatureScale.Fahrenheit, new Fahrenheit() },
        { TemperatureScale.Kelvin, new Kelvin() },
    };

    public TemperatureConversionController(TemperatureConverterView view)
    {
        _view = view;

        _view.InputScaleChanged += OnInputScaleChanged;
        _view.InputTemperatureChanged += OnInputTemperatureChanged;

        _view.InitComboBoxesData(_scales.Select(kvp => new KeyValuePair<TemperatureScale, string>(kvp.Key, kvp.Value.ToString()!)).ToDictionary());
    }

    public void OnInputScaleChanged(object selectedScale)
    {
        _view.SetConversionScalesComboBoxData(_scales
            .Select(kvp => new KeyValuePair<TemperatureScale, string>(kvp.Key, kvp.Value.ToString()!))
            .Where(kvp => kvp.Key != (TemperatureScale)selectedScale).ToDictionary());
    }

    public void OnInputTemperatureChanged(double inputTemperature)
    {
        TemperatureScale fromScale = (TemperatureScale)_view.GetInputScaleValue();
        TemperatureScale toScale = (TemperatureScale)_view.GetConversionScaleValue();

        double convertedTemperature = TemperatureConverter.Convert(inputTemperature, _scales[fromScale], _scales[toScale]);

        _view.SetConvertedTemperature(convertedTemperature);
    }

    public void Run()
    {
        Application.Run(_view);
    }
}
