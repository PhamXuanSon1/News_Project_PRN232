using Microsoft.AspNetCore.Mvc;

namespace News_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SocialAuthController : ControllerBase
    {
        // POST: api/SocialAuth/google
        [HttpPost("google")]
        public IActionResult GoogleLogin([FromBody] string googleToken)
        {
            // Ở đây chỉ mock, thực tế sẽ xác thực với Google
            if (string.IsNullOrEmpty(googleToken)) return BadRequest("Missing Google token");
            // Trả về token giả lập
            return Ok(new { access_token = "mock_google_access_token", provider = "Google" });
        }

        // POST: api/SocialAuth/facebook
        [HttpPost("facebook")]
        public IActionResult FacebookLogin([FromBody] string facebookToken)
        {
            // Ở đây chỉ mock, thực tế sẽ xác thực với Facebook
            if (string.IsNullOrEmpty(facebookToken)) return BadRequest("Missing Facebook token");
            // Trả về token giả lập
            return Ok(new { access_token = "mock_facebook_access_token", provider = "Facebook" });
        }
    }
} 