using System;

namespace Uplift.DataAccess.Data.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        IFrequencyRepository Frequency { get; }
        IServiceRepository Service { get; }
        IOrderDetailsRepository OrderDetails { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IUserRepository User { get; }
        ISP_Call SP_Call { get; }
        IWebImageRepository WebImage { get; }
        void Save();
    }
}
