namespace TemperatureConverterTask.Model;

internal class TemperatureScaleRegistry
{
    private readonly Dictionary<TemperatureScale, ITemperatureScale> _scales = new()
    {
        { TemperatureScale.Celsius, new CelsiusScale() },
        { TemperatureScale.Fahrenheit, new FahrenheitScale() },
        { TemperatureScale.Kelvin, new KelvinScale() }
    };

    public Dictionary<TemperatureScale, ITemperatureScale> Scales { get { return _scales; } }

    public ITemperatureScale this[TemperatureScale scale]
    {
        get
        {
            return _scales[scale];
        }
    }
}
