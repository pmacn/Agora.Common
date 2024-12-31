namespace Agora.Common.Domain;

/// <summary>
/// An abstract base class for value objects, providing standard implementations for equality operations.
/// Value objects are objects that are compared based on their value rather than their reference.
/// </summary>
public abstract class ValueObject
{
    /// <summary>
    /// When overridden in a derived class, provides the components that make up the value of the object for equality comparison.
    /// </summary>
    protected abstract IEnumerable<object?> EqualityComponents { get; }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not ValueObject other || other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

        return EqualityComponents.SequenceEqual(other.EqualityComponents);
    }

    /// <summary>
    /// Equality operator to determine if two ValueObject instances are equal.
    /// </summary>
    public static bool operator ==(ValueObject left, ValueObject right)
    {
        if (left is null && right is null)
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }

    /// <summary>
    /// Inequality operator to determine if two ValueObject instances are not equal.
    /// </summary>
    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Provides a hash code for the components of the value object.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return EqualityComponents.Aggregate(1, (current, obj) => current * 23 + (obj?.GetHashCode() ?? 0));
    }
}

