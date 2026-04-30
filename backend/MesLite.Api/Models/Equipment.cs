namespace MesLite.Api.Models;

public class Equipment
{
	public int Id { get; set; }

	public string Name { get; set; } = string.Empty;

	public string Type { get; set; } = string.Empty;

	public string Status { get; set; } = "Available";

	public DateTime? LastMaintenanceDate { get; set; }

	public List <Defect> Defects { get; set; } = new();
	
}