using FlowingFusion.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowingFusion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Tags("Licenses")]
    public class LicensesController : ControllerBase
    {
        private static readonly List<License> Licenses = new List<License>();

        /// <summary>
        /// Verify a license
        /// </summary>
        /// <param name="licenseRequest">The license verification request.</param>
        /// <returns>The verification result.</returns>
        [HttpPost("verify")]
        [ProducesResponseType(typeof(LicenseResponse), 200)]
        [Produces("application/json")]
        public IActionResult VerifyLicense([FromBody] LicenseRequest licenseRequest)
        {
            var license = Licenses.FirstOrDefault(l => l.ProductId == licenseRequest.ProductId && l.LicenseKey == licenseRequest.LicenseKey);

            if (license == null)
            {
                return NotFound(new { success = false });
            }

            if (licenseRequest.IncrementUsesCount)
            {
                license.Uses += 1;
            }

            return Ok(new LicenseResponse { Success = true, Uses = license.Uses, Purchase = license.Purchase });
        }

        /// <summary>
        /// Enable a license
        /// </summary>
        /// <param name="licenseRequest">The license enable request.</param>
        /// <returns>The enable result.</returns>
        [HttpPut("enable")]
        [ProducesResponseType(typeof(LicenseResponse), 200)]
        [Produces("application/json")]
        public IActionResult EnableLicense([FromBody] LicenseRequest licenseRequest)
        {
            var license = Licenses.FirstOrDefault(l => l.ProductId == licenseRequest.ProductId && l.LicenseKey == licenseRequest.LicenseKey);

            if (license == null)
            {
                return NotFound(new { success = false });
            }

            license.Enabled = true;
            return Ok(new LicenseResponse { Success = true, Uses = license.Uses, Purchase = license.Purchase });
        }

        /// <summary>
        /// Disable a license
        /// </summary>
        /// <param name="licenseRequest">The license disable request.</param>
        /// <returns>The disable result.</returns>
        [HttpPut("disable")]
        [ProducesResponseType(typeof(LicenseResponse), 200)]
        [Produces("application/json")]
        public IActionResult DisableLicense([FromBody] LicenseRequest licenseRequest)
        {
            var license = Licenses.FirstOrDefault(l => l.ProductId == licenseRequest.ProductId && l.LicenseKey == licenseRequest.LicenseKey);

            if (license == null)
            {
                return NotFound(new { success = false });
            }

            license.Enabled = false;
            return Ok(new LicenseResponse { Success = true, Uses = license.Uses, Purchase = license.Purchase });
        }

        /// <summary>
        /// Decrement the uses count of a license
        /// </summary>
        /// <param name="licenseRequest">The license decrement request.</param>
        /// <returns>The decrement result.</returns>
        [HttpPut("decrement_uses_count")]
        [ProducesResponseType(typeof(LicenseResponse), 200)]
        [Produces("application/json")]
        public IActionResult DecrementUsesCount([FromBody] LicenseRequest licenseRequest)
        {
            var license = Licenses.FirstOrDefault(l => l.ProductId == licenseRequest.ProductId && l.LicenseKey == licenseRequest.LicenseKey);

            if (license == null)
            {
                return NotFound(new { success = false });
            }

            license.Uses -= 1;
            return Ok(new LicenseResponse { Success = true, Uses = license.Uses, Purchase = license.Purchase });
        }
    }
}
