using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using MyTasks.Repositories.Interfaces.IProjecRepository;
using System.Net;
using System.Text.Json;
using System.Collections.Concurrent;

namespace MyTasks.Functions;

public class Functions
{
    private readonly ILogger<Functions> _logger;
    private readonly IProjecRepository _projectRepository;

    // Simulated in-memory queue
    private static readonly ConcurrentQueue<string> _inMemoryQueue = new();

    public Functions(IProjecRepository projecRepository, ILogger<Functions> logger)
    {
        _projectRepository = projecRepository;
        _logger = logger;
    }

    [Function("GetAllProjects")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        var data = await _projectRepository.GetAllProjects();

        var response = req.CreateResponse(HttpStatusCode.OK);

        var jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(data, jsonOptions);
        await response.WriteStringAsync(json);

        return response;
    }

    // TimerTrigger every 30 seconds – generates a random GUID, inserts it into the simulated queue,
    // and immediately processes all messages in the queue
    [Function("TimerFunction")]
    public void RunTimer([TimerTrigger("*/30 * * * * *")] TimerInfo timer)
    {
        // Generate random GUID
        string guidMessage = Guid.NewGuid().ToString();
        _logger.LogInformation($"Timer triggered at: {DateTime.UtcNow}, enqueueing GUID: {guidMessage}");

        // Enqueue GUID
        _inMemoryQueue.Enqueue(guidMessage);

        // Immediately process all messages in the simulated queue
        while (_inMemoryQueue.TryDequeue(out var msg))
        {
            _logger.LogInformation($"Queue processed GUID: {msg}");
        }
    }
}