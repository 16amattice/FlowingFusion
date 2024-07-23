using FlowingFusion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FlowingFusion.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [Tags("Resource Subscriptions")]
    public class ResourceSubscriptionsController : ControllerBase
    {
        private static readonly List<ResourceSubscription> ResourceSubscriptions = new List<ResourceSubscription>();

        /// <summary>
        /// Subscribe to a resource.
        /// </summary>
        /// <param name="resourceSubscription">The resource subscription details.</param>
        /// <returns>The created resource subscription.</returns>
        /// <response code="200">Resource subscription created successfully.</response>
        /// <remarks>
        /// Currently, there are 8 supported resource names:
        /// 
        /// - "sale": Notifies of the user's sales with an HTTP POST to your post_url.
        /// - "refund": Notifies of refunds to the user's sales with an HTTP POST to your post_url.
        /// - "dispute": Notifies of disputes raised against user's sales with an HTTP POST to your post_url.
        /// - "dispute_won": Notifies of the sale disputes won by the user with an HTTP POST to your post_url.
        /// - "cancellation": Notifies of cancellations of the user's subscribers with an HTTP POST to your post_url.
        /// - "subscription_updated": Notifies when subscriptions to the user's products have been upgraded or downgraded with an HTTP POST to your post_url.
        /// - "subscription_ended": Notifies when subscriptions to the user's products have ended with an HTTP POST to your post_url.
        /// - "subscription_restarted": Notifies when subscriptions to the user's products have been restarted with an HTTP POST to your post_url.
        /// 
        /// In each POST request, these parameters are sent:
        /// 
        /// - subscription_id: Id of the subscription
        /// - product_id: Id of the product
        /// - product_name: Name of the product
        /// - user_id: User id of the subscriber
        /// - user_email: Email address of the subscriber
        /// - purchase_ids: Array of charge ids belonging to this subscription
        /// - created_at: Timestamp when subscription was created
        /// - charge_occurrence_count: Number of charges made for this subscription
        /// - recurrence: Subscription duration - monthly/quarterly/biannually/yearly/every_two_years
        /// - free_trial_ends_at: Timestamp when free trial ends, if free trial is enabled for the membership
        /// - custom_fields: Custom fields from the original purchase
        /// - license_key: License key from the original purchase
        /// 
        /// For "cancellation" resource:
        /// 
        /// - cancelled: True if subscription has been cancelled, otherwise false
        /// - cancelled_at: Timestamp at which subscription will be cancelled
        /// - cancelled_by_admin: True if subscription was cancelled by admin, otherwise not present
        /// - cancelled_by_buyer: True if subscription was cancelled by buyer, otherwise not present
        /// - cancelled_by_seller: True if subscription was cancelled by seller, otherwise not present
        /// - cancelled_due_to_payment_failures: True if subscription was cancelled automatically because of payment failure, otherwise not present
        /// 
        /// For "subscription_updated" resource:
        /// 
        /// - type: "upgrade" or "downgrade"
        /// - effective_as_of: Timestamp at which the change went or will go into effect
        /// - old_plan: Tier, subscription duration, price, and quantity of the subscription before the change
        /// - new_plan: Tier, subscription duration, price, and quantity of the subscription after the change
        /// 
        /// For "subscription_ended" resource:
        /// 
        /// - ended_at: Timestamp at which the subscription ended
        /// - ended_reason: The reason for the subscription ending ("cancelled", "failed_payment", or "fixed_subscription_period_ended")
        /// 
        /// For "subscription_restarted" resource:
        /// 
        /// - restarted_at: Timestamp at which the subscription was restarted
        /// </remarks>
        [HttpPut]
        [ProducesResponseType(typeof(ResourceSubscription), 200)]
        [Produces("application/json")]
        public IActionResult Subscribe([FromBody] ResourceSubscription resourceSubscription)
        {
            resourceSubscription.Id = System.Guid.NewGuid().ToString();
            ResourceSubscriptions.Add(resourceSubscription);
            return Ok(new { success = true, resource_subscription = resourceSubscription });
        }

        /// <summary>
        /// Show all active subscriptions of user for the input resource.
        /// </summary>
        /// <param name="resourceName">The name of the resource.</param>
        /// <returns>List of active resource subscriptions.</returns>
        /// <response code="200">Returns the list of resource subscriptions.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ResourceSubscription>), 200)]
        [Produces("application/json")]
        public IActionResult GetSubscriptions([FromQuery] string resourceName)
        {
            var subscriptions = ResourceSubscriptions.Where(rs => rs.ResourceName == resourceName).ToList();
            return Ok(new { success = true, resource_subscriptions = subscriptions });
        }

        /// <summary>
        /// Unsubscribe from a resource.
        /// </summary>
        /// <param name="id">The ID of the resource subscription.</param>
        /// <response code="204">Resource subscription deleted successfully.</response>
        /// <response code="404">Resource subscription not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult Unsubscribe(string id)
        {
            var resourceSubscription = ResourceSubscriptions.FirstOrDefault(rs => rs.Id == id);
            if (resourceSubscription == null)
            {
                return NotFound();
            }
            ResourceSubscriptions.Remove(resourceSubscription);
            return NoContent();
        }
    }
}
