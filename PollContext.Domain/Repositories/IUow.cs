namespace PollContext.Domain.Repositories
{
    public interface IUow
    {
        bool Commit();
    }
}
