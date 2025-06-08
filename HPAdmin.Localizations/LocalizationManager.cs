using System.Text.Json;

namespace HPAdmin.Localizations;

public static class LocalizationManager
{
    private static Dictionary<string, string>? _dict;
    
    public static void SetCulture(string cultureName)
    {
        _dict = new Dictionary<string, string>();
        if (string.IsNullOrWhiteSpace(cultureName))
            throw new InvalidOperationException($"Parameter {nameof(cultureName)} must be specified.");

        var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Locales", $"{cultureName}.json");
        if (!File.Exists(fileName)) 
            return;

        var json = File.ReadAllText(fileName);
        var root = JsonDocument.Parse(json).RootElement;            
        CollectValues(root, _dict);
    }

    private static void CollectValues(JsonElement element, Dictionary<string, string> dict)
    {
        if (_dict == null)
            throw new InvalidOperationException($"LocalizationManager must be initialized with {nameof(SetCulture)} before use.");

        if (element.ValueKind != JsonValueKind.Object) 
            return;

        foreach (var property in element.EnumerateObject())
        {
            var fullKey = property.Name;
            dict[fullKey] = property.Value.GetString() ?? throw new ArgumentNullException($"For key '{property.Name}' in json file is not specified a return value.");
        }
    }

    public static string GetString(string key)
    {
        if (_dict == null)
            throw new InvalidOperationException($"LocalizationManager must be initialized with {nameof(SetCulture)} before use.");

        return _dict.GetValueOrDefault(key, key);
    }
}
