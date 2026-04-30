namespace MesLite.Api.Models;

public class Defect
{
	public int Id { get; set; }

	public int WorkOrderId { get; set; }

	public WorkOrder? WorkOrder { get; set; }

	public int? EquipmentId { get; set; }

	public Equipment? Equipment { get; set; }

	public string Severity { get; set; } = "Low";

	public int QuantityAffected { get; set; }

	public string Description { get; set; } = string.Empty;

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}