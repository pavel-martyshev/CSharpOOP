namespace TemperatureConverterTask.Controller;

internal interface ITemperatureConversionController
{
    void OnInputScaleChanged(object selectedScale);

    void GetConvertedTemperature(double inputTemperature);

    void Run();
}
