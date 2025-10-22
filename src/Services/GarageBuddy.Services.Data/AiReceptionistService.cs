namespace GarageBuddy.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Core.Settings;
    using Contracts;
    using Microsoft.Extensions.Options;
    using Microsoft.SemanticKernel;
    using Microsoft.SemanticKernel.ChatCompletion;

    public class AiReceptionistService : IAiReceptionistService
    {
        private readonly Kernel kernel;
        private readonly KernelFunction function;
        private readonly bool isConfigured;

        public AiReceptionistService(IOptions<GarageInfoSettings> garageInfoOptions)
        {
            var garageInfo = garageInfoOptions.Value;
            var builder = Kernel.CreateBuilder();

            // --- IMPORTANT ---
            // To make this service functional, you must configure a chat completion service.
            // This is where you would connect to a real Large Language Model (LLM).
            //
            // Example for Azure OpenAI:
            // builder.Services.AddAzureOpenAIChatCompletion(
            //     "YourDeploymentName",
            //     "YourEndpoint",
            //     "YourApiKey");
            //
            // Example for a local open-source model (if hosted via an OpenAI-compatible API):
            // builder.Services.AddOpenAIChatCompletion(
            //     modelId: "your-model-id",
            //     apiKey: null, // API key may not be needed for local models
            //     endpoint: "http://localhost:5000");

            this.kernel = builder.Build();

            // Check if a chat completion service is available.
            this.isConfigured = this.kernel.GetServices<IChatCompletionService>().Any();

            var prompt = $@"
You are a friendly and helpful AI receptionist for a car garage named 'Garage Buddy'.
Your goal is to answer customer questions politely and concisely based ONLY on the information provided below.

--- Information ---
Opening Hours: {garageInfo.OpeningHours}
Location: {garageInfo.Location}
Services: {garageInfo.Services}
---

If the user asks a question that cannot be answered with the information above,
politely say 'I'm sorry, I don't have that information, but our team would be happy to help you.'.

User's question: {{{{input}}}}
";

            this.function = kernel.CreateFunctionFromPrompt(prompt);
        }

        public async Task<string> GetResponseAsync(string userInput)
        {
            if (!this.isConfigured)
            {
                // Return a helpful message if no AI model is configured.
                return "The AI Receptionist is currently offline. Please try again later.";
            }

            try
            {
                var result = await kernel.InvokeAsync(this.function, new() { { "input", userInput } });
                return result.GetValue<string>() ?? "I'm sorry, I encountered an issue. Please try again.";
            }
            catch (Exception ex)
            {
                // Log the exception here in a real application
                Console.WriteLine(ex);
                return "I'm sorry, I'm experiencing technical difficulties. Please contact us directly.";
            }
        }
    }
}
