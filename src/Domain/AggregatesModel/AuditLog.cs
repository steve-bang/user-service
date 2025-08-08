/*
* Author: Steve Bang
* History:
* - [2025-05-05] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.UserService.Domain.AggregatesModel;

public class Audit : AggregateRoot
{
    public Guid UserId { get; private set; }
    public string Action { get; private set; } // "Create", "Update", "Delete", etc.
    public string EntityType { get; private set; }  // "Product", "Order", etc.
    public Guid EntityId { get; private set; }
    public string OldValues { get; private set; } // JSON serialized
    public string NewValues { get; private set; } // JSON serialized
    public string AffectedColumns { get; private set; } // Comma-separated
    public string IpAddress { get; private set; }
    public string UserAgent { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Private constructor for EF Core
    private Audit() { }

    public Audit(
        Guid userId,
        string action,
        string entityType,
        Guid entityId,
        string oldValues,
        string newValues,
        string affectedColumns,
        string ipAddress,
        string userAgent
    )
    {
        UserId = userId;
        Action = action;
        EntityType = entityType;
        EntityId = entityId;
        OldValues = oldValues;
        NewValues = newValues;
        AffectedColumns = affectedColumns;
        IpAddress = ipAddress;
        UserAgent = userAgent;
    }


}