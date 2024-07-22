using FlowingFusion.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FlowingFusion.Controllers
{
    [ApiController]
    [Route("products/{productId}/variant_categories")]
    [Tags("Variant Categories")]
    public class VariantCategoriesController : ControllerBase
    {
        private static readonly List<VariantCategory> VariantCategories = new List<VariantCategory>();
        private static readonly List<Variant> Variants = new List<Variant>();

        /// <summary>
        /// Creates a new variant category on a product.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(VariantCategory), 201)]
        [Produces("application/json")]
        public IActionResult CreateVariantCategory(string productId, [FromBody] VariantCategory variantCategory)
        {
            variantCategory.Id = System.Guid.NewGuid().ToString();
            VariantCategories.Add(variantCategory);
            return CreatedAtAction(nameof(GetVariantCategory), new { productId, id = variantCategory.Id }, variantCategory);
        }

        /// <summary>
        /// Retrieves the details of a variant category of a product.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(VariantCategory), 200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult GetVariantCategory(string productId, string id)
        {
            var variantCategory = VariantCategories.FirstOrDefault(vc => vc.Id == id);
            if (variantCategory == null)
            {
                return NotFound();
            }
            return Ok(new { success = true, variant_category = variantCategory });
        }

        /// <summary>
        /// Edits a variant category of an existing product.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult EditVariantCategory(string productId, string id, [FromBody] VariantCategory variantCategory)
        {
            var existingVariantCategory = VariantCategories.FirstOrDefault(vc => vc.Id == id);
            if (existingVariantCategory == null)
            {
                return NotFound();
            }
            existingVariantCategory.Title = variantCategory.Title;
            return NoContent();
        }

        /// <summary>
        /// Deletes a variant category of a product.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult DeleteVariantCategory(string productId, string id)
        {
            var variantCategory = VariantCategories.FirstOrDefault(vc => vc.Id == id);
            if (variantCategory == null)
            {
                return NotFound();
            }
            VariantCategories.Remove(variantCategory);
            return NoContent();
        }

        /// <summary>
        /// Retrieves all of the existing variant categories of a product.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<VariantCategory>), 200)]
        [Produces("application/json")]
        public IActionResult GetVariantCategories(string productId)
        {
            return Ok(new { success = true, variant_categories = VariantCategories });
        }

        /// <summary>
        /// Creates a new variant of a product.
        /// </summary>
        [HttpPost("{variantCategoryId}/variants")]
        [ProducesResponseType(typeof(Variant), 201)]
        [Produces("application/json")]
        public IActionResult CreateVariant(string productId, string variantCategoryId, [FromBody] Variant variant)
        {
            variant.Id = System.Guid.NewGuid().ToString();
            Variants.Add(variant);
            return CreatedAtAction(nameof(GetVariant), new { productId, variantCategoryId, id = variant.Id }, variant);
        }

        /// <summary>
        /// Retrieves the details of a variant of a product.
        /// </summary>
        [HttpGet("{variantCategoryId}/variants/{id}")]
        [ProducesResponseType(typeof(Variant), 200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult GetVariant(string productId, string variantCategoryId, string id)
        {
            var variant = Variants.FirstOrDefault(v => v.Id == id);
            if (variant == null)
            {
                return NotFound();
            }
            return Ok(new { success = true, variant });
        }

        /// <summary>
        /// Edits a variant of an existing product.
        /// </summary>
        [HttpPut("{variantCategoryId}/variants/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult EditVariant(string productId, string variantCategoryId, string id, [FromBody] Variant variant)
        {
            var existingVariant = Variants.FirstOrDefault(v => v.Id == id);
            if (existingVariant == null)
            {
                return NotFound();
            }
            existingVariant.Name = variant.Name;
            existingVariant.PriceDifferenceCents = variant.PriceDifferenceCents;
            existingVariant.MaxPurchaseCount = variant.MaxPurchaseCount;
            return NoContent();
        }

        /// <summary>
        /// Deletes a variant of a product.
        /// </summary>
        [HttpDelete("{variantCategoryId}/variants/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult DeleteVariant(string productId, string variantCategoryId, string id)
        {
            var variant = Variants.FirstOrDefault(v => v.Id == id);
            if (variant == null)
            {
                return NotFound();
            }
            Variants.Remove(variant);
            return NoContent();
        }

        /// <summary>
        /// Retrieves all of the existing variants in a variant category.
        /// </summary>
        [HttpGet("{variantCategoryId}/variants")]
        [ProducesResponseType(typeof(IEnumerable<Variant>), 200)]
        [Produces("application/json")]
        public IActionResult GetVariants(string productId, string variantCategoryId)
        {
            var variantsInCategory = Variants.Where(v => v.Id == variantCategoryId).ToList();
            return Ok(new { success = true, variants = variantsInCategory });
        }
    }
}
