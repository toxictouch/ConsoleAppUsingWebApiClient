using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

// This application was built by following the steps in this tutorial: https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient
// Author: Stanley Munson
// Date: 2022-Sep-19

namespace WebAPIClient
{
    class Program
    {
        // Handles requests and responses.
        private static readonly HttpClient client = new HttpClient();

        // Changed to async to be able to call the ProcessRepositories
        static async Task Main(string[] args)
        {
            string Github_ApiEndpoint = "https://api.github.com/orgs/dotnet/repos";

            // Call the method that will REQUEST from the api endpoint
            List<Repository> repositories = await ProcessRepositories(Github_ApiEndpoint);

            // Read each repo from the repositories list while writing the repos name to the console
            foreach (Repository repo in repositories)
            {
                Console.WriteLine($"Repo Name: {repo.Name}");
                Console.WriteLine($"Github URL: {repo.GitHubHomeUrl}");
                Console.WriteLine($"Project Homepage: {repo.Homepage}");
                Console.WriteLine($"Number of github watchers: {repo.Watchers}");
                Console.WriteLine($"Last update pushed: {repo.LastPush}");
                Console.WriteLine($"Description: {repo.Description}");
                Console.WriteLine();
            }
        }

        // Call the GitHub endpoint that returns a list of all repositories under the .NET foundation organization.
        private static async Task<List<Repository>> ProcessRepositories(string target)
        {
            client.DefaultRequestHeaders.Accept.Clear();

            // An Accept header to accept JSON responses
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            
            // These headers are checked by the GitHub server code and are necessary to retrieve information from GitHub.
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");


            #region replaced by code using the repos class
            // Calls HttpClient.GetStringAsync(String) to make a web request and retrieve the response. 
            // This method starts a task that makes the web request.
            // When the request returns, the task reads the response stream and extracts the content from the stream.
            // var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");
            #endregion


            // This serializer method uses a stream instead of a string as its source.
            var streamTask = client.GetStreamAsync(target);
            // The DeserializeAsync method is generic.
            // The other two parameters, JsonSerializerOptions and CancellationToken, are optional and are omitted in the code snippet.
            // The type argument is your Repository class, because the JSON text represents a collection of repository objects.
            List<Repository>? repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);


            // Returns the list of repositories
            // (but only if it's not NULL for christ's sake {check get's rid of the warning}) 
            if (repositories != null)
                return repositories;
            // If it is null, then just return an empty list
            else
                return new List<Repository>();
            #region replaced by code using the repos class
            // Awaits the task for the response string and prints the response to the console.
            // var msg = await stringTask;
            // Console.Write(msg);
            #endregion
        }
    }
}