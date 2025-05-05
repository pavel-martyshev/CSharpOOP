namespace TemperatureConverterTask.Model;

public interface ITemperatureScale
{
    string Name { get; }

    string Symbol { get; }

    double ConvertToCelsius(double temperature);

    double ConvertFromCelsius(double temperature);

    string ToString();
}
