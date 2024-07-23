using FlowingFusion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FlowingFusion.Controllers
{
    [Authorize]
    [ApiController]
    [Route("products/{productId}/custom_fields")]
    [Tags("Custom Fields")]
    public class CustomFieldsController : ControllerBase
    {
        private static readonly List<Product> Products = new List<Product>();

        /// <summary>
        /// Retrieves all of the existing custom fields for a product.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomField>), 200)]
        [Produces("application/json")]
        public IActionResult GetCustomFields(string productId)
        {
            var product = Products.FirstOrDefault(p => p.Id.ToString() == productId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(new { success = true, custom_fields = product.CustomFields });
        }

        /// <summary>
        /// Retrieves the details of a specific custom field of a product.
        /// </summary>
        [HttpGet("{name}")]
        [ProducesResponseType(typeof(CustomField), 200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult GetCustomField(string productId, string name)
        {
            var product = Products.FirstOrDefault(p => p.Id.ToString() == productId);
            if (product == null)
            {
                return NotFound();
            }
            var customField = product.CustomFields.FirstOrDefault(cf => cf.Name == name);
            if (customField == null)
            {
                return NotFound();
            }
            return Ok(new { success = true, custom_field = customField });
        }

        /// <summary>
        /// Creates a new custom field for a product.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CustomField), 201)]
        [Produces("application/json")]
        public IActionResult CreateCustomField(string productId, [FromBody] CustomField customField)
        {
            var product = Products.FirstOrDefault(p => p.Id.ToString() == productId);
            if (product == null)
            {
                return NotFound();
            }
            product.CustomFields.Add(customField);
            return CreatedAtAction(nameof(GetCustomField), new { productId, name = customField.Name }, customField);
        }

        /// <summary>
        /// Edits an existing product's custom field.
        /// </summary>
        [HttpPut("{name}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult EditCustomField(string productId, string name, [FromBody] CustomField customField)
        {
            var product = Products.FirstOrDefault(p => p.Id.ToString() == productId);
            if (product == null)
            {
                return NotFound();
            }
            var existingCustomField = product.CustomFields.FirstOrDefault(cf => cf.Name == name);
            if (existingCustomField == null)
            {
                return NotFound();
            }
            existingCustomField.Required = customField.Required;
            return NoContent();
        }

        /// <summary>
        /// Permanently deletes a product's custom field.
        /// </summary>
        [HttpDelete("{name}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult DeleteCustomField(string productId, string name)
        {
            var product = Products.FirstOrDefault(p => p.Id.ToString() == productId);
            if (product == null)
            {
                return NotFound();
            }
            var customField = product.CustomFields.FirstOrDefault(cf => cf.Name == name);
            if (customField == null)
            {
                return NotFound();
            }
            product.CustomFields.Remove(customField);
            return NoContent();
        }
    }
}
