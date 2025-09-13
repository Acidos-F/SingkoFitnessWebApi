using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SingkoFItnessWebApi.Dtos;

[ApiController]
[Route("api/[controller]")]
public class SingkoFitnessAiController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    public SingkoFitnessAiController(IConfiguration config)
    {
        _config = config;
        _httpClient = new HttpClient();
    }

    [HttpPost("ask")]
    public async Task<IActionResult> Ask([FromBody] AiAskDto request)
    {
        string apiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY");
        string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";

        var requestBody = new
        {
            contents = new[]
            {
                new { parts = new[] { new { text = $"You are a fitness assistant from Singko Fitness. Introduce youself first. dont answer questions that are unrelated to fitness, gym, excercise. You MUST reply with valid JSON only. Do NOT include any explanatory text or markdown code fences. Schema: {{\\\"title\\\":\\\"string\\\",\\\"steps\\\":[\\\"string\\\"]}}. User question: {request.Prompt}" } } }
            }
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);
        var responseJson = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(responseJson);

        string reply = doc.RootElement
                          .GetProperty("candidates")[0]
                          .GetProperty("content")
                          .GetProperty("parts")[0]
                          .GetProperty("text")
                          .GetString()
                          .Replace("```json", "")
                          .Replace("```", "")
                          .Trim();

        return Ok(reply);
    }
}