using GrpcShared;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);
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
