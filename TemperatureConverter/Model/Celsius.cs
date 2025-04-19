namespace TemperatureConverterTask.Model;

internal class Celsius : ITemperatureScale
{
    public string Name => "Цельсий";

    public string Symbol => "°C";

    public double FromCelsius(double temperature) => temperature;

    public double ToCelsius(double temperature) => temperature;

    public override string ToString() => $"{Name} {Symbol}";
}
