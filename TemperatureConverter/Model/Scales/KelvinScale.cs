namespace TemperatureConverterTask.Model.Scales;

internal class KelvinScale : ITemperatureScale
{
    public string Name => "Кельвин";

    public string Symbol => "°K";

    public double ConvertFromCelsius(double temperature) => temperature + 273.15;

    public double ConvertToCelsius(double temperature) => temperature - 273.15;

    public override string ToString() => $"{Name} {Symbol}";
}
