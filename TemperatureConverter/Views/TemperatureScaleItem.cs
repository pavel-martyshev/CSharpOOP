using TemperatureConverterTask.Models;

namespace TemperatureConverterTask.Views;

public class TemperatureScaleItem(TemperatureScale scale, string displayName)
{
    public TemperatureScale Scale { get; } = scale;

    public string DisplayName { get; } = displayName;
}
