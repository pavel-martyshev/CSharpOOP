namespace TemperatureConverterTask.Model;

public interface ITemperatureScale
{
    string Name { get; }

    string Symbol { get; }

    double ToCelsius(double temperature);

    double FromCelsius(double temperature);
}
