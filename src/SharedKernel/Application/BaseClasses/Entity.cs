/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.SharedKernel.Application.BaseClasses;

/// <summary>
/// 
/// </summary>
public abstract class Entity : IEquatable<Entity>
{
    /// <summary>
    /// The unique identifier of the entity.
    /// </summary>
    public Guid Id { get; protected set; }
    
    /// <summary>
    /// Default constructor
    /// </summary>
    public Entity() { Id = CreateNewId(); }

    /// <summary>
    /// Create a new unique identifier.
    /// </summary>
    public static Guid CreateNewId()
    {
        return Guid.NewGuid();
    }

    public bool Equals(Entity? other)
    {
        return Equals((object?)other);
    }

}