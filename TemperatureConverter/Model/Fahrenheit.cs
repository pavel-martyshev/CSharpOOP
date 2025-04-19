namespace TemperatureConverterTask.Model;

internal class Fahrenheit : ITemperatureScale
{
    public string Name => "Фаренгейт";

    public string Symbol => "°F";

    public double FromCelsius(double temperature) => temperature * 1.8 + 32;

    public double ToCelsius(double temperature) => (temperature - 32) * 0.5556;

    public override string ToString() => $"{Name} {Symbol}";
}
