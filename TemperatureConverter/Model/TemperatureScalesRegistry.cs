using System.Reflection;

namespace TemperatureConverterTask.Model;

internal class TemperatureScalesRegistry
{
    private readonly Dictionary<string, ITemperatureScale> _scales;

    public TemperatureScalesRegistry()
    {
        _scales = LoadScales();
    }

    private static Dictionary<string, ITemperatureScale> LoadScales()
    {
        return Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(ITemperatureScale).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .Select(t => (ITemperatureScale)Activator.CreateInstance(t)!)
            .ToDictionary(s => s.Name, s => s);
    }

    public IReadOnlyDictionary<string, ITemperatureScale> Scales => _scales;

    public ITemperatureScale this[string scale] => _scales[scale];
}
