using System.Collections.Generic;
using RestSharp;
using ShoppingCartWebApiClientLib.Client;
using ShoppingCartWebApiClientLib.Client.Extensions;
using ShoppingCartWebApiClientLib.Models;

namespace ShoppingCartWebApiClientLib.Api
{
    /// <summary>
    ///     Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class ShoppingCartWebApi : IShoppingCartWebApi
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ShoppingCartWebApi" /> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public ShoppingCartWebApi(ApiClient apiClient = null)
        {
            ApiClient = apiClient ?? Configuration.ShoppingCartWebApiClient;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ShoppingCartWebApi" /> class.
        /// </summary>
        /// <returns></returns>
        public ShoppingCartWebApi(string basePath)
        {
            ApiClient = new ApiClient(basePath);
        }

        /// <summary>
        ///     Gets or sets the API client.
        /// </summary>
        /// <value>An instance of the ApiClient</value>
        public ApiClient ApiClient { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Delete all items Delete all the items in the shopping cart
        /// </summary>
        /// <returns>ShoppingCart</returns>
        public ShoppingCart DeleteRequestDeleteAllItemsFromShoppingCart()
        {
            var path = "/shoppingcart";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<string, string>();
            var headerParams = new Dictionary<string, string>();
            var formParams = new Dictionary<string, string>();
            var fileParams = new Dictionary<string, FileParameter>();

            // make the HTTP request
            var response = (IRestResponse) ApiClient.CallApi(path, Method.DELETE, queryParams, null, headerParams,
                formParams, fileParams);

            if ((int) response.StatusCode >= 400)
                throw new ApiException((int) response.StatusCode,
                    "Error calling ShoppingcartDelete: " + response.Content, response.Content);
            if ((int) response.StatusCode == 0)
                throw new ApiException((int) response.StatusCode,
                    "Error calling ShoppingcartDelete: " + response.ErrorMessage, response.ErrorMessage);

            return (ShoppingCart) response.Content.Deserialize(typeof(ShoppingCart), response.Headers);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Delete an item Delete a specific item from the shopping cart
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns>ShoppingCart</returns>
        public ShoppingCart DeleteRequestDeleteItemByItemId(long itemId)
        {
            var path = "/shoppingcart/{itemid}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "itemid" + "}", itemId.ParameterToString());

            var queryParams = new Dictionary<string, string>();
            var headerParams = new Dictionary<string, string>();
            var formParams = new Dictionary<string, string>();
            var fileParams = new Dictionary<string, FileParameter>();

            // make the HTTP request
            var response = (IRestResponse) ApiClient.CallApi(path, Method.DELETE, queryParams, null, headerParams,
                formParams, fileParams);

            if ((int) response.StatusCode >= 400)
                throw new ApiException((int) response.StatusCode,
                    "Error calling ShoppingcartItemidDelete: " + response.Content, response.Content);
            if ((int) response.StatusCode == 0)
                throw new ApiException((int) response.StatusCode,
                    "Error calling ShoppingcartItemidDelete: " + response.ErrorMessage, response.ErrorMessage);

            return (ShoppingCart) response.Content.Deserialize(typeof(ShoppingCart), response.Headers);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Update item count Update the item count of a particular item in the shopping cart
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="itemCount"></param>
        /// <returns>ShoppingCart</returns>
        public ShoppingCart PutRequestUpdateItemCountForAnItemId(long itemId, int itemCount)
        {
            var path = "/shoppingcart/{itemid}/{itemcount}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "itemid" + "}", itemId.ParameterToString());
            path = path.Replace("{" + "itemcount" + "}", itemCount.ParameterToString());

            var queryParams = new Dictionary<string, string>();
            var headerParams = new Dictionary<string, string>();
            var formParams = new Dictionary<string, string>();
            var fileParams = new Dictionary<string, FileParameter>();

            // make the HTTP request
            var response = (IRestResponse) ApiClient.CallApi(path, Method.PUT, queryParams, null, headerParams,
                formParams, fileParams);

            if ((int) response.StatusCode >= 400)
                throw new ApiException((int) response.StatusCode,
                    "Error calling ShoppingcartItemidItemcountPut: " + response.Content, response.Content);
            if ((int) response.StatusCode == 0)
                throw new ApiException((int) response.StatusCode,
                    "Error calling ShoppingcartItemidItemcountPut: " + response.ErrorMessage, response.ErrorMessage);

            return (ShoppingCart) response.Content.Deserialize(typeof(ShoppingCart), response.Headers);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Add new item Add a new item to the shopping cart
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="body"></param>
        /// <returns>ShoppingCart</returns>
        public ShoppingCart PostRequestAddItemToShoppingCart(long itemId, Item body)
        {
            // verify the required parameter 'body' is set
            if (body == null)
                throw new ApiException(400, "Missing required parameter 'body' when calling ShoppingcartItemidPost");

            var path = "/shoppingcart/{itemid}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "itemid" + "}", itemId.ParameterToString());

            var queryParams = new Dictionary<string, string>();
            var headerParams = new Dictionary<string, string>();
            var formParams = new Dictionary<string, string>();
            var fileParams = new Dictionary<string, FileParameter>();

            var postBody = body.Serialize();

            // make the HTTP request
            var response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams,
                formParams, fileParams);

            if ((int) response.StatusCode >= 400)
                throw new ApiException((int) response.StatusCode,
                    "Error calling ShoppingcartItemidPost: " + response.Content, response.Content);
            if ((int) response.StatusCode == 0)
                throw new ApiException((int) response.StatusCode,
                    "Error calling ShoppingcartItemidPost: " + response.ErrorMessage, response.ErrorMessage);

            return (ShoppingCart) response.Content.Deserialize(typeof(ShoppingCart), response.Headers);
        }

        /// <summary>
        ///     Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public string GetBasePath()
        {
            return ApiClient.BasePath;
        }
    }
}