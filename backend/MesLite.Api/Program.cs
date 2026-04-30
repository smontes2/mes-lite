using Microsoft.EntityFrameworkCore;
using MesLite.Api.Data;
using MesLite.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MesLiteDbContext>(options =>
    options.UseSqlite("Data Source=meslite.db"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();

    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "MES-Lite API is running");

// GET all work orders
app.MapGet("/api/workorders", async (MesLiteDbContext db) => 
{
    return await db.WorkOrders.ToListAsync();; 
});

// GET one work order by ID
app.MapGet("/api/workorders/{id}", async (int id, MesLiteDbContext db) =>
{
    WorkOrder? workOrder = await db.WorkOrders.FindAsync(id);

    if (workOrder is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(workOrder);
});

// CREATE a work order
app.MapPost("/api/workorders", async (WorkOrder workOrder, MesLiteDbContext db) => 
{
    workOrder.CreatedAt = DateTime.UtcNow;
    workOrder.Status = "Planned";

    db.WorkOrders.Add(workOrder);

    await db.SaveChangesAsync();

    db.AuditLogs.Add(new AuditLog
    {
        UserName = "System",
        EntityType = "WorkOrder",
        EntityId = workOrder.Id,
        Action = "Created",
        OldValue = null,
        NewValue = workOrder.Status,
        Timestamp = DateTime.UtcNow
    });

    await db.SaveChangesAsync();

    return Results.Created($"/api/workorders/{workOrder.Id}", workOrder);
});

// UPDATE work order status
app.MapPut("/api/workorders/{id}/status", async (int id, UpdateWorkOrderStatusRequest request, MesLiteDbContext db) =>
{
    WorkOrder? workOrder = await db.WorkOrders.FindAsync(id);

    if (workOrder is null)
    {
        return Results.NotFound();
    }

    string oldStatus = workOrder.Status;

    workOrder.Status = request.Status;

    db.AuditLogs.Add(new AuditLog
    {
        UserName = request.OperatorName,
        EntityType = "WorkOrder",
        EntityId = workOrder.Id,
        Action = "StatusChanged",
        OldValue = oldStatus,
        NewValue = request.Status,
        Timestamp = DateTime.UtcNow
    });

    await db.SaveChangesAsync();

    return Results.Ok(workOrder);
});

// GET audit logs
app.MapGet("/api/auditlogs", async (MesLiteDbContext db) => 
{
    return await db.AuditLogs
        .OrderByDescending(log => log.Timestamp)
        .ToListAsync();
});

app.Run();

public record UpdateWorkOrderStatusRequest(string Status, string OperatorName);

