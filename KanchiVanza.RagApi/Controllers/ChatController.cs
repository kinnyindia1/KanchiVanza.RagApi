using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Crmf;
using SmartRAG.Interfaces.Document;
using SmartRAG.Models.RequestResponse;

namespace KanchiVanza.RagApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IDocumentSearchService _searchService;

        public ChatController(IDocumentSearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] ChatRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Question))
                return BadRequest("Question cannot be empty.");

            var queryRequest = new QueryIntelligenceRequest
            {
                Query = request.Question
                // You can also set properties like MaxResults here if available
            };

            // This single call routes to SQL Server, your PDFs, or both, automatically.
            var response = await _searchService.QueryIntelligenceAsync(queryRequest);

            return Ok(new { answer = response.Answer });
        }
    
}
    public class ChatRequest
    {
        public string Question { get; set; } = string.Empty;
    }
}
