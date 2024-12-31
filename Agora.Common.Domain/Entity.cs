namespace Agora.Common.Domain;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TIdentity"></typeparam>
public abstract class Entity<TIdentity>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public TIdentity Id { get; protected set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>
    /// Determines whether the current entity is equal to another entity.
    /// </summary>
    /// <param name="obj">The entity to compare.</param>
    /// <returns>True if the entities are equal, otherwise false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TIdentity> other || other is null)
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

        if (IsTransient() || other.IsTransient())
        {
            return false;
        }

        return Id!.Equals(other.Id);
    }

    /// <summary>
    /// Determines whether two entities are equal.
    /// </summary>
    /// <param name="left">The first entity to compare.</param>
    /// <param name="right">The second entity to compare.</param>
    /// <returns>True if the entities are equal, otherwise false.</returns>
    public static bool operator ==(Entity<TIdentity> left, Entity<TIdentity> right)
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
    /// Determines whether two entities are not equal.
    /// </summary>
    /// <param name="left">The first entity to compare.</param>
    /// <param name="right">The second entity to compare.</param>
    /// <returns>True if the entities are not equal, otherwise false.</returns>
    public static bool operator !=(Entity<TIdentity> left, Entity<TIdentity> right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Gets the hash code for the entity, based on its type and identifier.
    /// </summary>
    /// <returns>The hash code of the entity.</returns>
    public override int GetHashCode()
    {
        return (GetType().ToString() + Id).GetHashCode();
    }

    protected abstract bool IsTransient();
}

public abstract class EntityOfStruct<TIdentity> : Entity<TIdentity>
    where TIdentity : struct
{
    protected override bool IsTransient()
    {
        return Id.Equals(default(TIdentity));
    }
}

public abstract class EntityOfClass<TIdentity> : Entity<TIdentity>
    where TIdentity : class
{
    protected override bool IsTransient()
    {
        return Id is null;
    }
}

public abstract class Entity : EntityOfStruct<long> { }

