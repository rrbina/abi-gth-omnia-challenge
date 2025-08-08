using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Application.Common
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(T @event);
    }
}