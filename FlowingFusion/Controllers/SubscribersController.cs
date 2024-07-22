using FlowingFusion.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FlowingFusion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Tags("Subscribers")]
    public class SubscribersController : ControllerBase
    {
        private static readonly List<Subscriber> Subscribers = new List<Subscriber>();

        /// <summary>
        /// Retrieves all of the active subscribers for one of the authenticated user's products.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <param name="email">Filter subscribers by this email.</param>
        /// <returns>A list of subscribers.</returns>
        /// <response code="200">Returns the list of subscribers.</response>
        [HttpGet("products/{productId}/subscribers")]
        [ProducesResponseType(typeof(IEnumerable<Subscriber>), 200)]
        [Produces("application/json")]
        public IActionResult GetSubscribers(string productId, [FromQuery] string email)
        {
            var filteredSubscribers = Subscribers.Where(s => s.ProductId == productId);

            if (!string.IsNullOrEmpty(email))
            {
                filteredSubscribers = filteredSubscribers.Where(s => s.UserEmail == email);
            }

            return Ok(new { success = true, subscribers = filteredSubscribers });
        }

        /// <summary>
        /// Retrieves the details of a subscriber to this user's product.
        /// </summary>
        /// <param name="id">The ID of the subscriber.</param>
        /// <returns>The subscriber with the specified ID.</returns>
        /// <response code="200">Returns the subscriber with the specified ID.</response>
        /// <response code="404">If the subscriber is not found.</response>
        [HttpGet("subscribers/{id}")]
        [ProducesResponseType(typeof(Subscriber), 200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult GetSubscriber(string id)
        {
            var subscriber = Subscribers.FirstOrDefault(s => s.Id == id);
            if (subscriber == null)
            {
                return NotFound();
            }
            return Ok(new { success = true, subscriber });
        }
    }
}
