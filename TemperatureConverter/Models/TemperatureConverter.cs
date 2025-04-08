namespace TemperatureConverterTask.Models;

public static class TemperatureConverter
{
    public static double Convert(double inputTemperature, TemperatureScale fromScale, TemperatureScale toScale)
    {
        if (fromScale == TemperatureScale.Celsius)
        {
            if (toScale == TemperatureScale.Fahrenheit)
            {
                return ConvertFromCelsiusToFahrenheit(inputTemperature);
            }

            return ConvertFromCelsiusToKelvin(inputTemperature);
        }

        if (fromScale == TemperatureScale.Fahrenheit)
        {
            if (toScale == TemperatureScale.Celsius)
            {
                return ConvertFromFahrenheitToCelsius(inputTemperature);
            }

            return ConvertFromFahrenheitToKelvin(inputTemperature);
        }

        if (toScale == TemperatureScale.Celsius)
        {
            return ConvertFromKelvinToCelsius(inputTemperature);
        }

        return ConvertFromKelvinToFahrenheit(inputTemperature);
    }

    private static double ConvertFromCelsiusToFahrenheit(double celsiusDegrees)
    {
        return celsiusDegrees * 1.8 + 32;
    }

    private static double ConvertFromCelsiusToKelvin(double celsiusDegrees)
    {
        return celsiusDegrees + 273.15;
    }

    private static double ConvertFromFahrenheitToCelsius(double fahrenheitDegrees)
    {
        return (fahrenheitDegrees - 32) * 0.5556;
    }

    private static double ConvertFromFahrenheitToKelvin(double fahrenheitDegrees)
    {
        return ConvertFromCelsiusToKelvin(ConvertFromFahrenheitToCelsius(fahrenheitDegrees));
    }

    private static double ConvertFromKelvinToCelsius(double kelvinDegrees)
    {
        return kelvinDegrees - 273.15;
    }

    private static double ConvertFromKelvinToFahrenheit(double kelvinDegrees)
    {
        return ConvertFromCelsiusToFahrenheit(ConvertFromKelvinToCelsius(kelvinDegrees));
    }
}
