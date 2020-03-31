namespace PollContext.Domain.Services
{
    public interface ITokenService
    {
        //somente para passagem de valor fixo, alterar posteriormente
        //TODO: Criar input command user
        string GenerateToken(string id, string role);
    }
}
