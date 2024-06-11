namespace Dima.Core.Requests.Transactions;

public class GetTransactionsByPeriodRequest : PagedRequest
{
    //public DateTime? StartDate { get; set; }
    //public DateTime? EndDate { get; set; }

    private DateTime? _startDate;
    private DateTime? _endDate;

    public DateTime? StartDate
    {
        get => _startDate;
        set => _startDate = value.HasValue
            ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc)
            : (DateTime?)null;
    }

    public DateTime? EndDate
    {
        get => _endDate;
        set => _endDate = value.HasValue
            ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc)
            : (DateTime?)null;
    }
}