using GrpcShared;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080, opts => opts.Protocols = HttpProtocols.Http1);
    options.ListenAnyIP(8585, opts => opts.Protocols = HttpProtocols.Http2);
});

builder.Services.AddCodeFirstGrpc();

var app = builder.Build();

//client endpoint
app.MapGrpcService<ScheduleOMaticService>();

//server endpoint
app.MapGet("/", () => "Hello World!");

app.Run();

public class ScheduleOMaticService : IScheduleOMaticService
{
    public Task<ModifyScheduleResponse> ModifySchedule(ModifyScheduleRequest request)
    {
        return Task.FromResult(new ModifyScheduleResponse
        {
            IsSuccess = true,
            Message = "Band rescheduled",
            UpdatedBookingId = request.BookingId + "NEW"
        });
    }

    public Task<ScheduleResponse> Schedule(ScheduleRequest request)
    {
        return Task.FromResult(new ScheduleResponse
        {
            BookingId = Guid.NewGuid().ToString(),
            IsSuccess = true,
            Message = "Band scheduled"
        });
    }
}
