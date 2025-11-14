using BeeBuzz.Data.Interfaces;
using BeeBuzz.Data.Repositories;

namespace BeeBuzz.Data
{
    public interface IUnitOfWork : IDisposable
    {
        T GetRepository<T>() where T : class;
        //DutchProductRepository ProductRepository { get; }
        //DutchOrderRepository OrderRepository { get; }
        //DutchOrderItemRepository OrderItemRepository { get; }
    }
}