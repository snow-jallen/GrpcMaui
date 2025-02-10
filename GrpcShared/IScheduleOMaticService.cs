using System.Runtime.Serialization;
using System.ServiceModel;

namespace GrpcShared;

[DataContract]
public class ScheduleRequest
{
    [DataMember(Order = 1)]
    public string BandName { get; set; }
    [DataMember(Order = 2)]
    public DateTime PerformanceDate { get; set; }
    [DataMember(Order = 3)]
    public string Venue { get; set; }
}

[DataContract]
public class ScheduleResponse
{
    [DataMember(Order = 1)]
    public string BookingId { get; set; }
    [DataMember(Order = 2)]
    public bool IsSuccess { get; set; }
    [DataMember(Order = 3)]
    public string Message { get; set; }
}

[DataContract]
public class ModifyScheduleRequest
{
    [DataMember(Order = 1)]
    public string BookingId { get; set; }
    [DataMember(Order = 2)]
    public DateTime NewPerformanceDate { get; set; }
    [DataMember(Order = 3)]
    public string NewVenue { get; set; }
}

[DataContract]
public class ModifyScheduleResponse
{
    [DataMember(Order = 1)]
    public string UpdatedBookingId { get; set; }
    [DataMember(Order = 2)]
    public bool IsSuccess { get; set; }
    [DataMember(Order = 3)]
    public string Message { get; set; }
}

[ServiceContract]
public interface IScheduleOMaticService
{
    [OperationContract]
    public Task<ScheduleResponse> Schedule(ScheduleRequest request);
    [OperationContract]
    public Task<ModifyScheduleResponse> ModifySchedule(ModifyScheduleRequest request);
}
