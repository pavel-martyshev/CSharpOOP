namespace TemperatureConverterTask.Controllers;

internal interface ITemperatureConversionController
{
    public void OnInputScaleChanged(object? sender, EventArgs e);

    public void OnInputTemperatureChanged(object? sender, EventArgs e);

    public void Run();
}
