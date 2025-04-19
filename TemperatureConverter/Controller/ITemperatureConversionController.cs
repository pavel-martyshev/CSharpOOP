namespace TemperatureConverterTask.Controller;

internal interface ITemperatureConversionController
{
    public void OnInputScaleChanged(object selectedScale);

    public void OnInputTemperatureChanged(double inputTemperature);

    public void Run();
}
