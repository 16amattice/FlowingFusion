namespace FlowingFusion.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Published { get; set; }
        public string Url { get; set; }
        public string Currency { get; set; }
        public string ThumbnailUrl { get; set; }
        public List<string> Tags { get; set; }
        public string FormattedPrice { get; set; }
        public int SalesCount { get; set; }
        public int SalesUsdCents { get; set; }
        public bool IsTieredMembership { get; set; }
        public List<string> Recurrences { get; set; }
        public List<VariantCategory> VariantCategories { get; set; }
        public List<OfferCode> OfferCodes { get; set; }
        public List<CustomField> CustomFields { get; set; }


    }

    public class OfferCode
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? AmountCents { get; set; }
        public int? PercentOff { get; set; }
        public int? MaxPurchaseCount { get; set; }
        public bool Universal { get; set; }
        public int TimesUsed { get; set; }
    }

    public class VariantCategory
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public List<Option> Options { get; set; }
    }

    public class Variant
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int PriceDifferenceCents { get; set; }
        public int? MaxPurchaseCount { get; set; }
    }

    public class Option
    {
        public string Name { get; set; }
        public int PriceDifference { get; set; }
        public Dictionary<string, int> PurchasingPowerParityPrices { get; set; }
        public bool IsPayWhatYouWant { get; set; }
        public Dictionary<string, RecurrencePrice> RecurrencePrices { get; set; }
    }

    public class RecurrencePrice
    {
        public int PriceCents { get; set; }
        public int? SuggestedPriceCents { get; set; }
        public Dictionary<string, int> PurchasingPowerParityPrices { get; set; }
    }

    public class CustomField
    {
        public string Name { get; set; }
        public bool Required { get; set; }
    }
}
