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
    public class CartService : ICartService
    {
        private readonly ICartItemDal _cartItemDal;

        public CartService(ICartItemDal cartItemDal)
        {
            _cartItemDal = cartItemDal;
        }

        public async Task AddToCartAsync(int userId, int productId, int quantity)
        {
            var item = await _cartItemDal.Get(ci => ci.UserId == userId && ci.ProductId == productId);
            if (item != null)
            {
                item.Quantity += quantity;
                await _cartItemDal.Update(item);
            }
            else
            {
                await _cartItemDal.Add(new CartItem
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity
                });
            }
        }

        public async Task<List<CartItem>> GetUserCartAsync(int userId)
        {
            return await _cartItemDal.GetList(ci => ci.UserId == userId);
        }

        public async Task RemoveFromCartAsync(int userId, int productId)
        {
            var item = await _cartItemDal.Get(ci => ci.UserId == userId && ci.ProductId == productId);
            if (item != null)
            {
                await _cartItemDal.Delete(item);
            }
        }

        public async Task UpdateQuantityAsync(int userId, int productId, int newQuantity)
        {
            var item = await _cartItemDal.Get(ci => ci.UserId == userId && ci.ProductId == productId);
            if (item != null)
            {
                item.Quantity = newQuantity;
                await _cartItemDal.Update(item);
            }
        }
    }
}
