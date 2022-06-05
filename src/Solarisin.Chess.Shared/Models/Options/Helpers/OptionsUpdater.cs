namespace Solarisin.Chess.Shared.Models.Options.Helpers;

/// <summary>
///     Helper class used to update the EngineOptions and EnvironmentOptions with user-defined values
/// </summary>
public static class OptionsUpdater
{
    /// <summary>
    ///     Updates the EnvironmentOptions with the given values
    /// </summary>
    /// <param name="optionsToUpdate">The EnvironmentOptions to update </param>
    /// <param name="optionValues">The values to update the EnvironmentOptions with</param>
    /// <exception cref="AggregateException">Thrown if an error occurs while updating the EnvironmentOptions</exception>
    public static void SetValues(EnvironmentOptions optionsToUpdate, Dictionary<string, string> optionValues)
    {
        var exceptions = new List<Exception>();
        foreach (var option in optionValues)
            try
            {
                SetValue(optionsToUpdate, option.Key, option.Value);
            }
            catch (Exception e)
            {
                exceptions.Add(e);
            }

        if (exceptions.Any()) throw new AggregateException(exceptions.ToArray());
    }

    /// <summary>
    ///     Updates the EngineOptions with the given values
    /// </summary>
    /// <param name="optionsToUpdate">The EngineOptions to update </param>
    /// <param name="optionValues">The values to update the EngineOptions with</param>
    /// <exception cref="AggregateException">Thrown if an error occurs while updating the EngineOptions</exception>
    public static void SetValues(EngineOptions optionsToUpdate, Dictionary<string, string> optionValues)
    {
        var exceptions = new List<Exception>();
        foreach (var option in optionValues)
            try
            {
                SetValue(optionsToUpdate, option.Key, option.Value);
            }
            catch (Exception e)
            {
                exceptions.Add(e);
            }

        if (exceptions.Any()) throw new AggregateException(exceptions.ToArray());
    }

    /// <summary>
    ///     Updates the given EnvironmentOptions with the given value
    /// </summary>
    /// <param name="optionsToUpdate">The EnvironmentOptions to update</param>
    /// <param name="optionKey">The name of the option to update</param>
    /// <param name="optionValue">The value to update the option with</param>
    /// <exception cref="NotSupportedException">Thrown if the option type is not supported</exception>
    /// <exception cref="ArgumentException">Thrown if the option name was not found in the EnvironmentOptions properties</exception>
    public static void SetValue(EnvironmentOptions optionsToUpdate, string optionKey, string optionValue)
    {
        var property = optionsToUpdate.GetType().GetProperties().FirstOrDefault(o =>
            string.Equals(o.Name, optionKey, StringComparison.CurrentCultureIgnoreCase)
        );
        if (property != null)
        {
            if (property.PropertyType == typeof(bool))
                property.SetValue(optionsToUpdate, bool.Parse(optionValue));
            else if (property.PropertyType == typeof(int))
                property.SetValue(optionsToUpdate, int.Parse(optionValue));
            else if (property.PropertyType == typeof(string))
                property.SetValue(optionsToUpdate, optionValue);
            else
                throw new NotSupportedException(
                    $"Property type '{property.PropertyType.Name}' for '{property.Name}' is not supported");
        }
        else
        {
            // EnvironmentOptions did not contain a property with the specified key name
            throw new ArgumentException(
                $"EnvironmentOptions did not contain a property with the specified key name '{optionKey}'");
        }
    }

    /// <summary>
    ///     Updates the given EnvironmentOptions with the given value
    /// </summary>
    /// <param name="optionsToUpdate">The EnvironmentOptions to update</param>
    /// <param name="optionKey">The name of the option to update</param>
    /// <param name="optionValue">The value to update the option with</param>
    /// <exception cref="NotSupportedException">Thrown if the option type is not supported</exception>
    /// <exception cref="ArgumentException">Thrown if the option name was not found in the EnvironmentOptions properties</exception>
    public static void SetValue(EngineOptions optionsToUpdate, string optionKey, string optionValue)
    {
        var property = optionsToUpdate.GetType().GetProperties().FirstOrDefault(o =>
            string.Equals(o.Name, optionKey, StringComparison.CurrentCultureIgnoreCase)
        );
        if (property != null)
        {
            if (property.PropertyType == typeof(bool))
            {
                property.SetValue(optionsToUpdate, bool.Parse(optionValue));
            }
            else if (property.PropertyType == typeof(int))
            {
                property.SetValue(optionsToUpdate, int.Parse(optionValue));
            }
            else if (property.PropertyType == typeof(string))
            {
                property.SetValue(optionsToUpdate, optionValue);
            }
            else if (property.PropertyType == typeof(UciImplementation))
            {
                var enumValue = Enum.Parse<UciImplementation>(optionValue, true);
                property.SetValue(optionsToUpdate, enumValue);
            }
            else
            {
                throw new NotSupportedException(
                    $"Property type '{property.PropertyType.Name}' for '{property.Name}' is not supported");
            }
        }
        else
        {
            // EngineOptions did not contain a property with the specified key name
            throw new ArgumentException(
                $"EngineOptions did not contain a property with the specified key name '{optionKey}'");
        }
    }
}