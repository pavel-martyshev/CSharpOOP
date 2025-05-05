using System.Reflection;

namespace TemperatureConverterTask.Model;

internal class TemperatureScalesRegistry
{
    private readonly Dictionary<string, ITemperatureScale> _scales;

    public TemperatureScalesRegistry()
    {
        _scales = [];

        foreach (var scale in LoadScales())
        {
            _scales[scale.Name] = scale;
        }
    }

    private static List<ITemperatureScale> LoadScales()
    {
        return Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(ITemperatureScale).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .Select(t => (ITemperatureScale)Activator.CreateInstance(t)!).ToList();
    }

    public IReadOnlyDictionary<string, ITemperatureScale> Scales => _scales;

    public ITemperatureScale this[string scale] => _scales[scale];
}
