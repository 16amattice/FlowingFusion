using FlowingFusion.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlowingFusion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Tags("Sales")]
    public class SalesController : ControllerBase
    {
        private static readonly List<Sale> Sales = new List<Sale>();

        /// <summary>
        /// Retrieves all of the successful sales by the authenticated user.
        /// </summary>
        /// <param name="after">Only return sales after this date (YYYY-MM-DD).</param>
        /// <param name="before">Only return sales before this date (YYYY-MM-DD).</param>
        /// <param name="productId">Filter sales by this product.</param>
        /// <param name="email">Filter sales by this email.</param>
        /// <param name="orderId">Filter sales by this Order ID.</param>
        /// <param name="pageKey">A key representing a page of results. It is given in the response as `next_page_key`.</param>
        /// <returns>A list of sales.</returns>
        /// <response code="200">Returns the list of sales.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Sale>), 200)]
        [Produces("application/json")]
        public IActionResult GetSales([FromQuery] string after, [FromQuery] string before, [FromQuery] string productId, [FromQuery] string email, [FromQuery] string orderId, [FromQuery] string pageKey)
        {
            var filteredSales = Sales.AsEnumerable();

            if (DateTime.TryParse(after, out DateTime afterDate))
            {
                filteredSales = filteredSales.Where(s => s.CreatedAt > afterDate);
            }

            if (DateTime.TryParse(before, out DateTime beforeDate))
            {
                filteredSales = filteredSales.Where(s => s.CreatedAt < beforeDate);
            }

            if (!string.IsNullOrEmpty(productId))
            {
                filteredSales = filteredSales.Where(s => s.ProductId == productId);
            }

            if (!string.IsNullOrEmpty(email))
            {
                filteredSales = filteredSales.Where(s => s.Email == email);
            }

            if (!string.IsNullOrEmpty(orderId))
            {
                filteredSales = filteredSales.Where(s => s.OrderId.ToString() == orderId);
            }

            return Ok(new { success = true, sales = filteredSales });
        }

        /// <summary>
        /// Retrieves the details of a sale by this user.
        /// </summary>
        /// <param name="id">The ID of the sale to retrieve.</param>
        /// <returns>The sale with the specified ID.</returns>
        /// <response code="200">Returns the sale with the specified ID.</response>
        /// <response code="404">If the sale is not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Sale), 200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult GetSale(string id)
        {
            var sale = Sales.FirstOrDefault(s => s.Id == id);
            if (sale == null)
            {
                return NotFound();
            }
            return Ok(new { success = true, sale });
        }

        /// <summary>
        /// Marks a sale as shipped.
        /// </summary>
        /// <param name="id">The ID of the sale to mark as shipped.</param>
        /// <param name="trackingUrl">The tracking URL.</param>
        /// <returns>The updated sale.</returns>
        /// <response code="200">Sale marked as shipped successfully.</response>
        /// <response code="404">If the sale is not found.</response>
        [HttpPut("{id}/mark_as_shipped")]
        [ProducesResponseType(typeof(Sale), 200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult MarkAsShipped(string id, [FromBody] string trackingUrl)
        {
            var sale = Sales.FirstOrDefault(s => s.Id == id);
            if (sale == null)
            {
                return NotFound();
            }
            sale.Shipped = true;
            sale.TrackingUrl = trackingUrl;
            return Ok(new { success = true, sale });
        }

        /// <summary>
        /// Refunds a sale.
        /// </summary>
        /// <param name="id">The ID of the sale to refund.</param>
        /// <param name="amountCents">The amount in cents to refund. If not set, issue full refund.</param>
        /// <returns>The updated sale.</returns>
        /// <response code="200">Sale refunded successfully.</response>
        /// <response code="404">If the sale is not found.</response>
        [HttpPut("{id}/refund")]
        [ProducesResponseType(typeof(Sale), 200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult Refund(string id, [FromBody] int? amountCents)
        {
            var sale = Sales.FirstOrDefault(s => s.Id == id);
            if (sale == null)
            {
                return NotFound();
            }
            if (amountCents.HasValue)
            {
                sale.RefundedAmountCents += amountCents.Value;
            }
            else
            {
                sale.RefundedAmountCents = sale.Price;
            }
            return Ok(new { success = true, sale });
        }
    }
}
