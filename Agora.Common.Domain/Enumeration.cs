using CSharpFunctionalExtensions;
using System.Reflection;

namespace Agora.Common.Domain;

public abstract class Enumeration : IComparable
{
    private readonly int _value;
    private readonly string _displayName;

    /// <summary>
    /// Initializes a new instance of the Enumeration class.
    /// </summary>
    /// <param name="value">The integer value of the enumeration.</param>
    /// <param name="displayName">The display name of the enumeration.</param>
    protected Enumeration(int value, string displayName)
    {
        _value = value;
        _displayName = displayName;
    }

    /// <summary>
    /// Gets the value of the enumeration.
    /// </summary>
    public int Value => _value;

    /// <summary>
    /// Gets the display name of the enumeration.
    /// </summary>
    public string DisplayName => _displayName;

    public override string ToString() => DisplayName;

    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration otherValue)
        {
            return false;
        }

        var typeMatches = GetType().Equals(obj.GetType());
        var valueMatches = _value.Equals(otherValue.Value);

        return typeMatches && valueMatches;
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    public int CompareTo(object? obj)
    {
        return Value.CompareTo((obj as Enumeration)?.Value);
    }


    /// <summary>
    /// Calculates the absolute difference between the values of two enumeration instances.
    /// </summary>
    /// <param name="firstValue">The first Enumeration instance.</param>
    /// <param name="secondValue">The second Enumeration instance.</param>
    /// <returns>The absolute difference between the values of the two Enumeration instances.</returns>
    public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
    {
        var absoluteDifference = Math.Abs(firstValue.Value - secondValue.Value);
        return absoluteDifference;
    }

    /// <summary>
    /// Retrieves an enumeration instance of the specified type matching the given integer value.
    /// </summary>
    /// <typeparam name="T">The Enumeration type.</typeparam>
    /// <param name="value">The integer value to match.</param>
    /// <returns>An instance of the specified Enumeration type that matches the given value.</returns>
    public static T FromValue<T>(int value) where T : Enumeration
    {
        var matchingItem = Parse<T, int>(value, "value", item => item.Value == value);
        return matchingItem;
    }

    /// <summary>
    /// Retrieves an enumeration instance of the specified type matching the given display name.
    /// </summary>
    /// <typeparam name="T">The Enumeration type.</typeparam>
    /// <param name="displayName">The display name to match.</param>
    /// <returns>An instance of the specified Enumeration type that matches the given display name.</returns>
    public static T FromDisplayName<T>(string displayName) where T : Enumeration
    {
        var matchingItem = Parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
        return matchingItem;
    }

    /// <summary>
    /// Retrieves all enumeration instances of a specific enumeration type T.
    /// </summary>
    /// <typeparam name="T">The enumeration type.</typeparam>
    /// <returns>An IEnumerable of all instances of type T.</returns>
    public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
        typeof(T).GetFields(BindingFlags.Public |
                            BindingFlags.Static |
                            BindingFlags.DeclaredOnly)
                 .Select(f => f.GetValue(null))
                 .Cast<T>();

    private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
    {
        var matchingItem = GetAll<T>().FirstOrDefault(predicate);

        if (matchingItem == null)
        {
            var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
            throw new ArgumentOutOfRangeException(message);
        }

        return matchingItem;
    }
}