namespace TemperatureConverterTask.Model;

public static class TemperatureConverter
{
    public static double Convert(double inputTemperature, ITemperatureScale fromScale, ITemperatureScale toScale)
    {
        return toScale.FromCelsius(fromScale.ToCelsius(inputTemperature));
    }
}
