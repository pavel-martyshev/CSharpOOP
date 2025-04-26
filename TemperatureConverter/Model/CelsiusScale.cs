namespace TemperatureConverterTask.Model;

internal class CelsiusScale : ITemperatureScale
{
    public string Name => "Цельсий";

    public string Symbol => "°C";

    public double ConvertFromCelsius(double temperature) => temperature;

    public double ConvertToCelsius(double temperature) => temperature;

    public override string ToString() => $"{Name} {Symbol}";
}
