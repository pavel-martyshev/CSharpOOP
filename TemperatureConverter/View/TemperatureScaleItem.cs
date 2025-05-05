using TemperatureConverterTask.Model;

namespace TemperatureConverterTask.View;

public class TemperatureScaleItem(string scale, string displayName)
{
    public string Scale { get; } = scale;

    public string DisplayName { get; } = displayName;
}
