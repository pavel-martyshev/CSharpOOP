using TemperatureConverterTask.Model;

namespace TemperatureConverterTask.View;

public class TemperatureScaleItem(TemperatureScale scale, string displayName)
{
    public TemperatureScale Scale { get; } = scale;

    public string DisplayName { get; } = displayName;
}
