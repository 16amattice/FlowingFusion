namespace FlowingFusion.Models
{
    public class Subscriber
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<string> PurchaseIds { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UserRequestedCancellationAt { get; set; }
        public int? ChargeOccurrenceCount { get; set; }
        public string Recurrence { get; set; }
        public DateTime? CancelledAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public DateTime? FailedAt { get; set; }
        public DateTime? FreeTrialEndsAt { get; set; }
        public string LicenseKey { get; set; }
        public string Status { get; set; }
    }
}
