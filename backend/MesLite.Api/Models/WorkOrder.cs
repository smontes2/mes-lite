namespace MesLite.Api.Models;

public class WorkOrder
{
	public int Id { get; set; }

	public string WorkOrderNumber { get; set; } = string.Empty;

	public string ProductName { get; set; } = string.Empty;

	public string BatchNumber { get; set; } = string.Empty;

	public int TargetQuantity { get; set; }

	public int CompletedQuantity { get; set; }

	public string Status { get; set; } = "Planned";

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public DateTime? DueDate { get; set; }

	public List<Defect> Defects { get; set; } = new();
	
}