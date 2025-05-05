namespace TemperatureConverterTask.Controller;

internal interface ITemperatureConversionController
{
    void OnInputScaleChanged(string selectedScale);

    void GetConvertedTemperature(double inputTemperature);

    void Run();
}
