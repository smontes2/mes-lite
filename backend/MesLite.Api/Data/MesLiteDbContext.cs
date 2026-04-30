using Microsoft.EntityFrameworkCore;
using MesLite.Api.Models;

namespace MesLite.Api.Data;

public class MesLiteDbContext : DbContext
{
	public MesLiteDbContext(DbContextOptions<MesLiteDbContext> options)
		: base(options)
		{
		}

		public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();

		public DbSet<Equipment> Equipment => Set<Equipment>();

		public DbSet<Defect> Defects => Set<Defect>();

		public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
		
}