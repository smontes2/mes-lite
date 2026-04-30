namespace MesLite.Api.Models;

public class AuditLog
{
	public int Id { get; set; }

	public string UserName { get; set; } = string.Empty;

	public string EntityType { get; set; } = string.Empty;

	public int EntityId { get; set; }

	public string Action { get; set; } = string.Empty;

	public string? OldValue { get; set; }

	public string? NewValue { get; set; }

	public DateTime Timestamp { get; set; } = DateTime.UtcNow;
	
}