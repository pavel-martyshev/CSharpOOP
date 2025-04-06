namespace TemperatureConverterTask.Models;

interface ITemperatureConverter
{
    public double GetFromCelsiusToFahrenheit(double celsiusDegrees);

    public double GetFromCelsiusToKelvin(double celsiusDegrees);

    public double GetFromFahrenheitToCelsius(double fahrenheitDegrees);

    public double GetFromFahrenheitToKelvin(double fahrenheitDegrees);

    public double GetFromKelvinToCelsius(double kelvinDegrees);

    public double GetFromKelvinToFahrenheit(double kelvinDegrees);
}
