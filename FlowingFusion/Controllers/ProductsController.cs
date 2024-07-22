using FlowingFusion.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FlowingFusion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> Products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Basic Plan",
                Description = "Basic subscription plan",
                Price = 9.99M,
                Published = true,
                Url = "http://example.com/basic",
                Currency = "usd",
                ThumbnailUrl = "http://example.com/basic-thumbnail.png",
                Tags = new List<string> { "basic", "plan" },
                FormattedPrice = "$9.99",
                SalesCount = 0,
                SalesUsdCents = 0,
                IsTieredMembership = true,
                Recurrences = new List<string> { "monthly" },
                VariantCategories = new List<VariantCategory>
                {
                    new VariantCategory
                    {
                        Id = System.Guid.NewGuid().ToString(),
                        Title = "Tier",
                        Options = new List<Option>
                        {
                            new Option
                            {
                                Name = "First Tier",
                                PriceDifference = 0,
                                PurchasingPowerParityPrices = new Dictionary<string, int>
                                {
                                    { "US", 200 },
                                    { "IN", 100 },
                                    { "EC", 50 }
                                },
                                IsPayWhatYouWant = false,
                                RecurrencePrices = new Dictionary<string, RecurrencePrice>
                                {
                                    {
                                        "monthly", new RecurrencePrice
                                        {
                                            PriceCents = 300,
                                            SuggestedPriceCents = null,
                                            PurchasingPowerParityPrices = new Dictionary<string, int>
                                            {
                                                { "US", 400 },
                                                { "IN", 200 },
                                                { "EC", 100 }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                OfferCodes = new List<OfferCode>
                {
                    new OfferCode
                    {
                        Id = System.Guid.NewGuid().ToString(),
                        Name = "DISCOUNT10",
                        AmountCents = 1000,
                        MaxPurchaseCount = null,
                        Universal = false,
                        TimesUsed = 0
                    }
                },
                CustomFields = new List<CustomField>
                {
                    new CustomField
                    {
                        Name = "phone number",
                        Required = false
                    }
                }
            }
        };

        /// <summary>
        /// Retrieves a list of products.
        /// </summary>
        /// <returns>A list of products.</returns>
        /// <response code="200">Returns the list of products.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        [Produces("application/json")]
        public IActionResult Get()
        {
            return Ok(new { success = true, products = Products });
        }

        /// <summary>
        /// Retrieves a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product with the specified ID.</returns>
        /// <response code="200">Returns the product with the specified ID.</response>
        /// <response code="404">If the product is not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public ActionResult<Product> Get(int id)
        {
            var product = Products.Find(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>The created product.</returns>
        /// <response code="201">Returns the newly created product.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Product), 201)]
        [Produces("application/json")]
        public ActionResult<Product> Post(Product product)
        {
            product.Id = Products.Count + 1;
            Products.Add(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        /// <summary>
        /// Deletes a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <response code="204">If the product is successfully deleted.</response>
        /// <response code="404">If the product is not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult Delete(int id)
        {
            var product = Products.Find(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            Products.Remove(product);
            return NoContent();
        }

        /// <summary>
        /// Enables a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to enable.</param>
        /// <response code="204">If the product is successfully enabled.</response>
        /// <response code="404">If the product is not found.</response>
        [HttpPut("{id}/enable")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult Enable(int id)
        {
            var product = Products.Find(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            product.Published = true;
            return NoContent();
        }

        /// <summary>
        /// Disables a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to disable.</param>
        /// <response code="204">If the product is successfully disabled.</response>
        /// <response code="404">If the product is not found.</response>
        [HttpPut("{id}/disable")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult Disable(int id)
        {
            var product = Products.Find(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            product.Published = false;
            return NoContent();
        }
    }
}
