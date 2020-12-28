
using System.Threading;
using System.Threading.Tasks;

namespace SwishDB.TestCli.Commands
{
    public interface IAbstractCommand
    {
        Task ExecuteAsync(object options, CancellationToken cancellationToken);
    }
}
