using FlowingFusion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FlowingFusion.Controllers
{
    [Authorize]
    [ApiController]
    [Route("products/{productId}/offer_codes")]
    [Tags("Offer Codes")]
    public class OfferCodesController : ControllerBase
    {
        private static readonly List<Product> Products = new List<Product>();

        /// <summary>
        /// Retrieves all of the existing offer codes for a product.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OfferCode>), 200)]
        [Produces("application/json")]
        public IActionResult GetOfferCodes(string productId)
        {
            var product = Products.FirstOrDefault(p => p.Id.ToString() == productId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(new { success = true, offer_codes = product.OfferCodes });
        }

        /// <summary>
        /// Retrieves the details of a specific offer code of a product.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OfferCode), 200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult GetOfferCode(string productId, string id)
        {
            var product = Products.FirstOrDefault(p => p.Id.ToString() == productId);
            if (product == null)
            {
                return NotFound();
            }
            var offerCode = product.OfferCodes.FirstOrDefault(oc => oc.Id == id);
            if (offerCode == null)
            {
                return NotFound();
            }
            return Ok(new { success = true, offer_code = offerCode });
        }

        /// <summary>
        /// Creates a new offer code for a product.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(OfferCode), 201)]
        [Produces("application/json")]
        public IActionResult CreateOfferCode(string productId, [FromBody] OfferCode offerCode)
        {
            var product = Products.FirstOrDefault(p => p.Id.ToString() == productId);
            if (product == null)
            {
                return NotFound();
            }
            offerCode.Id = System.Guid.NewGuid().ToString();
            product.OfferCodes.Add(offerCode);
            return CreatedAtAction(nameof(GetOfferCode), new { productId, id = offerCode.Id }, offerCode);
        }

        /// <summary>
        /// Edits an existing product's offer code.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult EditOfferCode(string productId, string id, [FromBody] OfferCode offerCode)
        {
            var product = Products.FirstOrDefault(p => p.Id.ToString() == productId);
            if (product == null)
            {
                return NotFound();
            }
            var existingOfferCode = product.OfferCodes.FirstOrDefault(oc => oc.Id == id);
            if (existingOfferCode == null)
            {
                return NotFound();
            }
            existingOfferCode.Name = offerCode.Name;
            existingOfferCode.AmountCents = offerCode.AmountCents;
            existingOfferCode.PercentOff = offerCode.PercentOff;
            existingOfferCode.MaxPurchaseCount = offerCode.MaxPurchaseCount;
            existingOfferCode.Universal = offerCode.Universal;
            return NoContent();
        }

        /// <summary>
        /// Permanently delete a product's offer code.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult DeleteOfferCode(string productId, string id)
        {
            var product = Products.FirstOrDefault(p => p.Id.ToString() == productId);
            if (product == null)
            {
                return NotFound();
            }
            var offerCode = product.OfferCodes.FirstOrDefault(oc => oc.Id == id);
            if (offerCode == null)
            {
                return NotFound();
            }
            product.OfferCodes.Remove(offerCode);
            return NoContent();
        }
    }
}
