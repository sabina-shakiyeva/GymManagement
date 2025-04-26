using FitnessManagement.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Business.Abstract
{
    public interface IPaymentService
    {
        Task<string> PurchasePackageAsync(string identityUserId, int packageId, PaymentDto paymentDto);
    }
}
