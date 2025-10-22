namespace GarageBuddy.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IAiReceptionistService
    {
        Task<string> GetResponseAsync(string userInput);
    }
}
