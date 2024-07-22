namespace FlowingFusion.Models
{
    public class License
    {
        public string ProductId { get; set; }
        public string LicenseKey { get; set; }
        public int Uses { get; set; }
        public bool Enabled { get; set; }
        public Purchase Purchase { get; set; }
    }

    public class LicenseRequest
    {
        public string ProductId { get; set; }
        public string LicenseKey { get; set; }
        public bool IncrementUsesCount { get; set; } = true;
    }

    public class LicenseResponse
    {
        public bool Success { get; set; }
        public int Uses { get; set; }
        public Purchase Purchase { get; set; }
    }

    public class Purchase
    {
        public string SellerId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Permalink { get; set; }
        public string ProductPermalink { get; set; }
        public string Email { get; set; }
        public int Price { get; set; }
        public int GumroadFee { get; set; }
        public string Currency { get; set; }
        public int Quantity { get; set; }
        public bool DiscoverFeeCharged { get; set; }
        public bool CanContact { get; set; }
        public string Referrer { get; set; }
        public Card Card { get; set; }
        public int OrderNumber { get; set; }
        public string SaleId { get; set; }
        public DateTime SaleTimestamp { get; set; }
        public string PurchaserId { get; set; }
        public string SubscriptionId { get; set; }
        public string Variants { get; set; }
        public string LicenseKey { get; set; }
        public bool IsMultiseatLicense { get; set; }
        public string IpCountry { get; set; }
        public string Recurrence { get; set; }
        public bool IsGiftReceiverPurchase { get; set; }
        public bool Refunded { get; set; }
        public bool Disputed { get; set; }
        public bool DisputeWon { get; set; }
        public bool Chargebacked { get; set; }
        public DateTime? SubscriptionEndedAt { get; set; }
        public DateTime? SubscriptionCancelledAt { get; set; }
        public DateTime? SubscriptionFailedAt { get; set; }
    }
}
