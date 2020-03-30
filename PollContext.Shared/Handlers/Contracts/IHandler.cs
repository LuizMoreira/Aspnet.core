using PollContext.Shared.Commands.Contracts;
using System.Threading.Tasks;

namespace PollContext.Shared.Handlers.Contracts
{
    public interface IHandler<T> where T : ICommand
    {
        Task<ICommandResult> Handle(T command);
    }
}
