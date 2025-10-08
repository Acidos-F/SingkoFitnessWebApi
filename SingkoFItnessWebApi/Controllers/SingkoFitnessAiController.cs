using Microsoft.AspNetCore.Mvc;
using SingkoFItnessWebApi.Dtos.AiAskDto;
using System.Text;
using System.Text.Json;

/// <summary>
/// Controller responsible for handling AI-powered fitness queries using the Gemini API.
/// Acts as a middleware between the Singko Fitness app and the Gemini generative language model.
/// </summary>
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

    /// <summary>
    /// Sends a user prompt to the Gemini API and returns a JSON-formatted AI-generated fitness response.
    /// </summary>
    [HttpPost("ask")]
    public async Task<IActionResult> Ask([FromBody] AiAskDto request)
    {
        string apiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY");
        string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";

        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new
                        {
                            text = $"You are a fitness assistant from Singko Fitness. " +
                                   $"Introduce yourself first. Don't answer questions unrelated to fitness, gym, or exercise. " +
                                   $"You MUST reply with valid JSON only. Do NOT include any explanatory text or markdown code fences. " +
                                   $"Schema: {{\\\"title\\\":\\\"string\\\",\\\"steps\\\":[\\\"string\\\"]}}. " +
                                   $"User question: {request.Prompt}"
                        }
                    }
                }
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
