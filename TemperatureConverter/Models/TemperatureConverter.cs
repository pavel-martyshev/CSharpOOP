namespace TemperatureConverterTask.Models;

public class TemperatureConverter : ITemperatureConverter
{
    public double GetFromCelsiusToFahrenheit(double celsiusDegrees)
    {
        return celsiusDegrees * 1.8 + 32;
    }

    public double GetFromCelsiusToKelvin(double celsiusDegrees)
    {
        return celsiusDegrees + 273.15;
    }

    public double GetFromFahrenheitToCelsius(double fahrenheitDegrees)
    {
        return (fahrenheitDegrees - 32) * 0.5556;
    }

    public double GetFromFahrenheitToKelvin(double fahrenheitDegrees)
    {
        return GetFromCelsiusToKelvin(GetFromFahrenheitToCelsius(fahrenheitDegrees));
    }

    public double GetFromKelvinToCelsius(double kelvinDegrees)
    {
        return kelvinDegrees - 273.15;
    }

    public double GetFromKelvinToFahrenheit(double kelvinDegrees)
    {
        return GetFromCelsiusToFahrenheit(GetFromKelvinToCelsius(kelvinDegrees));
    }
}
