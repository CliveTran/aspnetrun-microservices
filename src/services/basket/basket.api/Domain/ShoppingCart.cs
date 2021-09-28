
using System.Collections.Generic;

namespace basket.api.Domain;
public class ShoppingCart
{
    public string UserName { get; set; }

    public List<ShoppingCartItem> Items { get; set; } = new();

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }

    public decimal TotalPrice
    {
        get
        {
            decimal totalPrice = 0;
            foreach (ShoppingCartItem item in Items)
            {
                totalPrice += item.Price * item.Quantity;
            }
            return totalPrice;
        }
    }
}
