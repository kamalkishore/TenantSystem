using System.Collections.Generic;
using TenantSystem.Model.Model;

namespace TenantSystem.Model.Interface
{
    public interface IPreviousReadingDetailRepository
    {
        int Add(TenantPreviousReadingDetails previousReading);
        void Update(TenantPreviousReadingDetails previousReading);
        void Delete(TenantPreviousReadingDetails previousReading);
        IEnumerable<TenantPreviousReadingDetails> GetAll();
        TenantPreviousReadingDetails Get(int id);
        TenantPreviousReadingDetails Get(Tenant tenant);
    }
}
