namespace TemperatureConverterTask.Model.Scales;

internal class FahrenheitScale : ITemperatureScale
{
    public string Name => "Фаренгейт";

    public string Symbol => "°F";

    public double ConvertFromCelsius(double temperature) => temperature * 1.8 + 32;

    public double ConvertToCelsius(double temperature) => (temperature - 32) * (5.0 / 9);

    public override string ToString() => $"{Name} {Symbol}";
}
