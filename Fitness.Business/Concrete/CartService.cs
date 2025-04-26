using Fitness.Business.Abstract;
using Fitness.DataAccess.Abstract;
using Fitness.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

public class CartService : ICartService
{
    private readonly ICartItemDal _cartItemDal;
    private readonly IProductDal _productDal;
    private readonly IUserDal _userDal;
    private readonly IPurchaseRequestDal _purchaseRequestDal;

    public CartService(ICartItemDal cartItemDal, IProductDal productDal, IUserDal userDal, IPurchaseRequestDal purchaseRequestDal)
    {
        _cartItemDal = cartItemDal;
        _productDal = productDal;
        _userDal = userDal;
        _purchaseRequestDal = purchaseRequestDal;
    }

    public async Task BuyAllFromCartAsync(int userId)
    {
      
        var cartItems = await _cartItemDal.GetList(
     ci => ci.UserId == userId,
     query => query.Include(ci => ci.Product)
                   .Include(ci => ci.User)
 );

        var user = cartItems.FirstOrDefault()?.User;
        if (user == null)
            throw new Exception("User not found");

        int totalPointCost = 0;

        foreach (var item in cartItems)
        {
            var product = item.Product;
            if (product == null) continue;

            totalPointCost += item.Quantity * product.PointCost;

            await _purchaseRequestDal.Add(new PurchaseRequest
            {
                UserId = userId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                IsApproved = false,
                RequestedAt = DateTime.Now
            });
        }

        if (totalPointCost > user.Point)
            throw new Exception("Not enough points for all items in cart");

        
        foreach (var item in cartItems)
        {
            await _cartItemDal.Delete(item);
        }
    }

    public async Task UpdateQuantityAsync(int userId, int productId, int newQuantity)
    {
        var item = await _cartItemDal.Get(ci => ci.UserId == userId && ci.ProductId == productId);
        if (item == null)
            throw new Exception("Cart item not found");

        var product = item.Product;
        if (product == null)
            throw new Exception("Product not found");

        var user = item.User; 
        if (user == null)
            throw new Exception("User not found");

        int totalPointCost = newQuantity * product.PointCost;

        if (totalPointCost > user.Point)
            throw new Exception("Not enough points");

        item.Quantity = newQuantity;
        await _cartItemDal.Update(item);
    }

    public async Task AddToCartAsync(int userId, int productId, int quantity)
    {
        var product = await _productDal.Get(p => p.Id == productId);
        if (product == null)
        {
            throw new Exception("Product not found");
        }
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
        return await _cartItemDal.GetList(
    ci => ci.UserId == userId,
    query => query.Include(ci => ci.Product)
);

    }

    public async Task RemoveFromCartAsync(int userId, int productId)
    {
        var item = await _cartItemDal.Get(ci => ci.UserId == userId && ci.ProductId == productId);
        if (item != null)
        {
            await _cartItemDal.Delete(item);
        }
    }
}
