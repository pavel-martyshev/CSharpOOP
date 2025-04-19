namespace TemperatureConverterTask.Model;

internal class Kelvin : ITemperatureScale
{
    public string Name => "Кельвин";

    public string Symbol => "°K";

    public double FromCelsius(double temperature) => temperature + 273.15;

    public double ToCelsius(double temperature) => temperature - 273.15;

    public override string ToString() => $"{Name} {Symbol}";
}
