using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Reverse_Text_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReverseTextController : ControllerBase
    {
        [HttpGet("ReverseText")]
        public IActionResult ReverseText([FromQuery] string inputText)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                return BadRequest("Input text is empty or null.");
            }

            char[] charArray = inputText.ToCharArray();
            Array.Reverse(charArray);
            string reversedText = new string(charArray);

            return Ok(reversedText);
        }



    }
}
