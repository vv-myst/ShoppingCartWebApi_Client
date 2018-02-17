using ShoppingCartWebApiClientLib.Models;

namespace ShoppingCartWebApiClientLib.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IShoppingCartWebApi
    {
        /// <summary>
        ///     Delete all items Delete all the items in the shopping cart
        /// </summary>
        /// <returns>ShoppingCart</returns>
        ShoppingCart DeleteRequestDeleteAllItemsFromShoppingCart ();

        /// <summary>
        ///     Delete an item Delete a specific item from the shopping cart
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns>ShoppingCart</returns>
        ShoppingCart DeleteRequestDeleteItemByItemId (long itemId);

        /// <summary>
        ///     Update item count Update the item count of a particular item in the shopping cart
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="itemCount"></param>
        /// <returns>ShoppingCart</returns>
        ShoppingCart PutRequestUpdateItemCountForAnItemId (long itemId, int itemCount);

        /// <summary>
        ///     Add new item Add a new item to the shopping cart
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="body"></param>
        /// <returns>ShoppingCart</returns>
        ShoppingCart PostRequestAddItemToShoppingCart (long itemId, Item body);
    }
}