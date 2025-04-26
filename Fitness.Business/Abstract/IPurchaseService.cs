using Fitness.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Business.Abstract
{
    public interface IPurchaseService
    {
        Task RequestPurchaseAsync(int userId, int productId, int quantity);
        Task<List<PurchaseRequest>> GetPendingRequestsAsync();
        Task ApprovePurchaseAsync(int requestId);
    }
}
