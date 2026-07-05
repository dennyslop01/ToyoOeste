namespace ToyoCarsClients.Business.Interfaces
{
    public interface IEncuesta
    {
        Task<bool> EnviarEncuestaAsync(string email, int rating, string comments);
    }
}
