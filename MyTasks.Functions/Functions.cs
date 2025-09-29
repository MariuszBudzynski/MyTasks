using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using MyTasks.Repositories.Interfaces.IProjecRepository;
using System.Net;
using System.Text.Json;

namespace MyTasks.Functions;

public class Functions
{
    private readonly ILogger<Functions> _logger;
    private readonly IProjecRepository _projectRepository;

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
}