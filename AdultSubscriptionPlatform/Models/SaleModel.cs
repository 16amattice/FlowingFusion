namespace FlowingFusion.Models
{
    public class Sale
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string SellerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ProductName { get; set; }
        public bool ProductHasVariants { get; set; }
        public int Price { get; set; }
        public int GumroadFee { get; set; }
        public string SubscriptionDuration { get; set; }
        public string FormattedDisplayPrice { get; set; }
        public string FormattedTotalPrice { get; set; }
        public string CurrencySymbol { get; set; }
        public string AmountRefundableInCurrency { get; set; }
        public string ProductId { get; set; }
        public string ProductPermalink { get; set; }
        public bool PartiallyRefunded { get; set; }
        public bool Chargedback { get; set; }
        public string PurchaseEmail { get; set; }
        public string ZipCode { get; set; }
        public bool Paid { get; set; }
        public bool HasVariants { get; set; }
        public Dictionary<string, string> Variants { get; set; }
        public string VariantsAndQuantity { get; set; }
        public bool HasCustomFields { get; set; }
        public Dictionary<string, string> CustomFields { get; set; }
        public long OrderId { get; set; }
        public bool IsProductPhysical { get; set; }
        public string PurchaserId { get; set; }
        public bool IsRecurringBilling { get; set; }
        public bool CanContact { get; set; }
        public bool IsFollowing { get; set; }
        public bool Disputed { get; set; }
        public bool DisputeWon { get; set; }
        public bool IsAdditionalContribution { get; set; }
        public bool DiscoverFeeCharged { get; set; }
        public bool IsGiftSenderPurchase { get; set; }
        public bool IsGiftReceiverPurchase { get; set; }
        public string Referrer { get; set; }
        public Card Card { get; set; }
        public int? ProductRating { get; set; }
        public int ReviewsCount { get; set; }
        public int AverageRating { get; set; }
        public string SubscriptionId { get; set; }
        public bool Cancelled { get; set; }
        public bool Ended { get; set; }
        public bool RecurringCharge { get; set; }
        public string LicenseKey { get; set; }
        public string LicenseId { get; set; }
        public bool LicenseDisabled { get; set; }
        public Affiliate Affiliate { get; set; }
        public int Quantity { get; set; }
        public bool Shipped { get; set; }
        public string TrackingUrl { get; set; }
        public int RefundedAmountCents { get; set; }
        public OfferCode OfferCode { get; set; }
    }

    public class Card
    {
        public string Visual { get; set; }
        public string Type { get; set; }
    }

    public class Affiliate
    {
        public string Email { get; set; }
        public string Amount { get; set; }
    }
}
