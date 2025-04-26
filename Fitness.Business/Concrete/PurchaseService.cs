using Fitness.Business.Abstract;
using Fitness.DataAccess.Abstract;
using Fitness.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Business.Concrete
{
    public class PurchaseService:IPurchaseService
    {
        private readonly IPurchaseRequestDal _purchaseDal;
        private readonly IUserDal _userDal;
        private readonly IProductDal _productDal;

        public PurchaseService(IPurchaseRequestDal purchaseDal, IUserDal userDal, IProductDal productDal)
        {
            _purchaseDal = purchaseDal;
            _userDal = userDal;
            _productDal = productDal;
        }

        public async Task ApprovePurchaseAsync(int requestId)
        {
            var request = await _purchaseDal.Get(r => r.Id == requestId);
            if (request == null) throw new Exception("Request not found.");

            var user = await _userDal.Get(u => u.Id == request.UserId);
            var product = await _productDal.Get(p => p.Id == request.ProductId);
            var totalCost = product.PointCost * request.Quantity;

            if (user.Point < totalCost)
                throw new Exception("User has insufficient points.");

            user.Point -= totalCost;
            product.Stock -= request.Quantity;
            request.IsApproved = true;
            request.ApprovedAt = DateTime.UtcNow;

            await _userDal.Update(user);
            await _productDal.Update(product);
            await _purchaseDal.Update(request);
        }

        public async Task<List<PurchaseRequest>> GetPendingRequestsAsync()
        {
            return await _purchaseDal.GetList(r => !r.IsApproved);
        }

        public async Task RequestPurchaseAsync(int userId, int productId, int quantity)
        {
            var product=await _productDal.Get(p => p.Id == productId);
            if (product == null || product.Stock < quantity)
                throw new Exception("Product not available in desired quantity.");
            await _purchaseDal.Add(new PurchaseRequest
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantity,
                RequestedAt = DateTime.UtcNow,
                IsApproved = false
                
                
            });
        }
    }
}
